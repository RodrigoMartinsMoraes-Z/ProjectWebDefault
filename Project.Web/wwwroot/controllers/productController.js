(function () {
    var app = angular.module('app');

    app.controller('productController', ['$scope', '$http', '$localStorage', '$window', function (scope, http, local, window) {

        scope.categoryRegistred = false;
        scope.product = {};
        scope.product.images = [];
        scope.product.imagesCount = 0;
        scope.take = 10;
        scope.skip = 0;

        scope.order = {
            name: false,
            price: false,
            category: 0
        }

        if (local.cart == null)
            local.cart = [];

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

        scope.orderByCategory = function (cat) {
            scope.order.category = cat;
            scope.listProducts();
        };

        scope.orderByName = function () {
            scope.order.name = true;
            scope.order.price = false;
            scope.listProducts();
        };

        scope.orderByPrice = function () {
            scope.order.name = false;
            scope.order.price = true;
            scope.listProducts();
        };

        scope.listProducts = function () {
            http.get("api/product/list?name="+scope.order.name+"&price="+scope.order.price+"&categoryId="+scope.order.category)
                .then(function (r) {
                    scope.products = r.data;
                });
        };

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

        scope.addProductInCart = function (product) {
            if (local.cart.includes(product)) {
                index = local.cart.indexOf(product);
                local.cart[index].amount += product.amount;
            }
            else
                local.cart.push(product);
        };

        scope.removeFromCart = function (p) {
            local.cart = local.cart.filter(product => product.id != p.id)
            scope.cart = local.cart;
        };

        scope.getCart = function () {
            scope.cart = local.cart;
        };
    }]);

    app.directive('currency', function () {
        return {
            require: 'ngModel',
            link: function (elem, $scope, attrs, ngModel) {
                ngModel.$formatters.push(function (val) {
                    return '$' + val
                });
                ngModel.$parsers.push(function (val) {
                    return val.replace(/^\$/, '')
                });
            }
        }
    })

})();