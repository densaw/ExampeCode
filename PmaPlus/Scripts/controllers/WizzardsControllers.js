var app = angular.module('MainApp');



app.controller('WizzardController', ['$scope', '$http', 'toaster', '$location', 'WizardHandler', function ($scope, $http, toaster, $location, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];


    //$scope.$watch(function () {
    //    return WizardHandler.wizard();
    //}, function (wizard) {
    //    if (wizard) {
            
           
    //    console.log(wizard);
    //    }
    //});
    //$scope.progress = {
    //    max: 1,
    //    current: 0
    //};

    $http.get('/api/Curriculum/Wizard/' + $scope.currId)
        .success(function(data) {
            $scope.steps = data;
            $scope.progress.max = data.length;
        });

    $scope.updateProgress = function() {
        $scope.progress.current = WizardHandler.wizard().currentStepNumber();
        console.log($scope.progress.current);
    };
}]);