(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var CharacterController = function ($scope, $http, $rootScope, $location, $timeout, Notification) {

        $scope.character = {
            Name: "",
            Gender: ""
            
        };

        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;

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