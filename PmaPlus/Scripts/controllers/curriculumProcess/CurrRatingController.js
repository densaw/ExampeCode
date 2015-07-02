var app = angular.module('MainApp');



app.controller('CurrRatingController', ['$scope', '$http', '$location', 'WizardHandler', function ($scope, $http, $location, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];




    var getTable = function () {

        $http.get('/api/Curriculum/Wizard/Session/RatingTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId)
          .success(function (result) {
              $scope.items = result;
          });

    }



    var saveRatings = function () {
        $http.post('/api/Curriculum/Wizard/Session/RatingTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId, $scope.items)
            .success(function () {

            });

    };



    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            saveRatings();
            $scope.nav.canNext = true;
            $scope.nav.canBack = true;

        }
    });


    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            if ($scope.$parent.step.done) {
                $scope.nav.canNext = true;
                $scope.nav.canBack = true;
            } else {
                $scope.nav.canNext = false;
                $scope.nav.canBack = false;
            }

            getTable();
        }
    });


}]);