var app = angular.module('MainApp');

app.controller('JustSessionController', ['$scope', '$http', '$location', 'WizardHandler', function ($scope, $http, $location, WizardHandler) {

    $scope.nav.canNext = false;

    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            $scope.nav.canNext = false;
        }
    });

    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            $scope.nav.canNext = true;
            $scope.nav.canBack = true;
        }
    });


}]);