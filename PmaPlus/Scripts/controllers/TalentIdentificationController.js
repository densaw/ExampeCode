var app = angular.module('MainApp');

app.controller('TalentIdentificationController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {

    var target = angular.element('#addTalent');


    $scope.open = function () {
        $scope.windowTitle = 'Add Scouted Player';
        $scope.myform.form_Submitted = false;
        target.modal('show');
    }

    $scope.cancel = function () {
        target.modal('hide');
        confDelete.modal('hide');
    };

}]);