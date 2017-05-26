(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");
    var backButton = function ($window) {
        return {
            restrict: 'A',
            link: function (scope, elem, attrs) {
                elem.bind('click', function () {
                    $window.history.back();
                });
            }
        };

    };

    RPGSupportApp.directive("backButton", backButton);


}());