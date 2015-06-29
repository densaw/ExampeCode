var app = angular.module('MainApp');

app.controller('WizardPage4Controller', ['$scope', '$http', '$q', '$location', '$rootScope', function ($scope, $http, $q, $location, $rootScope) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var confDetail1 = angular.element('#confDetail1');

    $scope.openEdit1 = function () {
        $scope.modalTitle = "Edit";
        confDetail1.modal('show');
    };

    $scope.closeDetails = function () {

        confDetail1.modal('hide');
        //$scope.objective = "";
    };

    $http.get('/api/MatchObjectives/' + $scope.currId).success(function (result) {
        $scope.playersList = result;
        console.log(result);
    });

}]);