(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var GameListController = function ($scope, $http, $rootScope, $location, $timeout, Notification) {
        $scope.message = "I'm working!";
        $scope.games = [];
        $scope.gamesystems = [];

        $scope.initController = function () {
            $http({
                method: 'GET',
                url: 'api/Game'
            }).then(function success(response) {
                $scope.games = response.data;
                console.log(response.data);
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
            }, function error(response) {
                $scope.error = "Games not found";
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
        }

          
        $scope.showGameSystemValue = function (gamesystem) {
            var selectedGameSystem = '';
            $.each($scope.gamesystems, function (index, item) {

                if (item.Key === gamesystem) {
                    selectedGameSystem = item.Value;
                }
            });
            return selectedGameSystem;
        };
    };

    RPGSupportApp.controller("GameListController", GameListController);

}());