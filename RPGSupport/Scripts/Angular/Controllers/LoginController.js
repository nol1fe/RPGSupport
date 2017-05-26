(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var LoginController = function ($scope, $http, $rootScope, $location, $timeout, AuthService, Notification) {

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

                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                AuthService.SetCredentials($scope.credentials.Email, $scope.credentials.Password, $scope.credentials.RememberMe);
                Notification.success('You have successfully logged in!');

                $timeout(function () {
                    $location.path("/");
                }, 700);

            }, function error(response) {
         
                $scope.onError = true;
                $scope.isLoading = false;
                Notification.error('Wrong username or password!');

            });
        };

         

    };

    RPGSupportApp.controller("LoginController", LoginController);

}());