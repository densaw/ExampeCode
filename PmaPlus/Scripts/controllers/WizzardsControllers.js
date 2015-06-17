var app = angular.module('MainApp');



app.controller('WizzardController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {
    $scope.enterValidation = function () {
        return true;
    };

    $scope.exitValidation = function () {
        return true;
    };
    //example using context object
    $scope.exitValidation = function (context) {
        return context.firstName === "Jacob";
    }
    //example using promises
    $scope.exitValidation = function () {
        var d = $q.defer()
        $timeout(function () {
            return d.resolve(true);
        }, 2000);
        return d.promise;
    }
}]);