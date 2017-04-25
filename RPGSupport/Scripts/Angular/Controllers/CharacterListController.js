(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var CharacterListController = function ($scope, $http, $rootScope, $location, $timeout, Notification, $filter) {
        $scope.characters = [];
        $scope.container;
        //$scope.characters = {
        //    character: {
        //        Name: "",
        //        Gender: ""
        //    }
        //};

        $scope.error = '';
        $scope.message = '';

        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;

        var onUserComplete = function (response) {
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
        $http.get('api/Character/GetAll').then(onUserComplete, onError);

        $scope.updateCharacter = function (character) {
            alert(character.Name);
        };

    };

    RPGSupportApp.controller("CharacterListController", CharacterListController);
}());