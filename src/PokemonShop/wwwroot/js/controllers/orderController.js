var app = angular.module('pokemonShopModule');

app.controller('OrderController', [
    '$scope', 'orderService', '$location',
    function ($scope, orderService, $location) {

        // default order form data
        $scope.orderDetails = {
            userName: '',
            email: '',
            phoneNumber: ''
        };

        $scope.orderingLoaderVisible = false;

        // submit order form
        $scope.makeOrder = function (isValid) {
            if (!isValid) {
                return;
            }

            $scope.orderingLoaderVisible = true;

            // create new order
            orderService.makeOrder($scope.orderDetails).then(function (result) {
                // redirect to order history page
                $location.path('/order-history');
            });
        };

    }]);
