main.controller('AddListController', function ($scope, $http, $location, SecretService, settings) {
    $scope.newListName = {};

    $scope.createList = function() {
        $http.post(settings.api + '/todo/CreateList', $scope.newListName)
            .success(function (data) {
                $scope.newListName = {};
                SecretService.listId = data.Id;
                $location.path('/');
            })
            .error(function(data) {
                console.log('Error: ' + data);
            });
    }
});