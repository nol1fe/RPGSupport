(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");
    var equals = function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            scope: {
                comparedValue: "=equals"
            },
            link: function (scope, element, attributes, ngModel) {

                ngModel.$validators.equals = function (modelValue) {
                    var result = modelValue === scope.comparedValue;
                    return result;
                };

                scope.$watch("comparedValue", function () {
                    ngModel.$validate();
                });
            }
        }
    };

    RPGSupportApp.directive("equals", equals);


}());