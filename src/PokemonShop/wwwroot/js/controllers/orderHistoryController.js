var app = angular.module('pokemonShopModule');

app.controller('OrderHistoryController', [
    '$scope', 'orderHistoryService',
    function ($scope, orderHistoryService) {

        $scope.orderHistoryItems = [];

        $scope.loadOrderHistoryItems = function() {
            orderHistoryService.getOrderHistoryItems().then(function (items) {
                $scope.orderHistoryItems = items;
            });
        };

        $scope.loadOrderHistoryItems();

        $scope.getHowManyTimesWord = function(count)
        {
            var modulo = count % 10;

            return (modulo > 1 && modulo < 5 && (count < 10 || count > 20)) ? 'раза' : 'раз';
        }

    }]);
