//(function () {
    //APP
    var RPGSupportApp = angular.module("RPGSupportApp", ["ngRoute"]);
    //var RPGSupportApp = angular.module("RPGSupportApp", []);

    RPGSupportApp.config(
        ['$routeProvider',
        function ($routeProvider) {
        $routeProvider
        .when("/", {
            templateUrl: "Scripts/Views/Users.html",
            controller: "UsersController"
        })
        .otherwise({ retdirectTo: "/" });
        }]);
    // PATHS SETUP

    //var paths = {
    //    viewsPath :'Scripts/Views/',
    //    apiPath: '/api/'
    //}



    //RPGSupportApp.controller("UsersController", UsersController)

    //RPGSupportApp.value("projectConfig", {
    //    viewsPath: paths.viewsPath,
    //    apiPath: paths.apiPath
    //});

    //RPGSupportApp.config(
    //['$routeProvider',
    //function ($routeProvider) {
    //    $routeProvider
    //    //.when('/dashboard', {
    //    //    templateUrl: paths.viewsPath + 'Dashboard.html',
    //    //    controller: 'dashboardController'
    //    //})
    //}]);


//}());