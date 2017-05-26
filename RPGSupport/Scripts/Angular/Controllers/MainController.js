(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var MainController = function ($scope, $rootScope, Notification) {
        $scope.message = '';
        $scope.message = "Main Controller";

        console.log($rootScope.user);
    };

    RPGSupportApp.controller("MainController", MainController);

}());