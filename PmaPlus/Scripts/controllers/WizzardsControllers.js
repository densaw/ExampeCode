var app = angular.module('MainApp');



app.controller('WizzardController', ['$scope', '$http', 'toaster', '$location', 'WizardHandler', '$timeout', function ($scope, $http, toaster, $location, WizardHandler, $timeout) {

    $scope.nav = {};

    $scope.nav.canNext = true;
    $scope.nav.canBack = true;
    $scope.obj = {
        laddaLoading: false
    }

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $scope.progress = {};

    $http.get('/api/Teams/' + $scope.currId)
        .success(function (data) {
            $scope.teamname = data.name;

        });

    var getWizard = function () {
        $http.get('/api/Curriculum/Wizard/' + $scope.currId)
       .success(function (data) {
           $scope.steps = data;
           $scope.progress.max = data.length - 1;

           $scope.$watch(function () {
               return WizardHandler.wizard();
           }, function (wizard) {
               if (wizard) {
                   $timeout(function () {
                       var last = 0;
                       for (var i = 0; i < $scope.steps.length; i++) {
                           if ($scope.steps[i].done) {
                               last = i;
                           }
                       }
                       wizard.goTo(last);
                       $scope.updateProgress();

                   }, 1000);

               }
           });
       });
    }


    $scope.saveProgress = function () {
        $scope.$broadcast('saveProgressEvent');
    };

    $scope.updateProgress = function () {
        $scope.progress.current = WizardHandler.wizard().currentStepNumber() - 1;
        $scope.$broadcast('moveEvent');
        console.log('aaaa');
    };
    getWizard();
}]);