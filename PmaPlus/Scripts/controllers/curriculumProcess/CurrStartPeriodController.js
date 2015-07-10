﻿var app = angular.module('MainApp');

app.controller('CurrStartPeriodController', ['$scope', '$http', '$location', 'WizardHandler', function ($scope, $http, $location, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $http.get('/api/CurriculumStatement/List').success(function (data) {
        $scope.statemants = data;
    });



    var getTable = function () {
        $http.get('/api/Curriculum/Wizard/Session/StartBlockObjectiveTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId)
            .success(function (result) {
                $scope.items = result;
            });
    }



    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {

            var completed = true;
            angular.forEach($scope.items, function (item) {
                if (!item.preObjective) {
                    completed = false;
                } else {
                    if (item.preObjective.length < 1) {
                        completed = false;
                    }
                }
            });
            if (completed) {
                $scope.saveObjective();
                $scope.nav.canNext = true;
                $scope.nav.canBack = true;

            } else {
                $scope.pressed = true;
            }
        }
    });

    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            if ($scope.$parent.step.done) {
                $scope.nav.canNext = true;
            } else {
                $scope.nav.canNext = false;
            }

            $scope.nav.canBack = true;
            getTable();
        }
    });



    $scope.saveObjective = function () {
        $http.post('/api/Curriculum/Wizard/Session/StartBlockObjectiveTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId, $scope.items)
          .success(function () {
              $http.post('/api/Curriculum/Wizard/Session/Save/' + $scope.currId + '/' + $scope.$parent.step.sessionId);
              $scope.$parent.step.done = true;
              $scope.nav.canNext = true;
              $scope.nav.canBack = true;
          }).error(function () {
          });
    }


    $scope.nav.loadedSteps++;

}]);