var app = angular.module("app", ["ngRoute"]);

(function () {
    app.config(function ($routeProvider) {
        $routeProvider
            .when("/login", {
                templateUrl: "Templates/Login/index.html"
            })
            .otherwise({
                templateUrl: "Templates/Home/index.html"
            });

    });
    app.controller('loginController', ['$scope', '$http', function (scope, http) {
        apiUrl = "/api/account/login";
        scope.controller = "loginController";

        scope.login = function () {
            user = {
                login: scope.userName,
                password: scope.password
            };
            http.post(apiUrl + "?login=" + scope.userName + "&pass=" + scope.password)
                .then(function () {
                    scope.log("login succesfull!")
                });
        };

    }]);
})();