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
        $scope.users = [
            {
                UserName: 'aadasasd',
                Email: 'sdaad'
            },
            {
                UserName: 'aadasasd',
                Email: 'sdaad'
            }
        ];
        
        $http.get('/api/user').success(function (data) {
            $scope.users = data;
        });
    }
})();