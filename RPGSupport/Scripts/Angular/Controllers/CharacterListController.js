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

            console.log($scope.characters);
        }

        getAllCharacters();

        $scope.updateCharacter = function (character) {
      
            $http({
                method: 'PUT',
                url: 'api/Character/Update{character.Id}',
                data: {
                    Id : character.Id,
                    Name : character.Name,
                    Gender: character.Gender,
                    Statistics : character.Statistics
                }
            }).then(function success(response) {
          
                $scope.onSuccess = true;
                notify.success();
                $timeout(function () {
                    $location.path("/character");
                    getAllCharacters();
                }, 700);

            }, onError);
        };

        $scope.deleteCharacter = function (character) {
            $scope.isLoading = true;
            $http({
                method: 'POST',
                url: 'api/Character/DeleteCharacter{id}',
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