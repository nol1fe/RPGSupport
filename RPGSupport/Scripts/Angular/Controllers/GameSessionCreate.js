(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var GameSessionCreateController = function ($scope, $http, $rootScope, $location, $timeout, Notification, $routeParams) {
        $scope.message = "I'm working!";
        $scope.gamesystems = [];
        $scope.gameSessions = [];
        $scope.gameId = $routeParams.id;
        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;
        $scope.isSystemSelected = false;

        $scope.checkIfSystemSelected = function () {
            $scope.isSystemSelected = true;
        };

        $scope.gameSession = {
            Name: "",
            Description: "",
            System: "",
            MaximumPlayers: 0,
            GameId: $routeParams.id
        };

        $scope.initController = function () {
            $http({
                method: 'GET',
                url: 'api/GameSystem/Lookup'
            }).then(function success(response) {
                $scope.gamesystems = response.data;
                console.log(response.data);
            }, function error(response) {
            });

        };

        $scope.create = function () {
            $scope.isLoading = true;
            $http({
                method: 'POST',
                url: 'api/GameSession/CreateNew',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: $scope.gameSession

            }).then(function success(response) {
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                Notification.success('Game session created!');

                $rootScope.game = $scope.game;

                $timeout(function () {
                    $location.path("/game/" + gameId + "gamesession/");
                }, 700);

            }, function error(response) {
                $scope.onError = true;
                $scope.isLoading = false;
            });

        };
    };

    RPGSupportApp.controller("GameSessionCreateController", GameSessionCreateController);

}());