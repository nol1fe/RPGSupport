(function () {

    var RPGSupportApp = angular.module("RPGSupportApp");

    var CharacterListController = function ($scope, $http, $rootScope, $location, $timeout, Notification, $filter) {
        $scope.characters = [];
        $scope.genders = [];
        $scope.container;
        $scope.min = 1;
        $scope.max = 99;

        $scope.error = '';
        $scope.message = '';

        $scope.onSuccess = false;
        $scope.onError = false;
        $scope.isLoading = false;

        $scope.initController = function () {
            $http({
                method: 'GET',
                url: 'api/Character/Gender/Lookup'
            }).then(function success(response) {

                $scope.genders = response.data;

            }, function error(response) {
            });
        };

        $scope.showGenderValue = function (gender) {

            var selectedGender = '';

            $.each($scope.genders, function (index, item) {

                if (item.Key === gender) {
                    selectedGender = item.Value;
                }
            });

            return selectedGender;

        };


        var onCharactersComplete = function (response) {
            $scope.characters = response.data;
            $scope.onSuccess = true;
            $scope.onError = false;
            $scope.isLoading = false;

            console.log($scope.characters);

        };

        var onError = function (reason) {
            $scope.error = "Characters not found";
            $scope.onError = true;
            $scope.isLoading = false;
        };

        //$http.get('api/Character/GetAll').then(onCharactersComplete, onError);

        var getAllCharacters = function () {
            $scope.isLoading = true;
            $http({
                method: 'GET',
                url: 'api/Character/'

            }).then(onCharactersComplete, onError);

        };

        getAllCharacters();

        $scope.updateCharacter = function (character) {
      
            $http({
                method: 'PUT',
                url: 'api/Character/' + character.Id, // poprawilem, bylo -> url: 'api/Character/{character.Id}', <- co to kurwa jest parzyk :D

                headers: {
                    'Content-Type': 'application/json'
                },
                data: {
                    Id : character.Id,
                    Name : character.Name,
                    Gender: character.Gender,
                    Statistics : character.Statistics
                }
            }).then(function success(response) {
          
                $scope.onSuccess = true;
                Notification.success('Character edited!');
                $timeout(function () {
                    $location.path("/character");
                    getAllCharacters();
                }, 700);

            }, onError);
        };

        $scope.deleteCharacter = function (characterId) {
            console.log(characterId);
            $scope.isLoading = true;
            $http({
                method: 'DELETE',
                url: 'api/Character/' + characterId,
                data: {
                    data: { id: characterId }
                }

            }).then(function success(response) {
                $scope.onSuccess = true;
                $scope.onError = false;
                $scope.isLoading = false;
                Notification.info('Character deleted!');
                $timeout(function () {
                    $location.path("/character");
                    getAllCharacters();
                }, 700);

            }, function error(response) {
                $scope.onError = true;
                $scope.isLoading = false;
            });

        };

    };

    RPGSupportApp.controller("CharacterListController", CharacterListController);
}());