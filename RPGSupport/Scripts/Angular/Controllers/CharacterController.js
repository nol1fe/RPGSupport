(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var CharacterController = function ($scope, $http, $rootScope, $location, $timeout, Notification) {

        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;
        $scope.isSystemSelected = false;
        $scope.isSystemLoaded = false;

        $scope.character = {
            Name: "",
            Gender: "",
            System: "",
            Statistics: [],
        };

        $scope.genders = [];
        $scope.gamesystems = [];

        $scope.characterStatistics = [];

        $scope.updateCharacterStatistics = function () {
            $scope.character.Statistics = $scope.characterStatistics;
        };

        $scope.checkIfSystemSelected = function () {
            $scope.isSystemSelected = true;
        };

        $scope.initController = function () {
            $http({
                method: 'GET',
                url: 'api/Character/Gender/Lookup',
            }).then(function success(response) {
            
                $scope.genders = response.data;

            }, function error(response) {
            });
            $http({
                method: 'GET',
                url: 'api/Character/GameSystem/Lookup',
            }).then(function success(response) {

                $scope.gamesystems = response.data;

            }, function error(response) {
            });

        }

        $scope.loadGameSystem = function (systemId) {
            $scope.isLoading = true;
            console.log("id : " + systemId);
            $http({
                method: 'GET',
                url: 'api/Character/GameSystem/' + $scope.character.System,
                data: { id: systemId }

            }).then(function success(response) {
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                $scope.isSystemLoaded = true;
                Notification.info('GameSystem Loaded');

                $scope.characterStatistics = response.data;

                for (var i = 0; i <= response.data.lenght; i++) {
                    $scope.characterStatistics.push(response.data[i]);

                };

            }, function error(response) {
                $scope.onError = true;
                $scope.isLoading = false;

                $scope.isSystemLoaded = false;
                $scope.characterStatistics = [];

                Notification.warning('Sorry, this system is not available yet');

            });

        };

        $scope.create = function () {

            $scope.isLoading = true;
            $http({
                method: 'POST',
                url: 'api/Character/CreateNew',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: $scope.character

            }).then(function success(response) {
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                Notification.success('Character created!');

                $rootScope.character = $scope.character;

                $timeout(function () {
                    $location.path("/character");
                }, 700);

            }, function error(response) {
                $scope.onError = true;
                $scope.isLoading = false;
            });

        };





    };

    RPGSupportApp.controller("CharacterController", CharacterController);
}());