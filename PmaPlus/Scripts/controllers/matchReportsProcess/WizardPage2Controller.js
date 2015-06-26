var app = angular.module('MainApp');

app.controller('WizardPage2Controller', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', function ($scope, $http, $q, $location, $rootScope, toaster) {

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
  

    $http.get('/api/MatchObjectives/' + $scope.currId).success(function (result) {
        $scope.playersList = result;
        console.log(result);
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

}]);