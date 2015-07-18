var app = angular.module('MainApp');

app.controller('WizardPage4Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];





    $scope.getTable = function () {
        $http.get('/api/PlayerMatchStatistic/' + $scope.currId).success(function (result) {
            $scope.playersStat = result;
        });
    };


    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 4) {
            $scope.getTable();
            $scope.nav.canNext = true;
            $scope.nav.canBack = true;
            $scope.nav.last = false;
        }
    });

    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 4) {
            var completed = true;
            angular.forEach($scope.playersStat, function (player) {
                if (player.formRating < 1 && player.formRating > 10) {
                    completed = false;
                }
            });

            if (completed) {
                $scope.addPlayerStat();
            } else { $scope.pressed = true; }
        }

    });

    $scope.updateMom = function (player) {
        if (player.mom) {
            angular.forEach($scope.playersStat, function (item) {
                if (item.playerId != player.playerId) {
                    item.mom = false;
                }

            });
        }



    }

    //ADD==========================================
    $scope.addPlayerStat = function () {


        $http.post('/api/PlayerMatchStatistic/Table', $scope.playersStat).success(function () {


        }).error(function (data, status, headers, config) {
            if (status == 400) {
                console.log(data);
                toaster.pop({
                    type: 'error',
                    title: 'Error', bodyOutputType: 'trustedHtml',
                    body: 'Please comptite compulsory fields'
                });
            }
        });

    };
    //ADD=========================================
}]);