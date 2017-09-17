(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var GameCreateController = function ($scope, $http, $rootScope, $location, $timeout, Notification) {
        $scope.message = "I'm working!";
        $scope.gamesystems = [];
        $scope.games = [];

        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;
        $scope.isSystemSelected = false;
        $scope.isSystemLoaded = false;


        $scope.game = {
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

        $scope.checkIfSystemSelected = function () {
            $scope.isSystemSelected = true;
        };

        $scope.create = function () {

            $scope.isLoading = true;
            $http({
                method: 'POST',
                url: 'api/Game/CreateNew',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: $scope.game

            }).then(function success(response) {
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                Notification.success('Game created!');

                $rootScope.game = $scope.game;

                $timeout(function () {
                    $location.path("/game");
                }, 700);

            }, function error(response) {
                $scope.onError = true;
                $scope.isLoading = false;
            });

        };

    };

    RPGSupportApp.controller("GameCreateController", GameCreateController);

}());