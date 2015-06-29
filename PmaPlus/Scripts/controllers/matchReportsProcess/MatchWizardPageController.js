var app = angular.module('MainApp');

app.controller('MatchWizardPageController', ['$scope', '$http', '$q', '$location', '$rootScope', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $scope.nav = {};

    $scope.nav.canNext = true;
    $scope.nav.canBack = true;

    $scope.progress = {};

    $scope.saveProgress = function () {
        $scope.$broadcast('saveProgressEvent');
    };

    $scope.updateProgress = function () {
        $scope.progress.current = WizardHandler.wizard().currentStepNumber() - 1;
        $scope.$broadcast('moveEvent');

    };

   

}]);