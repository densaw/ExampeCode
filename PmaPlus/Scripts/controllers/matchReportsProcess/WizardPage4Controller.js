var app = angular.module('MainApp');

app.controller('WizardPage4Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', function ($scope, $http, $q, $location, $rootScope, toaster) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var toggleInvite = angular.element('#toggleMoM');
    
    var confInvite = angular.element('#confInvite');
    $scope.playerAdd = {};

    $scope.openEdit1 = function (player) {

        //toggleInvite.bootstrapToggle($scope.profileTalents.invitedToTrial ? 'on' : 'off');
        $scope.player = player;
        $scope.modalTitle = "Edit";
        confInvite.modal('show');
    };

    $scope.closeDetails = function () {
        confInvite.modal('hide');
    };

    $http.get('/api/PlayerMatchStatistic/' + $scope.currId).success(function (result) {
        $scope.playersStat = result;       
    });

    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
        $scope.cuurrentMatch = result;
    });



    //ADD==========================================
    $scope.addPlayerStat = function () {
        
        $scope.loginLoading = true;
        //$scope.myform.form_Submitted = !$scope.myform.$valid;    
        $scope.loginLoading = false;
        $http.post('/api/PlayerMatchStatistic/', $scope.player).success(function () {
            
            $scope.playerAdd = {};
            confInvite.modal('hide');
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
    //ADD=========================================
}]);