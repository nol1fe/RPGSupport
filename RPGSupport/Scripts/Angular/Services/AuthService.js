(function () {

    var AuthService = function ($http, $rootScope, $cookies, store) {
        var userInfoKey = '%app%Usr';

        var getDefaultUserInfo = function () {

            var storedUser = store.get(userInfoKey);

            if (storedUser != null) {
                return storedUser;
            }

            return {
                username: 'anonymous',
                isAuthenticated: false,
            };

            $rootScope.user = user;

        }
        var user = getDefaultUserInfo();

        function SetCredentials(username, password, rememberme) {

            var user = {
                username : username,
                isAuthenticated: true
            }

            store.set(userInfoKey, user);
            $rootScope.user = user;
        }

        function ClearCredentials() {
            store.remove(userInfoKey);
            $rootScope.user = getDefaultUserInfo();

        }

        return {
            SetCredentials: SetCredentials,
            ClearCredentials: ClearCredentials,
            getDefaultUserInfo: getDefaultUserInfo
        };

    };

    var RPGSupportApp = angular.module("RPGSupportApp");

    RPGSupportApp.factory("AuthService", AuthService);

}());