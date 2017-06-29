(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var GameSessionListController = function ($scope, $http, $rootScope, $location, $timeout, Notification) {
        $scope.message = "I'm working!";
        $scope.gameSessions = [];

        $scope.initController = function () {
            $http({
                method: 'GET',
                url: 'api/GameSession'
            }).then(function success(response) {

                $scope.gameSessions = response.data;
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;

            }, function error(response) {
                $scope.error = "Game Sessions not found";
                $scope.onError = true;
                $scope.isLoading = false;
            });
        };
    };

    RPGSupportApp.controller("GameSessionListController", GameSessionListController);

}());