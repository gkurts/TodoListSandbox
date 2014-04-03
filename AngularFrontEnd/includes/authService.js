main.factory('AuthService', function ($http, settings, $window, $location) {
    return {
        login: function (credentials) {
            return $http.post(settings.api + '/login', credentials);
        },
        isLoggedIn: function () {
            if ($window.sessionStorage.getItem('token')) {
                return Boolean(true);
            }
            return Boolean(false);
        },
        username: function() {
            return $window.sessionStorage.getItem('username');
        },
        logout: function() {
            $window.sessionStorage.clear();
            $location.path('/login');
        }
    };
});