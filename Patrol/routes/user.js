var express = require('express');
var fs = require('fs');
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
module.exports = router;
