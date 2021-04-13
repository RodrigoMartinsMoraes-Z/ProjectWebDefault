var app = angular.module('app', []);

app.controller('loginController', ['$scope','$http', function (scope, http) {

    apiUrl = "http://localhost/api/login"

    scope.login = function () {
        user = {
            login: scope.userName,
            password: scope.password
        }
        http.post(apiUrl, user)
            .then(function () {
                scope.log("login succesfull!")
            });
    };

}]);