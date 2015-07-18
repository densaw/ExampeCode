var app = angular.module('MainApp');

app.controller('WizardPage2Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $scope.getTable = function () {
        $http.get('/api/MatchObjectives/' + $scope.currId).success(function (result) {
            $scope.playersList = result;
            var completed = true;
            angular.forEach($scope.playersList, function (player) {
                if (!player.objective) {
                    completed = false;
                } else {
                    if (player.objective.length < 1) {
                        completed = false;
                    }
                }
            });

            if (completed || $scope.$parent.cuurrentMatch.archived) {
                $scope.nav.canNext = true;
            } else { $scope.nav.canNext = false; }
        });
    };

    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 1) {
            $scope.getTable();
            $scope.nav.canNext = false;
            $scope.nav.canBack = false;
            $scope.nav.last = false;
        }
    });

    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 1) {
            var completed = true;
            angular.forEach($scope.playersList, function (player) {
                if (!player.objective) {
                    completed = false;
                } else {
                    if (player.objective.length < 1) {
                        completed = false;
                    }
                }
            });

            if (completed) {
                $scope.addDetails();
            } else { $scope.pressed = true; }
        }

    });



    $scope.addDetails = function () {
        $scope.$parent.obj.laddaLoading = true;

        $scope.loginLoading = false;
        $http.post('/api/MatchObjectives/Table', $scope.playersList)
            .success(function () {
                $scope.nav.canNext = true;
                $scope.$parent.obj.laddaLoading = false;
            })
            .error(function (data, status, headers, config) {

            });
    };

    $scope.getTable();
}]);