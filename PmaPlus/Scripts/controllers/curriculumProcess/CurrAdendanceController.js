var app = angular.module('MainApp');

app.controller('AtendanceController', ['$scope', '$http', '$location', function ($scope, $http, $location) {

    // get att step 2
    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $http.get('/api request/' + $scope.currId)
        .success(function (result) {
            console.log(result);
            $scope.item = result;

        });

}]);