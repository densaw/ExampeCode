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
    $scope.progress = {}

    $http.get('/api/Teams/' + $scope.currId)
        .success(function (data) {
            $scope.teamname = data.name;
           
        });

    $http.get('/api/Curriculum/Wizard/' + $scope.currId)
        .success(function(data) {
            $scope.steps = data;
            $scope.progress.max = data.length -1;
        });

    $scope.saveProgress = function() {
            $scope.$broadcast ('saveProgressEvent');
    };

    $scope.updateProgress = function() {
        $scope.progress.current = WizardHandler.wizard().currentStepNumber() - 1;
    };
}]);