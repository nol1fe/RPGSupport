(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var GameSessionListController = function ($scope, $http, $rootScope, $location, $timeout, Notification, $routeParams) {
        $scope.gameSessions = [];
        $scope.gamesystems = {};
        $scope.gameId = $routeParams.id;
        $scope.gameName = ''
        $scope.gameSessionsAny = false;



        $scope.initController = function () {
            $http({
                method: 'GET',
                url: 'api/GameSession/GetGameSessionsForGame/' + $routeParams.id,
            }).then(function success(response) {
                console.log(response.data);
                $scope.gameSessions = response.data;
                if (response.data.length) {
                    $scope.gameName = response.data[0].Game.Name;
                }
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                CheckIfAnyGameSessionsAvailable();

            }, function error(response) {
                $scope.error = "Game Sessions not found";
                $scope.onError = true;
                $scope.isLoading = false;

            });
            $http({
                method: 'GET',
                url: 'api/GameSystem/Lookup'
            }).then(function success(response) {
                $scope.gamesystems = response.data;
            }, function error(response) {
            });
        };

        $scope.showGameSystemValue = function (gamesystem) {
            var selectedGameSystem = '';
            $.each($scope.gamesystems, function (index, item) {

                if (item.Key === gamesystem) {
                    selectedGameSystem = item.Value;
                }
            });
            console.log(selectedGameSystem);
            return selectedGameSystem;
        };

        function CheckIfAnyGameSessionsAvailable() {
            $scope.gameSessionsAny = $scope.gameSessions.length > 0;
        }
    };

    RPGSupportApp.controller("GameSessionListController", GameSessionListController);

}());