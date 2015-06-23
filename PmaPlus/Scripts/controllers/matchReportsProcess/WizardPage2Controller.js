var app = angular.module('MainApp');

app.controller('WizardPage2Controller', ['$scope', '$http', '$q', '$location', '$rootScope',  function ($scope, $http, $q, $location, $rootScope) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var confDetail = angular.element('#confDetail');

    $scope.openEdit = function () {
        $scope.modalTitle = "Edit";
        confDetail.modal('show');
    };

    

    $http.get('/api/MatchObjectives/' + $scope.currId).success(function (result) {
        $scope.playersList = result;
        console.log(result);
    });

}]);