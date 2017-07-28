/**
 * Created by liuchengyun on 2017/7/12.
 */
var express = require('express');
var http = require('http');

var wcf = function () {
        const postData = {
            'token' : 'Hello World!'
        };

        const options = {
            hostname: 'http://localhost',
            port: 8732,
            path: '/PatrolServices/Patrol/show',
            method: 'POST'
        };
        var reqdata;
        const r = http.request(options, function (res) {
            res.on('data',function (data) {
                if(data){
                    reqdata += data;
                }
            });
            res.on('end',function () {
                if(reqdata){
                    callback(reqdata);
                }
            });
        });
        r.on('error',function (e) {
            console.log("错误信息");
        });

        function callback(reqdata) {
            res.send(reqdata);
        }
}

module.exports = wcf;