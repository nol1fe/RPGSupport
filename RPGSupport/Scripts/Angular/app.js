(function () {
    //APP
    var RPGSupportApp = angular.module("RPGSupportApp", ["ngRoute", "ngCookies", 'angular-storage', 'ui-notification', 'xeditable']);

    RPGSupportApp.config(
        ['$routeProvider', '$locationProvider', 'NotificationProvider',
            function ($routeProvider, $locationProvider, NotificationProvider) {
                $routeProvider
                    .when("/", {
                        templateUrl: paths.viewsPath + "Main.html",
                        controller: "MainController"
                    })
                    .when("/users", {
                        templateUrl: paths.viewsPath + "Users.html",
                        controller: "UsersController"
                    })
                    .when("/register", {
                        templateUrl: paths.viewsPath + "Register.html",
                        controller: "RegisterController"
                    })
                    .when("/login", {
                        templateUrl: paths.viewsPath + "Login.html",
                        controller: "LoginController"
                    })
                    .when("/logout", {
                        templateUrl: paths.viewsPath + "Main.html",
                        controller: "LogoutController"
                    })
                    .when("/manage", {
                        templateUrl: paths.viewsPath + "Manage.html",
                        controller: "ManageController"
                    })
                    .when("/character", {
                        templateUrl: paths.viewsPath + "CharacterList.html",
                        controller: "CharacterListController"

                    })
                    .when("/character/create", {
                        templateUrl: paths.viewsPath + "CharacterCreate.html",
                        controller: "CharacterController"
                    })
                    .when("/game/:id/gamesession", {
                        templateUrl: paths.viewsPath + "GameSessionList.html",
                        controller: "GameSessionListController"
                    })
                    .when("/game/:id/gamesession/create", {
                        templateUrl: paths.viewsPath + "GameSessionCreate.html",
                        controller: "GameSessionCreateController"
                    })
                    .when("/game/", {
                        templateUrl: paths.viewsPath + "GameList.html",
                        controller: "GameListController"
                    })
                    .when("/game/create", {
                        templateUrl: paths.viewsPath + "GameCreate.html",
                        controller: "GameCreateController"
                    })


                    .otherwise({ retdirectTo: "/" });

                //$locationProvider.html5Mode({
                //    enabled: true,

                //    //requireBase: false
                //});

                NotificationProvider.setOptions({
                    delay: 7000,
                    startTop: 60,
                    startRight: 10,
                    verticalSpacing: 20,
                    horizontalSpacing: 20,
                    positionX: 'right',
                    positionY: 'top'
                });

                //RPGSupportApp.config(
                //['$routeProvider',
                //function ($routeProvider) {
                //    $routeProvider
                //    //.when('/dashboard', {
                //    //    templateUrl: paths.viewsPath + 'Dashboard.html',
                //    //    controller: 'dashboardController'
                //    //})
                //}]);

            }]);


    // PATHS SETUP
    var paths = {
        viewsPath: 'Scripts/Angular/Views/',
        apiPath: '/api/'
    };

    RPGSupportApp.run(
        ['$rootScope', '$location', '$cookies', '$http', 'store', 'AuthService', 'editableOptions',
            function ($rootScope, $location, $cookies, $http, store, AuthService, editableOptions) {

                $rootScope.user = AuthService.getDefaultUserInfo();
                editableOptions.theme = 'bs3';
            }]);




}());