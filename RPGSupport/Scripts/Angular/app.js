(function () {
    //APP
    var RPGSupportApp = angular.module("RPGSupportApp", ["ngRoute"]);

    RPGSupportApp.config(
        ['$routeProvider', '$locationProvider',
        function ($routeProvider, $locationProvider) {
            $routeProvider
            .when("/", {
                templateUrl: paths.viewsPath + "Main.html",
                controller: "MainController"
            })
            .when("/users", {
                templateUrl: paths.viewsPath + "Users.html",
                controller: "UsersController"
            })
            .when("/login", {
                templateUrl: paths.viewsPath + "Login.html",
                controller: "LoginController"
            })
            .otherwise({ retdirectTo: "/" });

            //$locationProvider.html5Mode({
            //    enabled: true,
            //    //requireBase: false
            //});

        }]);


    // PATHS SETUP
    var paths = {
        viewsPath: 'Scripts/Angular/Views/',
        apiPath: '/api/'
    }


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


}());