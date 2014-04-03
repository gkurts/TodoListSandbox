
//todo: extract http calls to a "TodoService"

main.controller('MainController', function ($scope, $http, $routeParams, SecretService, settings) {

	$scope.formData = {};
	$scope.todoLists = [];
    getTodoLists();

    // when landing on the page, get all todos and show them
    function getTodoLists() {
        $http.get(settings.api + '/todo')
            .success(function (data) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Id == SecretService.listId) {
                        data[i].active = true;
                    }
                }
                $scope.todoLists = data;
            })
            .error(function(data) {
                console.log('Error: ' + data);
            });
    };

    $scope.setListId = function(id) {
        SecretService.listId = id;
    };
    
    // when submitting the add form, send the text to the node API
    $scope.createTodo = function (id) {
        $http.post(settings.api + '/todo/CreateTodo/' + id, $scope.formData)
            .success(function (data) {
                $scope.formData = {}; // clear the form so our user is ready to enter another
                getTodoLists();
            })
            .error(function(data) {
                console.log('Error: ' + data);
            });
    };

    // delete a todo after checking it
    $scope.deleteTodo = function (id) {
        $http.post(settings.api + '/todo/DeleteTodo/' + id)
            .success(function (data) {
                getTodoLists();
            })
            .error(function (data) {
                console.log('Error: ' + data);
            });
    };

    $scope.deleteList = function (id) {
        if (confirm("Are you really sure you want to delete this list?" + id)) {
            $http.post(settings.api + '/todo/DeleteList/' + id)
                .success(function (data) {
                    getTodoLists();
                })
                .error(function(data) {
                    console.log('Error: ' + data);
                });
        }
    };

});