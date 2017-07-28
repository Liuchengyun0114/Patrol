/**
 * Created by liuchengyun on 2017/7/17.
 */
angular.module('myPagination', []).directive('myPagination', function () {
    return {
        restrict: 'EA',
        replace: true,
        scope: {
            option: '=pageOption',
        },
        template:
        '<div ng-hide="{{option.config.totalcount<=0}}" class="col-xs-12 center-block">' +
            '<ul class="pagination col-xs-3">' +
                '<li ng-click="pagesizeClick(s)" ng-repeat="s in pagesize" class="{{option.currsize==s?\'active\':\'\'}}">' +
                '<a href="javascript:;">{{s}}</a>' +
                '</li>' +
            '</ul>' +
            '<ul class="pagination col-xs-6">' +
                '<li ng-click="pageClick(p)" ng-repeat="p in page" class="{{option.curr==p?\'active\':\'\'}}">' +
                '<a href="javascript:;">{{p}}</a>' +
                '</li>' +
            '</ul>' +
            '<ul class="pagination col-xs-3">' +
                '<li class="text-justify">' +
                '   <div class="input-group">' +
                '       <span class="input-group-addon">共{{totalcount}}页</span>' +
                '       <input type="text" class="form-control text-primary" ng-model="pageindex">' +
                '       <button class="btn btn-danger input-group-addon" ng-click="gotoPage(pageindex)">' +
                '           <span class="glyphicon glyphicon-hand-right">' +
                '           </span>' +
                '       </button>' +
                '   </div>' +
                '</li>' +
            '</ul>' +
        '</div>',
        link: function ($scope) {
            var config = {
                curr: 1,  //当前页数
                totalcount: 0, //总记录数
                maxpage:1,//最多显示页码按钮数量
                currsize:10,//最多显示的页数，默认为10
                pagesize:[10,50,100]
            };
            $scope.$watch("option.config.totalcount",function () {
                $scope.page = getRange($scope.option.config);
                $scope.totalcount = changeCount();
            });
            function checkData() {
                if(!angular.isObject($scope.option.config)){
                    $scope.option.config = config;
                }else{
                    var con = $scope.option.config;
                    //容错处理
                    if(!con.curr||isNaN(con.curr)||con.curr<1) con.curr = config.curr;
                    if(!con.totalcount||isNaN(con.totalcount)||con.totalcount<1) con.totalcount = config.totalcount;
                    if(!con.maxpage||isNaN(con.maxpage)||con.maxpage<1) con.maxpage = config.maxpage;
                    if(!con.currsize||isNaN(con.currsize)||con.currsize<1) con.currsize = config.currsize;
                    if(!con.pagesize||!angular.isArray(con.pagesize)) con.pagesize = config.pagesize;
                    $scope.option.config = con;
                }
            }
            //数据检查
            checkData();

            //总页数
            $scope.totalcount = changeCount();

            //得到显示页数的数组
            $scope.page = getRange($scope.option.config);

            //得到显示单页数量的数组
            $scope.pagesize = getPageSize($scope.option.config.pagesize);

            //跳转指定页码
            $scope.gotoPage = function(pageindex) {
                if(!pageindex || isNaN(pageindex) || pageindex < 1 || pageindex > $scope.option.config.totalcount) return;
                $scope.pageClick( pageindex);
            }

            //每页显示数量绑定点击事件
            $scope.pagesizeClick = function (pagesize) {
                //点击相同的页数 不执行点击事件
                if (pagesize == $scope.option.config.currsize) return;
                if ($scope.option.changesize && typeof $scope.option.changesize === 'function') {
                    var currtotal = $scope.option.config.currsize * $scope.option.config.curr; // 获取转换前页码包含记录总数
                    $scope.option.config.currsize = pagesize;
                    $scope.option.config.curr = parseInt(currtotal/pagesize) + ((currtotal % pagesize> 0) ? 1: 0);
                    $scope.page = getRange($scope.option.config);
                    $scope.totalcount =changeCount();
                    //调用外部处理程序
                    $scope.option.changesize(pagesize);
                }
            };

            //绑定点击事件
            $scope.pageClick = function (page) {
                var totalpage = parseInt($scope.option.config.totalcount/$scope.option.config.currsize) + (($scope.option.config.totalcount % $scope.option.config.currsize) > 0 ? 1 :0);
                if (page == '<<') {
                    page = 1;
                } else if (page == '>>') {
                    page = totalpage;
                }
                if (page == '«') {
                    page = parseInt($scope.option.config.curr) - 1;
                } else if (page == '»') {
                    page = parseInt($scope.option.config.curr) + 1;
                }
                if (page < 1) page = 1;
                else if (page > totalpage) page = totalpage;
                //点击相同的页数 不执行点击事件
                if (page == $scope.option.config.curr) return;
                if ($scope.option.click && typeof $scope.option.click === 'function') {
                    $scope.option.config.curr = page;
                    $scope.page = getRange($scope.option.config);
                    //调用外部处理程序
                    $scope.option.click(page);
                }
            };
            //重新计算总页数
            function changeCount() {
                var total = parseInt($scope.option.config.totalcount / $scope.option.config.currsize) + (($scope.option.config.totalcount % $scope.option.config.currsize > 0 ) ? 1:0);
                return total;
            }
            //返回页数范围（用来遍历）
            function getRange(opt) {
                //计算显示的页数
                var curr = parseInt(opt.curr);
                var totalpage = parseInt(opt.totalcount/opt.currsize) + ((opt.totalcount % opt.currsize > 0) ? 1 : 0);
                var maxpage = parseInt(opt.maxpage);
                maxpage = totalpage > opt.maxpage ? opt.maxpage : totalpage;

                var from = curr - parseInt(maxpage / 2);
                var to = curr + parseInt(maxpage / 2) + (maxpage % 2) - 1;
                //显示的页数容处理
                if (from <= 0) {
                    from = 1;
                    to = from + maxpage - 1;
                }
                if (to > totalpage) {
                    to = totalpage;
                    from = to - maxpage + 1;
                    if (from <= 0) {
                        from = 1;
                    }
                }
                var range = [];
                for (var i = from; i <= to; i++) {
                    range.push(i);
                }
                range.push('»');
                range.push('>>');
                range.unshift('«');
                range.unshift('<<');
                return range;
            }
            //返回每页显示数量范围
            function getPageSize(arraysize) {
                var range = [10,50,100];
                if(angular.isArray(arraysize)){
                    range = arraysize;
                }
                return range;
            }
        }
    }
});