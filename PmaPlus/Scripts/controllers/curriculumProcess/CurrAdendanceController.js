var app = angular.module('MainApp');

app.controller('AtendanceController', ['$scope', '$http', '$location', 'WizardHandler', function ($scope, $http, $location, WizardHandler) {

    // get att step 2
    //var confDetail = angular.element('#confDetail');
    //var confAtend = angular.element('#confAtend');
    //var notComplSes = angular.element('#confNotCompl');

    $scope.confDetail = false;
    $scope.confAtend = false;
    $scope.notComplSes = false;


    $scope.date = new Date();

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var getTable = function () {

        $http.get('/api/Curriculum/Wizard/Session/AttendanceTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId)
            .success(function (result) {
                $scope.items = result;
            });
    }

    var savePeriod = function () {
        $scope.$parent.obj.laddaLoading = true;
        $http.post('/api/Curriculum/Wizard/Session/AttendanceTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId, $scope.items)
             .success(function () {
                 $http.post('/api/Curriculum/Wizard/Session/Save/' + $scope.currId + '/' + $scope.$parent.step.sessionId);
                 $scope.$parent.obj.laddaLoading = false;
                 $scope.$parent.step.done = true;
                 $scope.nav.canNext = true;
                 $scope.nav.canBack = true;
             }).error(function () {
                 $scope.$parent.obj.laddaLoading = false;
             });
    }

    $scope.ssesionDetail = function () {
        $scope.modalTitle = "Details";
        $scope.confDetail = true;
    };
    $scope.saveDetails = function () {
        $scope.confDetail = false;
    }
    $scope.closeDetails = function () {
        $scope.addDate = null;
        $scope.confDetail = false;
    }

    $scope.attendense = [
       { id: 0, name: 'Attended' },
       { id: 1, name: 'Not Attended' },
       { id: 2, name: 'Holidays' },
       { id: 3, name: 'Injured' },
       { id: 4, name: 'School' },
       { id: 5, name: 'Sick' },
       { id: 6, name: 'Other' },
       { id: 7, name: 'Training ' },
       { id: -1, name: '' }

    ];

    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            var completed = true;
            angular.forEach($scope.items, function (item) {
                if (item.attendance == -1) {
                    completed = false;
                }
            });
            if (completed) {
                savePeriod();
            } else {
                $scope.pressed = true;
            }
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


    $scope.confAtendOpen = function () {
        $scope.modalTitle = "Details";
        $scope.confAtend = true;
    };
    $scope.okAtt = function () {
        angular.forEach($scope.items, function (item) {
            item.attendance = 0;
            item.duration = $scope.addDate.duration;
        });
        $scope.confAtend = false;
    };

    $scope.cancelAtt = function () {
        $scope.confAtend = false;
    };

    $scope.ssesionNotCompletedModal = function () {
        $scope.notComplSes = true;
    }

    $scope.ssesionNotCompletedCancel = function () {
        $scope.notComplSes = false;
    }

    $scope.ssesionNotCompleted = function () {
        angular.forEach($scope.items, function (item) {
            item.attendance = 0;
            item.duration = 0;
        });
        $scope.notComplSes = false;
    };
    $scope.nav.loadedSteps++;
}]);