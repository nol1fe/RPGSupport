(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var CharacterListController = function ($scope, $http, $rootScope, $location, $timeout, Notification, $filter) {
        $scope.characters = [];
        $scope.container;
 
        $scope.error = '';
        $scope.message = '';

        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;

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
                url: 'api/Character/GetAll',
                
            }).then(onCharactersComplete, onError);
        }

        getAllCharacters();

        $scope.updateCharacter = function (character) {
            alert(character.Name);

            $http({
                method: 'PUT',
                url: 'api/Character/{character.Id}',
                data: {
                    Id : character.Id,
                    Name : character.Name,
                    Gender: character.Gender
                }
            }).then(function success(response) {
          
                $scope.onSuccess = true;
                notify.success();

            }, onError);
        };

    };

    RPGSupportApp.controller("CharacterListController", CharacterListController);
}());