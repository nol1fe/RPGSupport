(function () {
    var RPGSupportApp = angular.module("RPGSupportApp");

    var LogoutController = function (AuthService, $rootScope, $location) {
        AuthService.ClearCredentials();
        $location.path('/');
    }

    RPGSupportApp.controller("LogoutController", LogoutController);
}());