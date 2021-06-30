(function () {
    var app = angular.module('app', ["ngStorage"]);

    app.controller('accountController', ['$scope', '$http', '$localStorage', '$sessionStorage', '$window', function (scope, http, local, session, window) {
        loginUrl = "/api/account/login";
        local.config = {};

        scope.user = local.user;

        scope.dontEdit = true;

        scope.backgroundImage = "data:" + local.backgroundType + ";base64," + local.backgroundImage;

        scope.loggedIn = function () {
            if (scope.user != null) {
                local.config = {
                    headers: {
                        'Authorization': 'Bearer ' + scope.user.token
                    }
                };
                return true;
            }
            else {
                return false;
            }
        };

        scope.redirectLogin = function () {
            if (scope.loggedIn() &&
                (
                    window.location.pathname == "/Account/Login" ||
                    window.location.pathname == "/Account/login" ||
                    window.location.pathname == "/account/login" ||
                    window.location.pathname == "/account/Login"
                ))
                window.location.href = '/Conta';
            else if (!scope.loggedIn())
                if (
                    window.location.pathname == "/Account/Login" ||
                    window.location.pathname == "/Account/login" ||
                    window.location.pathname == "/account/login" ||
                    window.location.pathname == "/account/Login"
                )
                    return;
                else
                    window.location.href = '/Account/Login';
        };

        scope.accessAccount = function () {

            http.post(loginUrl + "?login=" + scope.login + "&pass=" + scope.pass)
                .then(function (r) {
                    local.user = r.data;
                    scope.loggedIn();
                    window.location.href = '/Account';
                })
                .catch(function () {
                    alert("User or Password are incorrect!");
                });
        };

        scope.verifyAuth = function () {
            if (scope.loggedIn())
                http.get("/api/account/verify-auth", local.config)
                    .then(
                        function () {

                        })
                    .catch(
                        function () {
                            alert("Your session has expired, please login again.")
                            local.user = null;
                            scope.user = null;
                            scope.redirectLogin();
                        });
        };

        scope.saveChanges = function () {
            scope.verifyAuth();

            http.put("api/account/update", scope.user, local.config)
                .then(function (r) {
                    local.user = r.data;
                    scope.user = r.data;

                })
                .catch(function () {
                    alert("There was a problem with the request, if the error persists please contact the developer.");
                });
        };

        scope.newUser = function () {
            if (scope.user.password != scope.user.passwordConfirm) {
                alert("Passwords don't match!");
                return;
            }
            else {
                http.post("api/account/create", scope.user)
                    .then(function () {
                        window.location.href = '/Account/Login';
                        alert("User created succefull, please login. ");

                    })
                    .catch(function () {
                        alert("There was a problem with the request, if the error persists please contact the developer.");
                    });
            }
        };

        scope.logout = function () {

            local.user = null;
            scope.user = null;
            scope.redirectLogin();

        };

        scope.changePass = function () {
            scope.verifyAuth();

            if (scope.newPass != scope.newPassCheck)
                alert("Passwords don't match!");
            else {
                http.put("/api/account/change-pass?login=" + scope.user.login + "&pass=" + scope.currentPassword + "&newPass=" + scope.newPass, null, local.config)
                    .then(function () {
                        alert("Password changed successfully, please login again.")
                        scope.logout();
                    })
                    .catch(function () {
                        alert("There was a problem with the request, check if the password are correctly and try again, if the error persists please contact the developer.");
                    });
            }

        };

        scope.uploadBackground = function () {
            var f = document.getElementById('backgroundImage').files[0],
                r = new FileReader();

            r.onloadend = function (e) {
                var data = e.target.result;

                var imagem = {
                    backGround: true,
                    name: f.name,
                    base64: data
                };

                http.post("/api/file/upload-image", imagem, local.config)
            }

            r.readAsDataURL(f);
        };

        scope.getBackground = function () {
            if (local.backgroundImage == null) {

                if (local.background != null)
                    scope.background = local.background;
                else {
                    http.get("api/file/get-background", local.config)
                        .then(function (r) {
                            local.backgroundImage = r.data.base64;
                            local.backgroundType = r.data.type;
                            scope.backgroundImage = "data:" + local.backgroundType + "; base64," + local.backgroundImage;
                        });
                }
            }
        };

        scope.changeBackground = function () {
            var f = document.getElementById('backgroundImage').files[0],
                r = new FileReader();

            r.onloadend = function (e) {
                var data = e.target.result;

                scope.backgroundImage = data;
            }

            r.readAsDataURL(f);
        };

        scope.pageNewAccount = function () {
            window.location.href = '/account/new';
        };

        scope.getBackground();
    }]);

    app.directive('dateInput', function () {
        return {
            restrict: 'A',
            scope: {
                ngModel: '='
            },
            link: function (scope) {
                if (scope.ngModel) scope.ngModel = new Date(scope.ngModel);
            }
        }
    })
})();