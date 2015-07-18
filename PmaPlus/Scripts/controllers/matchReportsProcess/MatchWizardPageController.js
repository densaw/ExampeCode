var app = angular.module('MainApp');

app.controller('MatchWizardPageController', ['$scope', '$http', '$q', '$location', '$rootScope', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];
    $scope.obj = {
        laddaLoading:false
    }

    $scope.nav = {};

    $scope.nav.canNext = true;
    $scope.nav.canBack = true;
    $scope.nav.last = false;

    $scope.progress = {};

    $scope.saveProgress = function () {
        $scope.$broadcast('saveProgressEvent');
    };

    $scope.updateProgress = function () {
        $scope.progress.current = WizardHandler.wizard().currentStepNumber() - 1;
        $scope.$broadcast('moveEvent');

    };

    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
        $scope.cuurrentMatch = result;
    });

    $scope.finishWizard = function() {
        $scope.$broadcast('finishWizardEvent');
        confConfirm.modal('hide');
    }


    var confConfirm = angular.element('#confConfirm5');

    $scope.confirmModal = function () {
        confConfirm.modal('show');
    }

    $scope.confirmAbort = function () {
        confConfirm.modal('hide');
    }
}]);