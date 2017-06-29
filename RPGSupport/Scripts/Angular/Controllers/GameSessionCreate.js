(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var GameSessionCreateController = function ($scope, $http, $rootScope, $location, $timeout, Notification) {
        $scope.message = "I'm working!";
        $scope.gamesystems = [];
        $scope.gameSessions = [];

        $scope.gameSession = {
            Name: "",
            Description: "",
            System: "",

        };


        $scope.initController = function () {
            $http({
                method: 'GET',
                url: 'api/GameSystem/Lookup'
            }).then(function success(response) {

                $scope.gamesystems = response.data;

            }, function error(response) {
            });

        };

    };

    RPGSupportApp.controller("GameSessionCreateController", GameSessionCreateController);

}());