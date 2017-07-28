        /**
         * Created by liuchengyun on 2017/5/24.
         */
        //登录控制器
        app.controller("loginController",function ($scope,$http,$location,$cookieStore,CONSTANT) {
            $scope.loginModel = {
                isAccount:false,
                isPassword:false,
                isCompleted:false,
                isError:false,
                isValidated:false,
                message:""
            };
            $scope.login = function(){
                if($scope.loginModel.isCompleted){
                    $http({
                        method:'post',
                        url: CONSTANT.URL_LIST.WCF + 'user/loginCheck',
                        data:{
                            user_id: $scope.user.account,
                            password: $scope.user.pwd
                        }
                    }).then(function (res) {
                        //$scope.loginModel.message = res.data;
                        if(res.data.return_flag == 0){
                            // 验证成功登陆主界面
                            $scope.setCookie(res.data.token);
                            //跳转主页面
                            $location.path('/patrol').replace();
                        }else{
                            $scope.loginModel.message = "账号或者密码错误";
                            $scope.reset();
                        }
                    });
                }
            }
            $scope.reset = function () {
                //$scope.account = "";
                $scope.user.pwd = "";
            }
            $scope.validateAccount = function () {
                var value = $scope.user.account;
                var reg = /^[a-zA-Z][a-zA-Z0-9_]{4,15}$/
                $scope.loginModel.isAccount = value.search(reg) > -1;
                $scope.loginModel.isCompleted = $scope.loginModel.isAccount && $scope.loginModel.isPassword;
                return $scope.loginModel.isAccount;
            }
            $scope.validatePassword = function () {
                var value = $scope.user.pwd;
                var reg = /^[a-zA-Z][a-zA-Z0-9_]{5,17}$/
                $scope.loginModel.isPassword = value.search(reg) > -1;
                $scope.loginModel.isCompleted = $scope.loginModel.isAccount && $scope.loginModel.isPassword;
                return  $scope.loginModel.isPassword;
            }
            //监视输入合法性
            $scope.$watch("loginModel.isCompleted",function (isright) {
                if(!isright){
                    $scope.loginModel.message = "账号或者密码输入不符合规范" + isright;
                }else{
                    $scope.loginModel.message = "合法输入验证成功";
                }
            });
            //默认有输入时候执行一次检查
            $scope.validateAccount();
            $scope.validatePassword();
            //浏览器地址栏信息设置
        });




