(function () {
    var app = angular.module('app');

    app.controller('productController', ['$scope', '$http', '$localStorage', '$window', function (scope, http, local, window) {

        scope.categoryRegistred = false;
        scope.product = {};
        scope.product.images = [];
        scope.product.imagesCount = 0;
        scope.take = 10;
        scope.skip = 0;

        scope.getProduct = function () {
            http.get("api/product/" + model, local.config)
                .then(function (r) {
                    scope.product = r.data;
                })
        };

        scope.newCategory = function () {
            http.put("/api/category/", scope.category, local.config)
                .then(function () {
                    scope.category = {};
                    scope.categoryRegistred = true;
                    scope.getCategories();
                })
                .catch(function () {
                    alert("Categoria já cadastrada!");
                    scope.categoryRegistred = false;
                });
        };

        scope.getCategories = function () {
            http.get("/api/category/", local.config)
                .then(function (r) {
                    scope.categories = r.data;
                });
        };

        scope.newProduct = function () {
            http.put("/api/product/update", scope.product, local.config)
                .then(function () {

                })
                .catch(function () {

                });
        };

        scope.newProductPage = function () {
            window.location.href = '/product';
        };

        scope.productDetailsPage = function (id) {
            window.location.href = '/product/' + id;

        };

        scope.loadImage = function () {
            var f = document.getElementById('productImage');

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

        scope.deleteImage = function (id) {
            http.delete("api/file/delete-image/" + id, local.config)
                .then(function () {
                    scope.product.images = scope.product.images.filter(image => image.id != id);
                });
        };

        scope.listProducts = function () {
            http.get("api/product/list?take=" + scope.take + "&skip=" + scope.skip)
                .then(function (r) {
                    scope.products = r.data;
                });
        }

        scope.previousImage = function (min, imageIndex) {
            if (imageIndex > min)
                return imageIndex - 1;
            return imageIndex;
        };

        scope.nextImage = function (max, imageIndex) {
            if (imageIndex < max - 1)
                return imageIndex + 1;
            return imageIndex;
        };
    }]);

})();