main.controller('LoginController', function($scope, $http, $window, $location, AuthService) {
    $scope.credentials = { username: '', password: '' };
    $scope.message = '';
    
    $scope.login = function (credentials) {
        $scope.message = '';
        AuthService.login(credentials).then(function (data) {
            $window.sessionStorage.setItem('token', data.data.Token);
            $window.sessionStorage.setItem('username', credentials.username);
            $location.path('/');
        }, function () {
            $window.sessionStorage.clear();
            $scope.message = 'Invalid Login!';
        });
    };


});

