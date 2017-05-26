(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var RegisterController = function ($scope, $http, $rootScope, AuthService, $location, $timeout, Notification) {
        AuthService.ClearCredentials();

        $scope.user = {
            Email: "",
            Password: "",
            ConfirmPassword: ""

        };

        $scope.RememberMe = false;

        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;

        $scope.register = function () {
            $scope.isLoading = true;
            $http({
                method: 'POST',
                url: 'api/register',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: $scope.user

            }).then(function success(response) {
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                AuthService.SetCredentials($scope.user.Email, $scope.user.Password, $scope.RememberMe);
                Notification.success('User successfully created!');

            $timeout(function () {
                $location.path("/");
            }, 700);

            }, function error(response) {
                $scope.onError = true;
                $scope.isLoading = false;
            });

        };
    };

    RPGSupportApp.controller("RegisterController", RegisterController);

}());