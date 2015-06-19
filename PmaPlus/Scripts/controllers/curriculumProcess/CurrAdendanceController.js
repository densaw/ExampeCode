﻿var app = angular.module('MainApp');

app.controller('AtendanceController', ['$scope', '$http', '$location', function ($scope, $http, $location) {

    // get att step 2
    var confDetail = angular.element('#confDetail');
   
    var confAtend = angular.element('#confAtend');

    $scope.date = new Date();
    

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $http.get('/api/Curriculum/Wizard/Session/AttendanceTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId)
        .success(function (result) {
            $scope.items = result;

        });

    $scope.ssesionDetail = function () {
        $scope.modalTitle = "Details";
        confDetail.modal('show');
        $('#confDetail').appendTo("body").modal('show');
    };
    $scope.confAtend = function () {
        $scope.modalTitle = "Details";
        confAtend.modal('show');
        $('#confAtend').appendTo("body").modal('show');
    };

    $scope.addDetails = function ()
    { confDetail.modal('hide'); }
    $scope.closeDetails = function ()
    { confDetail.modal('hide'); }

}]);