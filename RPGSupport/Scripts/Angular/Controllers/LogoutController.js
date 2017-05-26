(function () {
    var RPGSupportApp = angular.module("RPGSupportApp");

    var LogoutController = function (AuthService, $rootScope, $location, Notification) {
        AuthService.ClearCredentials();
        $location.path('/');
        Notification.info("You have successfully logged off.")
    }

    RPGSupportApp.controller("LogoutController", LogoutController);
}());