(function () {
    'use strict';

    var app = angular.module('pokemonShopModule', ['ngRoute', 'ui.mask']);

    app.config(
    ['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/', {
            title: 'Форма заказа - Интернет-магазин Покемонов',
            templateUrl: '/templates/order/order.html',
            controller: 'OrderController'
        });
        $routeProvider.when('/order-history', {
            title: 'Лента - Интернет-магазин Покемонов',
            templateUrl: '/templates/orderHistory/orderHistory.html',
            controller: 'OrderHistoryController'
        });
    }]);

    app.run(['$rootScope', function ($rootScope) {
        $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
            $rootScope.pageTitle = current.$$route.title;
        });
    }]);
})();