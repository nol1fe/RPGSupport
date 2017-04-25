(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var ManageController = function ($scope, $http, $rootScope, Notification) {
        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;
        $scope.userInfo = {};

        var onUserComplete = function (response) {
            $scope.user = response.data;
            $scope.onSuccess = true;
            $scope.onError = false;
            $scope.isLoading = false;

            Notification.success('Done');

            $timeout(function () {
                $location.path("/");
            }, 700);
        }

        var onError = function (reason) {
            $scope.onError = true;
            $scope.isLoading = false;
            Notification.error('Wrong username or password!');
        }

        $scope.login = function () {
            $scope.isLoading = true;

            $http({
                method: 'GET',
                url: 'api/Users/GetCurrentUser',

            }).then(onUserComplete, onError);
        }
    }


    RPGSupportApp.controller("ManageController", ManageController);

}());