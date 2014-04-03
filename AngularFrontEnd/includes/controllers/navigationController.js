main.controller('NavigationController', function ($scope, AuthService) {
    $scope.username = ''
    $scope.showNav = false;
    
    //watch for route changes and stuff to show/hide our navigation.
    //there is probably a better way to do this.
    $scope.$on('$routeChangeStart', function(next, current) {
        $scope.showNav = AuthService.isLoggedIn();
        $scope.username = AuthService.username();
    });

    $scope.logout = function() {
        AuthService.logout();
    };
});