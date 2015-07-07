var app = angular.module('MainApp');

app.controller('CurrStartPeriodController', ['$scope', '$http', '$location', 'WizardHandler', function ($scope, $http, $location, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $http.get('/api/CurriculumStatement/List').success(function (data) {
        $scope.statemants = data;
    });



    var getTable = function () {

        $http.get('/api/Curriculum/Wizard/Session/BlockObjectiveTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId)
            .success(function (result) {
                $scope.items = result;
                console.log(result);
            });

    }


    
    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            $scope.nav.canNext = true;
            $scope.nav.canBack = true;
        }
    });

    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            $scope.nav.canNext = false;
            $scope.nav.canBack = true;

            getTable();
        }
    });



    $scope.addObjective = function (player) {
        angular.element('#objModal').appendTo("body").modal('show');
        $scope.player = player;

    }



    $scope.saveObjective = function (player) {
        $http.post('/api/Curriculum/Wizard/Session/BlockObjective/' + $scope.currId + '/' + $scope.$parent.step.sessionId, player)
          .success(function () {
          }).error(function () {
          });
        angular.element('#objModal').modal('hide');
    }

    //Next step allow
    $scope.canNext = function () {
        return true;
    };


}]);