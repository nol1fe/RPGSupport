//(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var UsersController = function ($scope, $http) {
        $scope.users = [];
        $scope.error = '';
        $scope.message = '';

        var onUserComplete = function (response) {
            $scope.users = response.data;
            for (var i = 0; i <= response.data.lenght; i++) {
                $scope.users.push(response.data[i]);
            }
        }
        
        var onError = function (reason) {
            $scope.error = "User not found";
        }

        $http.get("/api/Users").then(onUserComplete, onError);

        $scope.message = "oki działa";
       
    };

    RPGSupportApp.controller("UsersController", UsersController);

//}());