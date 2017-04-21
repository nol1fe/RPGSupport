(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var LoginController = function ($scope, $http, $rootScope, $location, $timeout, AuthService) {

        $scope.credentials = {
            Email: '',
            Password: '',
            RememberMe: false
        };

        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;



        $scope.login = function () {
            $scope.isLoading = true;

            $http({
                method: 'POST',
                url: 'api/login',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: $scope.credentials

            }).then(function success(response) {
    
                $rootScope.userName = $scope.credentials.Email;
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                AuthService.SetCredentials($scope.credentials.Email, $scope.credentials.Password, $scope.credentials.RememberMe);
                console.log($scope.credentials.RememberMe);
                //$rootScope.isAuthenticated = true;
                console.log($rootScope.globals.currentUser);

                $timeout(function () {
                    $location.path("/");
                }, 700);

            }, function error(response) {
                $scope.onError = true;
                $scope.isLoading = false;
            });
        };

         

    };

    RPGSupportApp.controller("LoginController", LoginController);

}());