var app = angular.module('MainApp');

app.controller('WizardPage4Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];





    $scope.getTable = function () {
        $http.get('/api/PlayerMatchStatistic/' + $scope.currId).success(function (result) {
            $scope.playersStat = result;
            var completed = true;
            angular.forEach($scope.playersStat, function (player) {
               
                if (player.formRating < 1 && player.playingTime > 0) {
                    completed = false;
                }

                $scope.checkForMaxDuration(player);

            });

            if (completed) {
                $scope.nav.canNext = true;
            } else { $scope.nav.canNext = false; }
        });
    };


    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 4) {
            $scope.getTable();
            $scope.nav.canNext = false;
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

                if (player.formRating < 1 && player.playingTime > 0) {
                    completed = false;
                }
            });

            if (completed) {
                $scope.nav.canNext = true;
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


    $scope.checkForMaxDuration = function(player){
        if (player.playingTime > ($scope.$parent.cuurrentMatch.periods * $scope.$parent.cuurrentMatch.periodDuration)) {
            player.playingTime = ($scope.$parent.cuurrentMatch.periods * $scope.$parent.cuurrentMatch.periodDuration);
        }
    }
    //ADD==========================================
    $scope.addPlayerStat = function () {
        $scope.$parent.obj.laddaLoading = true;

        $http.post('/api/PlayerMatchStatistic/Table', $scope.playersStat).success(function () {

            $scope.$parent.obj.laddaLoading = false;
        }).error(function (data, status, headers, config) {
            if (status == 400) {
                console.log(data);
                toaster.pop({
                    type: 'error',
                    title: 'Error', bodyOutputType: 'trustedHtml',
                    body: 'Please comptite compulsory fields'
                });
            }
            $scope.$parent.obj.laddaLoading = false;
        });

    };
    //ADD=========================================
}]);