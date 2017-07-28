var express = require('express');

function  validate() {
    return 'helloworld';
}
function UserValidate(){
    var tokenlist = [];//登录列表控制
    var tokentime = 1000 * 60 * 30;//token有效期 毫秒计数

    //新增token
    var add = function (account,timeout) {
        timeout = timeout ? timeout : tokentime;
        var tokeninfo = {};
        tokeninfo.account = account;
        tokeninfo.tokenInvalid = new Date().getTime() + timeout;
        tokeninfo.token = Math.random().toString().substr(2);
        tokeninfo.timeout = timeout;
        tokenlist.push(tokeninfo);
    }

    //更新token
    var update = function (account) {
        var ret = null;
        for (var i = 0; i < tokenlist.length; i++) {
            if (tokenlist[i].account == account) {
                //生成新的token延迟token失效时间
                tokenlist[i].token = Math.random().toString().substr(2);
                tokenlist[i].tokenInvalid = new Date().getTime() + tokenlist[i].timeout;
                ret = tokenlist[i];
                break;
            }
        }
        return ret;
    }

    //判断是否过期
    var istimeout = function (account) {
        var success = true;
        for (var i = 0; i < tokenlist.length; i++) {
            if (tokenlist[i].account == account) {
                if(tokenlist[i].tokenInvalid > new Date().getTime()){
                    success = false;
                }
                break;
            }
        }
        return success;
    }
    //判断是否存在token
    var isvalidate = function (account) {
        var index = -1;
        for (var i = 0; i < tokenlist.length; i++) {
            if (tokenlist[i].account == account) {
                index = i;
                break;
            }
        }
        return index;
    }
};

module.exports = validate;
