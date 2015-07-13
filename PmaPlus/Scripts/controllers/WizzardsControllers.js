var app = angular.module('MainApp');



app.controller('WizzardController', ['$scope', '$http', 'toaster', '$location', 'WizardHandler', '$timeout', function ($scope, $http, toaster, $location, WizardHandler, $timeout) {
    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var submitArhive = angular.element('#submitArhive');


    $scope.showArhive = function () {
        submitArhive.modal('show');
    }

    $scope.submitCancel = function () {
        submitArhive.modal('hide');
    }

    $scope.submitOk = function () {
        $http.put('/api/Curriculum/Wizard/Team/Archive/' + $scope.currId).success(function () {
            console.log('ok');
            submitArhive.modal('hide');
        });
    }


    $scope.nav = {};

    $scope.nav.loadedSteps = 0;

    $scope.nav.canNext = true;
    $scope.nav.canBack = true;
    $scope.obj = {
        laddaLoading: false
    }



    $scope.progress = {};

    $http.get('/api/Teams/' + $scope.currId)
        .success(function (data) {
            $scope.teamname = data.name;
            $scope.team = data;
        });

    var getWizard = function () {
        $http.get('/api/Curriculum/Wizard/' + $scope.currId)
       .success(function (data) {
           $scope.steps = data;
           $scope.progress.max = data.length - 1;

           $scope.$watch(
               function () { return $scope.nav.loadedSteps },
               function (loadedSteps) {
                   console.log(loadedSteps);
                   if (loadedSteps == $scope.steps.length) {
                       var last = 0;
                       for (var i = 0; i < $scope.steps.length; i++) {
                           if ($scope.steps[i].done) {
                               last = i;
                           }
                       }

                       WizardHandler.wizard().goTo(last);


                   }
               }
               );



       });
    }
    $scope.$on('wizard:stepChanged', function () {
        $scope.updateProgress();
        if ($scope.steps.length == WizardHandler.wizard().currentStepNumber()) {
            $scope.isLast = true;
        } else {
            $scope.isLast = false;
        }
    });

    $scope.saveProgress = function () {
        $scope.$broadcast('saveProgressEvent');
    };

    $scope.updateProgress = function () {
        $scope.progress.current = WizardHandler.wizard().currentStepNumber() - 1;
        $scope.$broadcast('moveEvent');
    };
    getWizard();
}]);