var app = angular.module('MainApp');

app.controller('AtendanceController', ['$scope', '$http', '$location', function ($scope, $http, $location) {

    // get att step 2
    var confDetail = angular.element('#confDetail');
   
    var confAtend = angular.element('#confAtend');


    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $http.get('/api/Curriculum/Wizard/Session/AttendanceTable/')
        .success(function (result) {
            console.log(result);
            $scope.item = result;

        });
    $scope.ssesionDetail = function () {
        confDetail.modal('show');
    };

}]);