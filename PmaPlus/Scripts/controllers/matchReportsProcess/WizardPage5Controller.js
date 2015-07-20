var app = angular.module('MainApp');

app.controller('WizardPage5Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var confConfirm = angular.element('#confConfirm');

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
        }
    });

    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 5) {
            $http.put('/api/MatchReports/' + $scope.currId, $scope.$parent.cuurrentMatch);
        }

    });

    $scope.$on('finishWizardEvent', function () {
        $scope.addMatchNotes();
    });

    $scope.addMatchNotes = function () {
        $scope.$parent.obj.laddaLoading = true;
        //$scope.myform.form_Submitted = !$scope.myform.$valid;    
        $scope.$parent.cuurrentMatch.archived = true;
        $http.put('/api/MatchReports/' + $scope.currId, $scope.$parent.cuurrentMatch)
            .success(function () {

                $scope.$parent.obj.laddaLoading = false;
            }).error(function (data, status, headers, config) {
                $scope.$parent.obj.laddaLoading = false;
            });
    };

}]);