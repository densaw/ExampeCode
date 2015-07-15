var app = angular.module('MainApp');


app.controller('TeamPlayersTableController', ['$scope', '$http', '$location', function ($scope, $http, $location) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];
    $scope.players = {};

    $http.get('/api/Curriculum/Players/Statistic/' + $scope.currId).success(function (data) {
        $scope.players = data;
    });

  


    $scope.addScale = function (cur) {

        if (cur == '0' || cur == '1' || cur == '2' || cur == '3' || cur == '4'|| cur == '5') {
            return { "background": "rgb(210, 0, 24)" };
        }
        else if ( cur == '6') {
            return { "background": "rgb(255, 221, 50)" };
        }
        else {
            return { "background": "rgb(0, 210, 58)" };
        }

    };

    $scope.addScale = function (att) {

        if (att == '0' || att == '1' || att == '2' || att == '3' || att == '4' || att == '5') {
            return { "background": "rgb(210, 0, 24)" };
        }
        else if (att == '6') {
            return { "background": "rgb(255, 221, 50)" };
        }
        else {
            return { "background": "rgb(0, 210, 58)" };
        }

    };


}]);