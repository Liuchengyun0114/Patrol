/**
 * Created by liuchengyun on 2017/7/11.
 */
angular.module("dataService",[])
.service("dataService",["$window",function ($window) {
    var datalist = {};
    return {
        set:function (key,value) {
            datalist[key] = value;
        },
        get:function (key,defaultvalue) {
            return datalist[key] || defaultvalue;
        },
        setObject:function (key,value) {
            datalist[key] = JSON.stringify(value);
        },
        getObject:function (key) {
            return JSON.parse(datalist[key])||{};
        }
    }
}]);