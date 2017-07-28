var express = require('express');
var queryString = require('querystring');
var fs = require('fs');
var http = require('http');
var router = express.Router();


//html5mode模式F5刷新路由配置
router.get('/', function(req, res, next) {
    var datapath = process.cwd() + '/app/index.html';
    res.header("content-type","text/html");
    res.sendFile(datapath);
});

//html5mode模式F5刷新路由配置
router.get('/edit/:id', function(req, res, next) {
    var datapath = process.cwd() + '/app/index.html';
    res.header("content-type","text/html");
    res.sendFile(datapath);
});

//html5mode模式F5刷新路由配置
router.get('/report/:id', function(req, res, next) {
    var datapath = process.cwd() + '/app/index.html';
    res.header("content-type","text/html");
    res.sendFile(datapath);
});

/* GET users listing. */
router.post('/', function(req, res, next) {
    function sendPatrolList(data) {
        res.send(data);
    }
    var account = req.body.account;
    var token = req.body.token;
    console.log(account + token);
    if(account!="" && token !=""){
        var req1 = http.request({
            hostname:'localhost',
            port:8732,
            method:'POST',
            path:'/PatrolService/Patrol/list'
        },function (res1) {
            var databuffer="";
            res1.on('data',function (data) {
                databuffer +=data;
            });
            res1.on('end',function () {
                //console.log(databuffer);
                sendPatrolList(databuffer);
                //res.send("[12,45,45,45,456,456]");
            });
        });
        req1.end();
        req1.on('error',function (e) {
            console.log(e);
        });
    }else{
        res.send({code:4000,message:"用户会话过期",token:''});
        res.end();
    }
});

//取得所有特巡报告列表
router.get('/',function (req,res,next) {
    //console.log(123444);
    function sendPatrolList(data) {
        res.send(data);
    }
    if(req.params.id==""){
        Console.log(req.params.id);
    }else{

        var req1 = http.request({
            hostname:'localhost',
            port:8732,
            method:'POST',
            path:'/PatrolService/Patrol/list'
        },function (res1) {
            var databuffer="";
            res1.on('data',function (data) {
                databuffer +=data;
            });
            res1.on('end',function () {
                //console.log(databuffer);
                sendPatrolList(databuffer);
                //res.send("[12,45,45,45,456,456]");
            });
        });
        req1.end();
        req1.on('error',function (e) {
            console.log(e);
        });
    }
});

module.exports = router;
