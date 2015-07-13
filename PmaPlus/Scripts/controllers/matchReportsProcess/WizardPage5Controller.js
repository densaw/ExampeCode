var app = angular.module('MainApp');

app.controller('WizardPage5Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];



    var confConfirm = angular.element('#confConfirm');

    var getTable = function () {

        $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
            $scope.matchNotes = result;
        });
    }


    $scope.confirmModal = function () {
        confConfirm.modal('show');
    }

    $scope.confirmAbort = function () {
        confConfirm.modal('hide');
    }

    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 5) {
            $scope.nav.canNext = false;
            $scope.nav.last = true;
            getTable();
        }
    });

    $scope.$on('finishWizardEvent', function () {
        $scope.addMatchNotes();
       
    });

    $scope.addMatchNotes = function () {
        $scope.loginLoading = true;
        //$scope.myform.form_Submitted = !$scope.myform.$valid;    
        $scope.matchNotes.archived = true;
        $http.put('/api/MatchReports/' + $scope.currId, $scope.matchNotes)
            .success(function () {
                confConfirm.modal('hide');
                $scope.loginLoading = false;
            }).error(function (data, status, headers, config) {
                $scope.loginLoading = false;
            });
    };

}]);