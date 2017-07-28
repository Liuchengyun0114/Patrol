/**
 * Created by liuchengyun on 2017/5/24.
 */
    //特巡报告控制器
app.controller('patrolController',function ($scope,$cookieStore,$http,$resource,$route,$routeParams,$location,CONSTANT,dataService) {
    //********************************作用域存储变量**********开始********************************
    $scope.date1 = "";
    $scope.date2 = "";
    $scope.$watch("date1",function () {
        if($scope.date1){
            var dateinfo = new Date($scope.date1);
            var year = dateinfo.getFullYear().toString();
            var mon = "0" + (dateinfo.getMonth() + 1).toString();
            mon = mon.substr(mon.length-2,2);
            var day ="0" + dateinfo.getDate().toString();
            day = day.substr(day.length-2,2);
            $scope.searchInfo.ReportDateStart = "".concat(year,mon,day);
        }else{
            $scope.searchInfo.ReportDateStart = "";
        }
    });
    $scope.$watch("date2",function () {
        if($scope.date2) {
            var dateinfo = new Date($scope.date2);
            var year = dateinfo.getFullYear().toString();
            var mon = "0" + (dateinfo.getMonth() + 1).toString();
            mon = mon.substr(mon.length - 2, 2);
            var day = "0" + dateinfo.getDate().toString();
            day = day.substr(day.length - 2, 2);
            $scope.searchInfo.ReportDateClosed = "".concat(year, mon, day);
        }else{
            $scope.searchInfo.ReportDateClosed = "";
        }
    });
    $scope.format = "yyyyMMdd";
    $scope.altInputFormats = ['yyyyMMdd'];

    $scope.popup1 = {
        opened: false
    };
    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };
    $scope.popup2 = {
        opened: false
    };
    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };
    //列表数据
    $scope.patrollist = [];
    //分页信息
    $scope.paginatorInfo = {
        currentIndex:CONSTANT.PAGINATOR_INFO.CURRENT_INDEX,
        currrentPageSize:CONSTANT.PAGINATOR_INFO.CURRENT_PAGE_SIZE,
        changeIndex:function (index) {
            $scope.searchInfo.paginator.PageIndex = index;
            $scope.getPatrolList();
        },
        changePageSize:function (pagesize) {
            $scope.searchInfo.paginator.PageSize = pagesize;
            $scope.getPatrolList();
        }

    }
    //用户登录信息
    $scope.userinfo = null;
    //查询条件数据
    $scope.searchModel = {};
    // 查询条件对象
    $scope.searchInfo = {
        ReportDateStart:"",
        ReportDateClosed:"",
        ReportStatus:"",
        AgencyShop:"",
        Filiale:"",
        Reporter:"",
        MachineType:"",
        Customer:"",
        MachineNO:"",
        MachineStatus:"",
        IsEmergency:"",
        HasErrorImage:"",
        LocationCode:"",
        SpotCode:"",
        Remarks:"",
        Paginator:{
            PageIndex:CONSTANT.PAGINATOR_INFO.CURRENT_INDEX,
            PageSize:CONSTANT.PAGINATOR_INFO.CURRENT_PAGE_SIZE
        }
    };

    //设置分页的参数
    $scope.option = {
        config:{
            curr: 1,  //当前页数
            totalcount:1,  //总页数
            maxpage:3,//最多显示页码按钮数量
            currsize:10,//最多显示的页数，默认为10
            pagesize:[10,50,100]
        },
        //点击页数的回调函数，参数page为点击的页数
        click: function (page) {
            //设置分页码和页数
            $scope.option.refresh();
        },
        changesize:function (size) {
            $scope.option.refresh();
        },
        refresh:function () {
            $scope.option.updatePaginator();
            $scope.getPatrolList();
        },
        updatePaginator:function () {
            $scope.searchInfo.Paginator.PageSize = $scope.option.config.currsize;
            $scope.searchInfo.Paginator.PageIndex = $scope.option.config.curr;
        }
    }
    $scope.ajaxComplete = true;
    //********************************作用域存储变量**********结束********************************
    // -------------------------------------------------------------------------------------------
    // -----------------------------------分割线--------------------------------------------------
    // -------------------------------------------------------------------------------------------
    // ******************************.辅助功能性方法区域******开始********************************
    $scope.getSearchInfo = function () {
        //用户查询条件
        var search = dataService.get(CONSTANT.PATROL_SEARCH_INFO);
        if(search){
            //从缓存中获取查询条件基础数据,没有则重新从服务器获取
            $scope.searchInfo = search;
        }
        //分页控件配置信息
        search = dataService.get(CONSTANT.PATROL_PAGINATOR_CONFIG);
        if(search){
            //从缓存中获取查询条件基础数据,没有则重新从服务器获取
            $scope.option.config = search;
        }
        //查询基础数据
        search = dataService.get(CONSTANT.PATROL_SEARCH_BASE);
        if(search){
            //从缓存中获取查询条件基础数据,没有则重新从服务器获取
            $scope.searchModel = search;
        }else{
            //从服务器获取一遍
            $scope.getSearchBase();
        }
    }
    //获取用户缓存信息
    $scope.getUserInfo = function () {
        $scope.userinfo = $cookieStore.get(CONSTANT.USER_INFO);
    }
    //更新用户查询信息变化情况 点击查询时候写入缓存中
    $scope.updateSearchInfo = function () {
        dataService.set(CONSTANT.PATROL_SEARCH_INFO,$scope.searchInfo);
        dataService.set(CONSTANT.PATROL_PAGINATOR_CONFIG,$scope.option.config);
    }
    //更新基础数据缓存
    $scope.updateSearchBase = function () {
        dataService.set(CONSTANT.PATROL_SEARCH_BASE,$scope.searchModel);
    }
    //异步回调更新状态 防止频繁后台ajax调用
    $scope.changeState = function (state) {
        $scope.ajaxComplete = state;
    }

    //画面联动效果控制
    $scope.changeLocation = function (code) {
        for(var i=0;i<$scope.searchModel.check_list.length;i++){
            if($scope.searchModel.check_list[i].code == code){
                //返回code所在索引
                $scope.locationIndex = i;
                break;
            }
        }
    }
    // *******************************辅助功能性方法区域******结束********************************
    // ------------------------------------------------------------------------------------------
    // -----------------------------------分割线-------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // ******************************.视图业务控制接口区域******开始******************************
    $scope.getPatrolList = function () {
        //每次查询前判断验证用户是否过期
        $scope.isLoginIn();
        //有任务未完成停止执行 避免重复ajax提交
        if(!$scope.ajaxComplete) return;
        $scope.changeState(false);
        $http({
            method:'post',
            url: CONSTANT.URL_LIST.WCF + 'patrol/getpatrollist',
            data:{
                account:$scope.userinfo.account,
                token:$scope.userinfo.token,
                search_info:$scope.searchInfo
            }
        }).then(function (res) {
            if(res.data.return_flag==0){
                // 获取特巡报告列表
                if(angular.isArray(res.data.patrol_list)){
                    $scope.patrollist = res.data.patrol_list;
                    //更新分页器
                    $scope.option.config.totalcount = res.data.count;
                    $scope.option.updatePaginator();
                    //将查询条件写入缓存中以便下次使用
                    $scope.updateSearchInfo();
                    $scope.changeState(true);
                }
            }else{
                //没有获取到数据
                $scope.patrollist = [];
            }
        });
    }
    //按条件查询
    $scope.searchByCondition = function () {
        $scope.searchInfo.Paginator.PageIndex = CONSTANT.PAGINATOR_INFO.CURRENT_INDEX;
        $scope.searchInfo.Paginator.PageSize = CONSTANT.PAGINATOR_INFO.CURRENT_PAGE_SIZE;
        $scope.option.config.totalcount = 1;
        $scope.option.config.curr = 1;
        $scope.option.refresh();
    }
    //获得查询条件中的基础数据
    $scope.getSearchBase = function () {
        $http({
            method:'post',
            url:CONSTANT.URL_LIST.WCF + 'patrol/getpatrolbase'
        }).then(function (res) {
            if(res.data.return_flag==0){
                if(angular.isObject(res.data.patrolbase)){
                    $scope.searchModel = res.data.patrolbase;
                    $scope.updateSearchBase();
                }
            }else{
                //获取基础数据失败,请重新登录
                alert("获取基础数据失败");
                //$scope.relogin();
            }
        });
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
        //加载查询条件
        $scope.getSearchInfo();
        //获取列表信息
        $scope.getPatrolList();
    }
    // *******************************控制器初始化区域******结束**********************************

    //启动函数
    $scope.Init();
});