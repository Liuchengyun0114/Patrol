/**
 * Created by liuchengyun on 2017/5/27.
 */
    //定义路由器
    app.config(['$routeProvider','$locationProvider','$httpProvider',function ($routeProvider,$locationProvider,$httpProvider) {
        $routeProvider
            .when('/',{templateUrl:'views/login.html',controller:'loginController'})
            //.when('/main',{templateUrl:'templates/template2.html'})
            .when('/patrol',{templateUrl:'/views/patrol/list.html',controller:'patrolController'})
            .when('/patrol/edit/:id',{templateUrl:'/views/patrol/detail.html',controller:'patrolEditController'})
            .when('/patrol/report/:id',{templateUrl:'/views/patrol/report.html',controller:'patrolReportController'})
            .when('/user',{templateUrl:'/views/user/list.html',controller:'userController'})
            .when('/user/edit/:id',{templateUrl:'/views/user/detail.html',controller:'userEditController'})
            .otherwise({redirectTo:'/'});
        //解决url路径乱码问题
        $locationProvider.hashPrefix('');
        //解决#号问题 配合后台路由解决F5刷新404问题
        $locationProvider.html5Mode(true);
        //ajax请求服务设置解决post提交数据问题
        $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
    }]);