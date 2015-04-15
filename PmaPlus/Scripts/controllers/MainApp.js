(function () {


    var module = angular.module('MainApp',['ngRoute']);

    module.config(function($routeProvider, $locationProvider) {
        $routeProvider
            .when('/users', {
                templateUrl: '/user.html',
                controller: 'userController'
            });
        
    });

    module.controller('userController', userController);

    function userController($scope,$http) {
        
        
        $http.get('/api/user').success(function (data) {
            $scope.users = data;
        });
    }
})();