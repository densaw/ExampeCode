var app = angular.module('MainApp');

app.controller('CurrObjectiveController', ['$scope', '$http', '$location', function($scope, $http, $location) {
    
    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $http.get('/api/Curriculum/Wizard/Session/ObjectiveTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId)
        .success(function (result) {
            $scope.items = result;

        });
    $scope.saveObjectives = function() {
        $http.post('/api/Curriculum/Wizard/Session/ObjectiveTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId,$scope.items)
            .success(function() {

            });

    };

    $scope.$on('saveProgressEvent', function () {
        console.log('Objective controller');
    });



}]);