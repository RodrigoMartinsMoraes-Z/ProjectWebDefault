(function () {
    var app = angular.module('app');

    app.controller('productController', ['$scope', '$http', '$localStorage', '$window', function (scope, http, local, window) {

        scope.catCadastrada = false;
        scope.product = {};
        scope.product.images = [];
        scope.product.imagesCount = 0;
        scope.take = 10;
        scope.skip = 0;

        scope.carregarProduto = function () {
            http.get("api/product/" + model, local.config)
                .then(function (r) {
                    scope.product = r.data;
                })
        };

        scope.novaCategoria = function () {
            http.put("/api/category/", scope.newCategory, local.config)
                .then(function () {
                    scope.newCategory = {};
                    scope.catCadastrada = true;
                })
                .catch(function () {
                    alert("Categoria já cadastrada!");
                    scope.catCadastrada = false;
                });
        };

        scope.buscarCategorias = function () {
            http.get("/api/category/", local.config)
                .then(function (r) {
                    scope.categories = r.data;
                });
        };

        scope.cadastrarProduto = function () {
            http.put("/api/product/update", scope.product, local.config)
                .then(function () {

                })
                .catch(function () {

                });
        };

        scope.novoProduto = function () {
            window.location.href = '/produto';
        };

        scope.detalhesProduto = function (id) {
            window.location.href = '/produto/' + id;

        };

        scope.uparImagem = function () {
            var f = document.getElementById('imagemDoProduto');

            var i = 0;
            for (i = 0; i < f.files.length; i++) {
                var file = f.files[i];

                r = new FileReader();

                r.onloadend = function (e) {
                    var data = e.target.result;

                    var image = {
                        backGround: false,
                        name: file.name,
                        base64: data
                    };

                    scope.product.images.push(image);
                    scope.product.imagesCount = scope.product.imagesCount + 1;
                }

                r.readAsDataURL(file);
            }

        };

        scope.deletarImagem = function (id) {
            http.delete("api/file/delete-image/" + id, local.config)
                .then(function () {
                    scope.product.images = scope.product.images.filter(image => image.id != id);
                });
        };

        scope.teste = function (id) {
            console.log(id);
            scope.product.images = scope.product.images.filter(image => image.id != id);
        };

        scope.listarProdutos = function () {
            http.get("api/product/list?take=" + scope.take + "&skip=" + scope.skip)
                .then(function (r) {
                    scope.products = r.data;
                });
        }

        scope.imagemAnterior = function (min, imageIndex) {
            if (imageIndex > min)
                return imageIndex - 1;
            return imageIndex;+-..
        };

        scope.proximaImagem = function (max, imageIndex) {
            if (imageIndex < max - 1)
                return imageIndex + 1;
            return imageIndex;
        };
    }]);

})();