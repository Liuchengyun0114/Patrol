/**
 * Created by liuchengyun on 2017/5/24.
 */
//用户管理列表控制器
app.controller('userController',function ($scope,$cookieStore,$http,$resource,$route,$routeParams,$location,CONSTANT,dataService) {
    //********************************作用域存储变量**********开始********************************
    //列表数据
    $scope.userlist = [];
    //分页信息
    $scope.paginatorInfo = {
        currentIndex:CONSTANT.PAGINATOR_INFO.CURRENT_INDEX,
        currrentPageSize:CONSTANT.PAGINATOR_INFO.CURRENT_PAGE_SIZE,
        changeIndex:function (index) {
            $scope.searchInfo.paginator.PageIndex = index;
            $scope.getUserList();
        },
        changePageSize:function (pagesize) {
            $scope.searchInfo.paginator.PageSize = pagesize;
            $scope.getUserList();
        }

    }
    //用户登录信息
    $scope.userinfo = [];
    //查询条件数据
    $scope.searchModel = {};
    // 查询条件对象
    $scope.searchInfo = {
        AgencyShop:"",
        Filiale:"",
        Reporter:"",
        Paginator:{
            PageIndex:CONSTANT.PAGINATOR_INFO.CURRENT_INDEX,
            PageSize:CONSTANT.PAGINATOR_INFO.CURRENT_PAGE_SIZE
        }
    };
    //设置分页的参数
    $scope.option = {
        config:{
            curr: 1,  //当前页数
            totalcount: 1,  //总页数
            maxpage:3,//最多显示页码按钮数量
            currsize:10,//最多显示的页数，默认为10
            pagesize:[10,50,100]
        },
        //点击页数的回调函数，参数page为点击的页数
        click: function (page) {
            //这里可以写跳转到某个页面等...
            //设置分页码和页数
            $scope.option.refresh();
        },
        changesize:function (size) {
            $scope.option.refresh();
        },
        refresh:function () {
            $scope.option.updatePaginator();
            $scope.getUserList();
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
        var search = dataService.get(CONSTANT.USER_SEARCH_INFO);
        if(search){
            //从缓存中获取查询条件基础数据,没有则重新从服务器获取
            $scope.searchInfo = search;
        }
        //分页控件配置信息
        search = dataService.get(CONSTANT.USER_PAGINATOR_CONFIG);
        if(search){
            //从缓存中获取查询条件基础数据,没有则重新从服务器获取
            $scope.option.config = search;
        }
        //查询基础数据
        search = dataService.get(CONSTANT.USER_SEARCH_BASE);
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
        dataService.set(CONSTANT.USER_SEARCH_INFO,$scope.searchInfo);
        dataService.set(CONSTANT.USER_PAGINATOR_CONFIG,$scope.option.config);
    }
    //更新基础数据缓存
    $scope.updateSearchBase = function () {
        dataService.set(CONSTANT.USER_SEARCH_BASE,$scope.searchModel);
    }
    //异步回调更新状态 防止频繁后台ajax调用
    $scope.changeState = function (state) {
        $scope.ajaxComplete = state;
    }
    // *******************************辅助功能性方法区域******结束********************************
    // ------------------------------------------------------------------------------------------
    // -----------------------------------分割线-------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // ******************************.视图业务控制接口区域******开始******************************
    $scope.getUserList = function () {
        //每次查询前判断验证用户是否过期
        $scope.isLoginIn();
        if(!$scope.ajaxComplete) return;
        $scope.changeState(false);
        $http({
            method:'post',
            url: CONSTANT.URL_LIST.WCF + 'user/getuserlist',
            data:{
                account:$scope.userinfo.account,
                token:$scope.userinfo.token,
                search_info:$scope.searchInfo
            }
        }).then(function (res) {
            $scope.updateSearchInfo();
            if(res.data.return_flag==0){
                // 获取特巡报告列表
                if(angular.isObject(res.data)){
                    $scope.userlist = res.data.user_list;
                    //更新分页
                    $scope.option.config.totalcount = res.data.count;
                    $scope.option.updatePaginator();
                    //将查询条件写入缓存中以便下次使用
                    $scope.updateSearchInfo();
                    //通知主页面完成
                    $scope.changeState(true);
                }
            }else{
                //没有获取到数据
                //$scope.userlist = [];
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
        //用户检索条件中代理店列表和分公司列表可以从特巡条件中基础数据中获取
        $http({
            method:'post',
            url:CONSTANT.URL_LIST.WCF + 'user/getuserbase'
        }).then(function (res) {

            $scope.searchModel = res.data.userbase;
            if(res.data.return_flag==0){
                if(angular.isObject(res.data.userbase)){
                    $scope.searchModel = res.data.userbase;
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
        $scope.getUserList();
    }
    // *******************************控制器初始化区域******结束**********************************

    //启动函数
    $scope.Init();
});