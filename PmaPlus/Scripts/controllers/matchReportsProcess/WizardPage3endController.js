var app = angular.module('MainApp');

app.controller('WizardPage3endController', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', function ($scope, $http, $q, $location, $rootScope, toaster) {

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
        console.log('details');
        console.log(result);
    });
    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
        $scope.cuurrentMatch = result;
        console.log('pre');
        console.log(result);
    });

    $scope.addMatchDetails = function () {
        $scope.loginLoading = true;
        //$scope.myform.form_Submitted = !$scope.myform.$valid;    
        $scope.loginLoading = false;
        $http.put('/api/MatchReports/' + $scope.currId, $scope.matchDetails).success(function () {
        confConfirm1.modal('hide');
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