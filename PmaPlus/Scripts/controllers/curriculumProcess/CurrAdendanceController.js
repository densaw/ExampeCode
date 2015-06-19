var app = angular.module('MainApp');

app.controller('AtendanceController', ['$scope', '$http', '$location', function ($scope, $http, $location) {

    // get att step 2
    var confDetail = angular.element('#confDetail');
   
    var confAtend = angular.element('#confAtend');


    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $http.get('/api/Curriculum/Wizard/Session/AttendanceTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId)
        .success(function (result) {
            $scope.items = result;

        });
    $scope.ssesionDetail = function () {
        confDetail.modal('show');
    };

}]);