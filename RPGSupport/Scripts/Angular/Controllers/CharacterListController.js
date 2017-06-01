﻿(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var CharacterListController = function ($scope, $http, $rootScope, $location, $timeout, Notification, $filter) {
        $scope.characters = [];
        $scope.genders = [];
        $scope.container;

        //$scope.Gender = [
        //      { value: 1, text: 'Male' },
        //      { value: 2, text: 'Female' }
        //];
 
        $scope.error = '';
        $scope.message = '';

        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;

        //$scope.selectedGender = "";


        $scope.initController = function () {
            $http({
                method: 'GET',
                url: 'api/Character/Gender/Lookup',
            }).then(function success(response) {

                $scope.genders = response.data;

            }, function error(response) {
            });
        }

        $scope.showGenderValue = function(gender){

            var selectedGender = '';

            $.each($scope.genders, function (index, item) {

                if (item.Key === gender) {
                    selectedGender = item.Value;
                }
            });

            return selectedGender;

        }


        var onCharactersComplete = function (response) {
            $scope.characters = response.data;
            $scope.onSuccess = true;
            $scope.onError = false;
            $scope.isLoading = false;

            console.log($scope.characters);

        }

        var onError = function (reason) {
            $scope.error = "Characters not found";
            $scope.onError = true;
            $scope.isLoading = false;
        }

        //$http.get('api/Character/GetAll').then(onCharactersComplete, onError);

        var getAllCharacters = function () {
            $scope.isLoading = true;
            $http({
                method: 'GET',
                url: 'api/Character/',
                
            }).then(onCharactersComplete, onError);

        }

        getAllCharacters();

        //$scope.showGender = function (character) {
        //    var selected = $filter('filter')($scope.Gender, { text: character.Gender });

        //    return (character.Gender && selected.length) ? selected[0].text : 'Not set';

        //};

        $scope.updateCharacter = function (character) {
      
            $http({
                method: 'PUT',
                url: 'api/Character/{character.Id}',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    Id : character.Id,
                    Name : character.Name,
                    Gender: character.Gender,
                    Statistics : character.Statistics
                }
            }).then(function success(response) {
          
                $scope.onSuccess = true;
                Notification.success('Character edited!');
                $timeout(function () {
                    $location.path("/character");
                    getAllCharacters();
                }, 700);

            }, onError);
        };

        $scope.deleteCharacter = function (character) {
            $scope.isLoading = true;
            $http({
                method: 'DELETE',
                url: 'api/Character/{character.Id}',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    Id : character.Id
                }

            }).then(function success(response) {
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                Notification.info('Character deleted!');
                $timeout(function () {
                    $location.path("/character");
                    getAllCharacters();
                }, 700);

            }, function error(response) {
                $scope.onError = true;
                $scope.isLoading = false;
            });

        };

    };

    RPGSupportApp.controller("CharacterListController", CharacterListController);
}());