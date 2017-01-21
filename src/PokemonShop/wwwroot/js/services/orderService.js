var app = angular.module('pokemonShopModule');

app.factory('orderService', function ($http, $q) {
    var service = {};

    service.makeOrder = function (orderDetails) {
        var deferred = $q.defer();

        $http({
            method: 'POST',
            url: '/api/order',
            data: orderDetails
        })
        .then(
                function (result, status, headers, config) {
                    deferred.resolve(result.data);
                },
                function (result, status, headers, config) {
                    deferred.reject(status);
                });

        return deferred.promise;
    }

    return service;
})