app.config(['$routeProvider',
    function($routeProvider) {
        $routeProvider.
            when('/list/newlist', {
                templateUrl: 'Partials/AddList.html',
                controller: 'AddListController'
            }).
            when('/', {
                templateUrl: 'Partials/Tasks.html',
                controller: 'MainController'
            }).
            when('/login', {
                templateUrl: 'Partials/Login.html',
                controller: 'LoginController'
            });
    }]);

