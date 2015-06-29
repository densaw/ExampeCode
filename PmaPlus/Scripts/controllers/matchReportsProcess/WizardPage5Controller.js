﻿var app = angular.module('MainApp');

app.controller('WizardPage5Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', function ($scope, $http, $q, $location, $rootScope, toaster) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var confConfirm = angular.element('#confConfirm');

    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
        $scope.matchNotes = result;

    });

    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
        $scope.cuurrentMatch = result;

    });
    $scope.confirmModal = function () {
        confConfirm.modal('show');
    }

    $scope.confirmAbort = function () {
        confConfirm.modal('hide');
    }

    $scope.addMatchNotes = function () {
        $scope.loginLoading = true;
        //$scope.myform.form_Submitted = !$scope.myform.$valid;    
        $scope.loginLoading = false;
        $http.put('/api/MatchReports/' + $scope.currId, $scope.matchNotes).success(function () {
        confConfirm.modal('hide');
        }).error(function (data, status, headers, config) {
            if (status == 400) {
                console.log(data);
                toaster.pop({
                    type: 'error',
                    title: 'Error', bodyOutputType: 'trustedHtml',

                });
            }
        });
    };

}]);