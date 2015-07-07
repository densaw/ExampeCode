var app = angular.module('MainApp');

app.controller('CurrReportPeriodController', ['$scope', '$http', '$location', 'WizardHandler', function ($scope, $http, $location, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $http.get('/api/CurriculumStatement/List').success(function (data) {
        $scope.statemants = data;
    });

    var getTable = function () {

        $http.get('/api/Curriculum/Wizard/Session/BlockObjectiveTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId)
            .success(function (result) {
                $scope.items = result;
            });
    }

    var savePeriod = function () {
        $scope.$parent.obj.laddaLoading = true;
        $http.post('/api/Curriculum/Wizard/Session/BlockObjectiveTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId, $scope.items)
            .success(function() {
                $scope.$parent.obj.laddaLoading = false;
            }).error(function() {
                $scope.$parent.obj.laddaLoading = false;
        });

    };

    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {

            savePeriod();

        }
    });

    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            $scope.nav.canNext = true;
            $scope.nav.canBack = true;

            getTable();
        }
    });

    $scope.addObjective = function (player) {
        angular.element('#reportPeriodModal').appendTo("body").modal('show');
        $scope.player = player;

    }



    $scope.saveObjective = function () {
        angular.element('#reportPeriodModal').modal('hide');
    }

    //Next step allow
    $scope.canNext = function () {
        return true;
    };


}]);