var app = angular.module('pokemonShopModule');

app.factory('orderHistoryService', function ($http, $q) {
    var service = {};

    service.getOrderHistoryItems = function () {
        var deferred = $q.defer();

        $http({
                method: 'GET',
                url: '/api/order'
            })
            .then(
                function(result, status, headers, config) {
                    deferred.resolve(result.data);
                },
                function (result, status, headers, config) {
                    deferred.reject(status);
                });

        return deferred.promise;
    }

    return service;
})