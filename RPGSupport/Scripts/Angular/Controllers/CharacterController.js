﻿(function () {

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
        };

        $scope.characterStatistics = [];
        $scope.stats = [{
            Key: "",
            Value: ""
        }];


        $scope.checkIfSystemSelected = function () {
            $scope.isSystemSelected = true;
        }

        $scope.loadGameSystem = function () {
            $scope.isLoading = true;
            $http({
                method: 'POST',
                url: 'api/Character/GameSystemStatistics',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: $scope.character

            }).then(function success(response) {
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                $scope.isSystemLoaded = true;
                Notification.success('GameSystem Loaded');

                $scope.characterStatistics = response.data;

                for (var i = 0; i <= response.data.lenght; i++) {
                    $scope.characterStatistics.push(response.data[i]);

                };

            }, function error(response) {
                $scope.onError = true;
                $scope.isLoading = false;
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

            }, function error(response) {
                $scope.onError = true;
                $scope.isLoading = false;
            });

        };
    };

    RPGSupportApp.controller("CharacterController", CharacterController);
}());