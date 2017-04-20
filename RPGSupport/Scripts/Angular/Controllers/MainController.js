(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var MainController = function ($scope, $http, $rootScope) {
        $scope.message = '';


        $scope.message = "Main Controller";

 


    };

    RPGSupportApp.controller("MainController", MainController);

}());