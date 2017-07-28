var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function(req, res, next) {
    //console.log(req.headers);
    var datapath = process.cwd() + '/app/index.html';
    res.header("content-type","text/html");
    res.sendFile(datapath);
});

router.get('/4567', function(req, res, next) {
    //console.log(req.headers);
    var datapath = process.cwd() + '/app/index.html';
    res.header("content-type","text/html");
    res.sendFile(datapath);
});
module.exports = router;
