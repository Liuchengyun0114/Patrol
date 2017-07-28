/**
 * Created by liuchengyun on 2017/5/24.
 */
    // 主控制器
app.controller('baseController',function($timeout,$scope,$http,$location,$cookieStore,CONSTANT){
    $scope.user = {
        account:'QCR00000',//默认账号测试
        pwd:'admin111111',//默认密码测试
        name:'',
        token:'',
        isLogin:false,
        timeoutAt:0
    };
    //全局路由地址变量集合
    $scope.URL_LIST = CONSTANT.URL_LIST;//注入常量地址到本作用域

    //导航页面控制变量
    $scope.loginAt = new Date().toLocaleDateString();

    //退出系统
    $scope.loginOut = function () {
        $location.path('/').replace();
        return;
    }

    //验证是否登录超时
    $scope.isTimeout = function () {
        var uservalidate = $cookieStore.get(CONSTANT.USER_INFO);
        var accessAt = new Date().getTime();
        if(uservalidate == null || uservalidate.token == "" || uservalidate.timeoutAt == 0 || uservalidate.timeoutAt < accessAt){
            //会话超时,回到登录页面
            alert("验证超时");
            return true;
        }
        return false;
    }

    //跳转登录页面
    $scope.relogin = function () {
        $location.path("/").replace();
        return;
    }

    //用户会话超时处理
    $scope.isLoginIn = function () {
        if($scope.isTimeout()){
            $scope.relogin();
        }
    }

    //刷新cookie有效期
    $scope.updateCookie = function () {
        var uservalidate = $cookieStore.get(CONSTANT.USER_INFO);
        var accessAt = new Date().getTime();
        uservalidate.timeoutAt = accessAt + CONSTANT.LIFE_TIME;

        $cookieStore.put(CONSTANT.USER_INFO,uservalidate);
    }

    //设置cookie
    $scope.setCookie = function (token) {
        $scope.user.isLogin = true;
        $scope.user.token = token;
        $scope.user.timeoutAt = new Date().getTime() + CONSTANT.LIFE_TIME;

        $cookieStore.put(CONSTANT.USER_INFO,$scope.user);
    }
});