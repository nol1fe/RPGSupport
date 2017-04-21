(function () {
    //APP
    var RPGSupportApp = angular.module("RPGSupportApp", ["ngRoute", "ngCookies"]);

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
            .otherwise({ retdirectTo: "/" });

            //$locationProvider.html5Mode({
            //    enabled: true,
            //    //requireBase: false
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

        }]);


    // PATHS SETUP
    var paths = {
        viewsPath: 'Scripts/Angular/Views/',
        apiPath: '/api/'
    }

    RPGSupportApp.run(
        ['$rootScope', '$location', '$cookies', '$http',
        function ($rootScope, $location, $cookies, $http) {
            // keep user logged in after page refresh
            $rootScope.globals = $cookies.getObject('globals') || {};
            if ($rootScope.globals.currentUser) {
                $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata;
            }

            //$rootScope.$on('$locationChangeStart', function (event, next, current) {
            //    // redirect to login page if not logged in and trying to access a restricted page
            //    var restrictedPage = $.inArray($location.path(), ['/login', '/register']) === -1;
            //    var loggedIn = $rootScope.globals.currentUser;
            //    if (restrictedPage && !loggedIn) {
            //        $location.path('/login');
            //    }
            //});
        }]);




}());