var app = angular.module('MainApp');

app.controller('WizardPage3Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var confDetail = angular.element('#confDetail2');

    $scope.openEdit = function (player) {
        $scope.modalTitle = "Edit";
        $scope.player = player;
        $scope.playerName = player.playerName;
        $scope.objective = player.objective;
        $scope.outcome = player.outcome;
        confDetail.modal('show');
    };

    $scope.closeDetails = function () {
        confDetail.modal('hide');
    };

    $scope.getTable = function () {
        $http.get('/api/MatchObjectives/' + $scope.currId).success(function (result) {
            $scope.playersList = result;
        });
    };

    

    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 2) {
            $scope.getTable();
            $scope.nav.canNext = true;
            $scope.nav.canBack = true;
            $scope.nav.last = false;
        }
    });

    $scope.addDetails = function (player, outcome) {
        $scope.loginLoading = true;

        player.outcome = outcome;
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

}]);