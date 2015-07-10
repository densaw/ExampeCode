var app = angular.module('MainApp');

app.controller('CurrObjectiveController', ['$scope', '$http', '$location', 'WizardHandler', function ($scope, $http, $location, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var getTable = function () {

        $http.get('/api/Curriculum/Wizard/Session/StartObjectiveTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId)
        .success(function (result) {
            $scope.items = result;
        });

    }
    var saveObjectives = function () {
        $scope.$parent.obj.laddaLoading = false;
        $http.post('/api/Curriculum/Wizard/Session/StartObjectiveTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId, $scope.items)
            .success(function () {
                $http.post('/api/Curriculum/Wizard/Session/Save/' + $scope.currId + '/' + $scope.$parent.step.sessionId);
                $scope.$parent.obj.laddaLoading = false;
                $scope.nav.canNext = true;
            }).error(function () {
                $scope.$parent.obj.laddaLoading = false;
            });

    };

    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            var completed = true;
            angular.forEach($scope.items, function (item) {
                if (!item.objective) {
                    completed = false;
                } else {
                    if (item.objective.length < 1) {
                        completed = false;
                    }
                }
            });

            if (completed) {
                saveObjectives();
            } else {$scope.pressed = true;}
        }
    });


    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            if ($scope.$parent.step.done) {
                $scope.nav.canNext = true;
                $scope.nav.canBack = true;
            } else {
                $scope.nav.canNext = false;
            }
            getTable();
        }
    });


    $scope.nav.loadedSteps++;
}]);