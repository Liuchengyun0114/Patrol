/**
 * Created by liuchengyun on 2017/5/24.
 */
    // 定义模块和引用模块
var app = angular.module("myApp",['ngRoute','ngResource','ngCookies','ui.bootstrap','dataService','myPagination']);
app.constant("CONSTANT",{
    //基础地址 + 路由地址 + 数据服务地址
    URL_LIST : {
        BASE:'http://172.18.1.42:3000',
        LOGIN: 'views/login.html',
        NAVBAR: 'views/navbar.html',
        LIST: 'views/list.html',
        WCF:'http://172.18.1.42:8731/PatrolService/'
    },
    //cookie存储用户信息变量名称
    USER_INFO:"USER_INFO",
    //分页器默认初始化信息
    PAGINATOR_INFO:{
        CURRENT_INDEX:1,
        CURRENT_PAGE_SIZE:10
    },
    //分页控件配置信息缓存变量
    PATROL_PAGINATOR_CONFIG:"PATROL_PAGINATOR_CONFIG",
    //分页控件配置信息缓存变量
    USER_PAGINATOR_CONFIG:"USER_PAGINATOR_CONFIG",
    //特巡一览查询条件 存储变量名称
    PATROL_SEARCH_INFO:"PATROL_SEARCH_INFO",
    //特巡一览查询条件基础数据
    PATROL_SEARCH_BASE:"PATROL_SEARCH_BASE",
    //用户一览查询条件 存储变量名称
    USER_SEARCH_INFO:"USER_SEARCH_INFO",
    //特巡一览查询条件 存储变量名称
    USER_SEARCH_BASE:"USER_SEARCH_BASE",
    //用户登录有效期时长 毫秒计数
    LIFE_TIME:30 * 60 * 1000
});