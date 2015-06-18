var app = angular.module('MainApp');


app.controller('TeamPlayersTableController', ['$scope', '$http', function ($scope, $http) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];
    $scope.players = {};

    $http.get('/api/Curriculum/Players/Statistic/' + $scope.currId).success(function (data) {
        $scope.players = data;
    });

}
]);