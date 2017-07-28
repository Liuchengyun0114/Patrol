var express = require('express');
var router = express.Router();
var tokenlist = [];//登录列表控制
var tokentime = 1000 * 60 * 30;//token有效期 毫秒计数

/* GET home page. */
router.post('/', function(req, res, next) {
    // 获得账号密码信息
    var account = req.body.account;
    var password = req.body.password;
    console.log(req.headers);
    if(account=="test123" && password=="test888888"){
        var tokeninfo = {};
        tokeninfo.account = account;
        tokeninfo.tokenInvalid = new Date().getTime() + tokentime;
        tokeninfo.token = Math.random().toString().substr(2);
        tokenlist.push(tokeninfo);
        console.log(tokenlist);
        res.send({code:200,message:"验证成功",token:tokeninfo.token});
        res.end();
    }else{
        // 验证失败发送错误信息
        res.send({code:4000,message:"账号或者密码错误"});
        res.end();
    }
});

router.post('/validate', function(req, res, next) {
    // 获得账号密码信息
    var account = req.body.account;
    var token = req.body.token;
    var success = false;
    if(account && token) {
        for (var i = 0; i < tokenlist.length; i++) {
            if (tokenlist[i].account == account && tokenlist[i].token == token) {
                //判断是否过期
                var tokenDate = tokenlist[i].tokenInvalid;
                if (tokenDate < new Date().getTime()) {
                    Console.log("token失效");
                }
                //生成新的token延迟token失效时间
                tokenlist[i].token = Math.random().toString().substr(2);
                tokenlist[i].tokenInvalid = new Date().getTime() + tokentime;
                token = tokenlist[i].token;
                success = true;
            }
        }
    }
    if(success){
        res.send({code:200,message:"验证成功",token:token});
        res.end();
    }else{
        // 验证失败发送错误信息
        res.send({code:4000,message:"用户会话超时,请重新登录"});
        res.end();
    }
});



router.get('/', function(req, res, next) {
    var pathinfo = process.cwd() + "/app/index.html";
    res.sendFile(pathinfo);
});


module.exports = router;
