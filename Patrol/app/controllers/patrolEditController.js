/**
 * Created by liuchengyun on 2017/5/24.
 */
//特巡报告控制器
app.controller('patrolEditController',function ($scope,$cookieStore,$http,$resource,$route,$routeParams,$location,CONSTANT,dataService) {
    //********************************作用域存储变量**********开始********************************
    $scope.queryId = $routeParams.id;
    //用户登录信息
    $scope.userinfo = null;
    // 特巡头部信息
    $scope.headerInfo = {};
    // 特巡点检列表
    $scope.detailList = {};
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
    //获取指定特巡报告头部和详情数据
    $scope.getPatrol = function () {
        //每次查询前判断验证用户是否过期
        $scope.isLoginIn();
        $http({
            method:'post',
            url: CONSTANT.URL_LIST.WCF + 'patrol/getpatrol',
            data:{
                account:$scope.userinfo.account,
                token:$scope.userinfo.token,
                patrol_no:$scope.queryId
            }
        }).then(function (res) {
            if(res.data.return_flag==0){
                // 获取特巡报告列表
                if(angular.isObject(res.data)){
                    $scope.headerInfo = res.data.patrol_header;
                    $scope.detailList = res.data.patrol_detail_list;
                }
            }else{
                //关闭详情窗口 回到列表页面获,取数据失败
                //$location.path('/patrol').replace();
            }
        });
    }
    //更新特巡报告头部和详情信息
    $scope.updatePatrol = function () {
        //每次查询前判断验证用户是否过期
        $scope.isLoginIn();
        $http({
            method:'post',
            url: CONSTANT.URL_LIST.WCF + 'patrol/update',
            data:{
                account:$scope.userinfo.account,
                token:$scope.userinfo.token,
                patrol_header:$scope.headerInfo,
                patrol_detail_list:$scope.detailList
            }
        }).then(function (res) {
            if(res.data.return_flag==0){
                // 更新成功
                alert("更新成功");
                //$location.path('/patrol').replace();
            }else{
                //关闭详情窗口 回到列表页面获,取数据失败
                alert("更新失败");
                //$location.path('/patrol').replace();
            }
        });
    }
    //生成特巡报告书
    $scope.createReport = function () {
        //每次查询前判断验证用户是否过期
        $scope.isLoginIn();
        $http({
            method:'post',
            url: CONSTANT.URL_LIST.WCF + 'patrol/createReport/' + $scope.queryId
        }).then(function (res) {
            if(res.data){
                // 生成成功
                alert("生成报告书成功");
                //$location.path('/patrol').replace();
            }else{
                //关闭详情窗口 回到列表页面获,取数据失败
                alert("生成报告书失败");
                //$location.path('/patrol').replace();
            }
        });
    }
    //不保存退出
    $scope.exitNoSave = function () {
        $location.path("/patrol").replace();
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
        $scope.getPatrol();
    }
    // *******************************控制器初始化区域******结束**********************************

    //启动函数
    $scope.Init();
});