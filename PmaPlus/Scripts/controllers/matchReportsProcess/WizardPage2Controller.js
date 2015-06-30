var app = angular.module('MainApp');

app.controller('WizardPage2Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var confDetail = angular.element('#confDetail');

    $scope.openEdit = function (player) {
        $scope.modalTitle = "Edit";
        $scope.player = player;
        $scope.playerName = player.playerName;
        $scope.objective = player.objective;
        confDetail.modal('show');
    };

    $scope.closeDetails = function () {

        confDetail.modal('hide');
        //$scope.objective = "";
    };

    $scope.getTable = function () {
        $http.get('/api/MatchObjectives/' + $scope.currId).success(function (result) {
            $scope.playersList = result;
        });
    };

    $scope.$on('moveEvent', function() {
        if (WizardHandler.wizard().currentStepNumber() == 1) {
            $scope.getTable();
        }
    });

    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
        $scope.cuurrentMatch = result;
    });

    $scope.addDetails = function (player, objective) {
        $scope.loginLoading = true;
        player.objective = objective;

        //$scope.myform.form_Submitted = !$scope.myform.$valid;    
        $scope.loginLoading = false;
        $http.post('/api/MatchObjectives/', player).success(function () {

            confDetail.modal('hide');
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

    $scope.getTable();
}]);