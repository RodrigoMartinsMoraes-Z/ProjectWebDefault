(function () {
    var app = angular.module('app', ["ngStorage"]);

    app.controller('contaController', ['$scope', '$http', '$localStorage', '$sessionStorage', '$window', function (scope, http, local, session, window) {
        loginUrl = "/api/account/login";
        local.config = {};

        scope.user = local.user;

        scope.naoEditar = true;

        scope.backgroundImage = "data:" + local.backgroundType + ";base64," + local.backgroundImage;

        scope.estaLogado = function () {
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
        }

        scope.direcionarLogin = function () {
            if (scope.estaLogado() &&
                (
                    window.location.pathname == "/Conta/Login" ||
                    window.location.pathname == "/Conta/login" ||
                    window.location.pathname == "/conta/login" ||
                    window.location.pathname == "/conta/Login"
                ))
                window.location.href = '/Conta';
            else if (!scope.estaLogado())
                if (
                    window.location.pathname == "/Conta/Login" ||
                    window.location.pathname == "/Conta/login" ||
                    window.location.pathname == "/conta/login" ||
                    window.location.pathname == "/conta/Login"
                )
                    return;
                else
                    window.location.href = '/Conta/Login';
        }

        scope.logar = function () {

            http.post(loginUrl + "?login=" + scope.login + "&pass=" + scope.pass)
                .then(function (r) {
                    console.log("login succesfull!");
                    local.user = r.data;
                    scope.estaLogado();
                    window.location.href = '/Conta';
                })
                .catch(function () {
                    alert("Usuario ou senha incorreto!");
                });
        };

        scope.verificarAutenticacao = function () {
            if (scope.estaLogado())
                http.get("/api/account/verify-auth", local.config)
                    .then(
                        function () {

                        })
                    .catch(
                        function () {
                            alert("Sua sessão expirou, favor entrar novamente.")
                            local.user = null;
                            scope.user = null;
                            scope.direcionarLogin();
                        });
        }

        scope.salvar = function () {
            scope.verificarAutenticacao();

            http.put("api/account/update", scope.user, local.config)
                .then(function (r) {
                    local.user = r.data;
                    scope.user = r.data;

                })
                .catch(function () {
                    alert("Ocorreu um problema com a requisição, se o erro persistir entre em contato com o desenvolvedor.");
                });
        }

        scope.sair = function () {

            local.user = null;
            scope.user = null;
            scope.direcionarLogin();

        }

        scope.alterarSenha = function () {
            scope.verificarAutenticacao();

            if (scope.novaSenha != scope.novaSenhaConfirma)
                alert("As senhas não conferem!");
            else {
                http.put("/api/account/change-pass?login=" + scope.user.login + "&pass=" + scope.senhaAtual + "&newPass=" + scope.novaSenha, null, local.config)
                    .then(function () {
                        alert("Senha alterada com sucesso, favor realizar login novamente.")
                        scope.sair();
                    })
                    .catch(function () {
                        alert("Ocorreu um problema inesperado, verifique se a senha digitada está correta e tente novamente, se o problema persistir entre em contato com o desenvolvedor.");
                    });
            }

        };

        scope.uparImagemDeFundo = function () {
            var f = document.getElementById('imagemDeFundo').files[0],
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

        scope.buscarBackGround = function () {
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

        scope.trocarFundo = function () {
            var f = document.getElementById('imagemDeFundo').files[0],
                r = new FileReader();

            r.onloadend = function (e) {
                var data = e.target.result;

                scope.backgroundImage = data;
            }

            r.readAsDataURL(f);
        }

        scope.buscarBackGround();
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