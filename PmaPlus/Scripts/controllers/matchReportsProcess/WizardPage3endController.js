var app = angular.module('MainApp');

app.controller('WizardPage3endController', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', function ($scope, $http, $q, $location, $rootScope, toaster) {

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
        //$scope.objective = "";
    };

    

    $http.get('/api/MatchObjectives/' + $scope.currId).success(function (result) {
        $scope.playersList = result;
        console.log(result);
    });

    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
        $scope.cuurrentMatch = result;
        console.log('pre');
        console.log(result);
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