var app = angular.module('MainApp');

app.controller('WizardPage3Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];



    $scope.getTable = function () {
        $http.get('/api/MatchObjectives/' + $scope.currId).success(function (result) {
            $scope.playersList = result;
        });
    };



    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 2) {
            $scope.getTable();
            $scope.nav.canNext = false;
            $scope.nav.canBack = true;
            $scope.nav.last = false;
        }
    });

    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 2) {
            $scope.addDetails();

        }

    });



    $scope.addDetails = function () {
        $scope.$parent.obj.laddaLoading = true;
        $http.post('/api/MatchObjectives/Table', $scope.playersList)
            .success(function () {
                $scope.nav.canNext = true;
                $scope.$parent.obj.laddaLoading = false;
            })
            .error(function (data, status, headers, config) {

            });
    };

}]);