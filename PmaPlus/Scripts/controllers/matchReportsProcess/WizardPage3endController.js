var app = angular.module('MainApp');

app.controller('WizardPage3endController', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var confConfirm1 = angular.element('#confConfirm1');

    $scope.confirmModal1 = function () {
        confConfirm1.modal('show');
    }

    $scope.confirmCancel = function () {
        confConfirm1.modal('hide');
    }

    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
        $scope.matchDetails = result;
    });
    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
        $scope.cuurrentMatch = result;
    });

    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 4) {
            $scope.nav.canNext = true;
            $scope.nav.canBack = true;
        }
    });

    $scope.addMatchDetails = function () {
        $scope.loginLoading = true;
        //$scope.myform.form_Submitted = !$scope.myform.$valid;    
        $scope.loginLoading = false;
        $http.put('/api/MatchReports/' + $scope.currId, $scope.matchDetails).success(function () {
            confConfirm1.modal('hide');
        }).error(function (data, status, headers, config) { });
    };

}]);