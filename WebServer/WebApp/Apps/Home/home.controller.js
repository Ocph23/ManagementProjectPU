angular.module("home.controllers", [])
    .controller('LoginController', LoginController)
  
    .controller('RegisterController', RegisterController)
    ;

function LoginController($scope, AuthServices) {
 //   sessionStorage.clear();
    $scope.Login = function (model) {
        AuthServices.login(model);
    };
}

function RegisterController($scope, AuthServices) {
    $scope.model = {};
    $scope.register = function (model) {
        AuthServices.register(model).then(function (response) {
            $scope.model = {};
        });
    };
}
