/**
 * Created by liuchengyun on 2017/5/24.
 */
//用户管理详情控制器
app.controller('userEditController',function ($scope,$cookieStore,$http,$resource,$route,$routeParams,$location,CONSTANT,dataService) {
    //********************************作用域存储变量**********开始********************************
    $scope.queryId = $routeParams.id;
    //用户登录信息
    $scope.userinfo = null;
    //用户详情
    $scope.user = null;
    //用户操作频率控制
    $scope.cando = true;
    //********************************作用域存储变量**********结束********************************
    // -------------------------------------------------------------------------------------------
    // -----------------------------------分割线--------------------------------------------------
    // -------------------------------------------------------------------------------------------
    // ******************************.辅助功能性方法区域******开始********************************
    //获取用户缓存信息
    $scope.getUserInfo = function () {
        $scope.userinfo = $cookieStore.get(CONSTANT.USER_INFO);
    }
    // *******************************辅助功能性方法区域******结束********************************
    // ------------------------------------------------------------------------------------------
    // -----------------------------------分割线-------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // ******************************.视图业务控制接口区域******开始******************************
    //获取单个用户详情数据
    $scope.getUserDetail = function () {
        //每次查询前判断验证用户是否过期
        $scope.isLoginIn();
        $http({
            method:'post',
            url: CONSTANT.URL_LIST.WCF + 'user/getuser',
            data:{
                account:$scope.userinfo.account,
                token:$scope.userinfo.token,
                user_id:$scope.queryId
            }
        }).then(function (res) {
            if(res.data.return_flag==0){
                // 获取特巡报告列表
                if(angular.isObject(res.data)){
                    $scope.user = res.data.user;
                }
            }else{
                //关闭详情窗口 回到列表页面获,取数据失败
                //$location.path('/user').replace();
            }
        });
    }
    //更新用户详情信息
    $scope.updateUser = function () {
        //每次查询前判断验证用户是否过期
        $scope.isLoginIn();
        if(!$scope.cando) return;
        $scope.cando = false;
        $http({
            method:'post',
            url: CONSTANT.URL_LIST.WCF + 'user/update',
            data:{
                account:$scope.userinfo.account,
                token:$scope.userinfo.token,
                user:$scope.user
            }
        }).then(function (res) {
            if(res.data.return_flag==0){
                // 更新成功
                alert("更新成功");
                $scope.cando = true;
                //$location.path('/user').replace();
            }else{
                //关闭详情窗口 回到列表页面获,取数据失败
                alert("更新失败");
                //$location.path('/user').replace();
            }
        });
    }

    //新增权限用户到特巡系统详情信息
    $scope.addUser = function () {
        //每次查询前判断验证用户是否过期
        $scope.isLoginIn();
        if(!$scope.cando) return;
        $scope.cando = false;
        $http({
            method:'post',
            url: CONSTANT.URL_LIST.WCF + 'user/add',
            data:{
                account:$scope.userinfo.account,
                token:$scope.userinfo.token,
                user:$scope.user
            }
        }).then(function (res) {
            if(res.data.return_flag==0){
                // 更新成功
                alert("新增成功");
                $scope.cando = true;
                //$location.path('/user').replace();
            }else{
                //关闭详情窗口 回到列表页面获,取数据失败
                alert("新增失败");
                //$location.path('/user').replace();
            }
        });
    }
    //删除用户详情信息
    $scope.deleteUser = function () {
        //每次查询前判断验证用户是否过期
        $scope.isLoginIn();
        if(!$scope.cando) return;
        $scope.cando = false;
        $http({
            method:'post',
            url: CONSTANT.URL_LIST.WCF + 'user/delete',
            data:{
                account:$scope.userinfo.account,
                token:$scope.userinfo.token,
                user_id:$scope.user.user_id
            }
        }).then(function (res) {
            if(res.data.return_flag==0){
                // 更新成功
                alert("删除成功");
                $scope.cando = true;
                //$location.path('/user').replace();
            }else{
                //关闭详情窗口 回到列表页面获,取数据失败
                alert("删除失败");
                //$location.path('/user').replace();
            }
        });
    }
    //不保存退出
    $scope.exitNoSave = function () {
        $location.path("/user").replace();
    }
    //保存后退出
    $scope.exitWithSave = function () {
        $scope.updateUser();
    }
    // *******************************视图业务控制接口区域******结束*****************************
    // ------------------------------------------------------------------------------------------
    // -----------------------------------分割线-------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // ******************************.控制器初始化区域******开始**********************************
    $scope.Init = function () {
        //验证用户是否有效
        $scope.isLoginIn();
        //获取用户信息
        $scope.getUserInfo();
        //获取列表信息
        $scope.getUserDetail();
    }
    // *******************************控制器初始化区域******结束**********************************

    //启动函数
    $scope.Init();
});