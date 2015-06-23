var app = angular.module('MainApp');

app.controller('AtendanceController', ['$scope', '$http', '$location', 'WizardHandler', function ($scope, $http, $location, WizardHandler) {

    // get att step 2
    var confDetail = angular.element('#confDetail');
    var confAtend = angular.element('#confAtend');


    

    $scope.date = new Date();
    

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $http.get('/api/Curriculum/Wizard/Session/AttendanceTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId)
        .success(function (result) {
            $scope.items = result;

        });
   

    var savePeriod = function () {
        $http.post('/api/Curriculum/Wizard/Session/AttendanceTable/' + $scope.currId + '/' + $scope.$parent.step.sessionId, $scope.items)
            .success(function () {

            });
    }

    $scope.ssesionDetail = function () {
        $scope.modalTitle = "Details";
        //confDetail.modal('show');
        $('#confDetail').appendTo("body").modal('show');
    };
    
   

    $scope.confAtend = function () {
        $scope.modalTitle = "Details";
        //confAtend.modal('show');
        $('#confAtend').appendTo("body").modal('show');
    };

    $scope.addDetails = function ()
    { confDetail.modal('hide'); }
    $scope.closeDetails = function ()
    { confDetail.modal('hide'); }

    $scope.attendense = [
       { id: 0, name: 'Attended' },
       { id: 1, name: 'NotAttended' },
       { id: 2, name: 'Holidays' },
       { id: 3, name: 'Injured' },
       { id: 4, name: 'School' },
       { id: 5, name: 'Sick ' },
       { id: 6, name: 'OtherTraining ' },
       { id: -1, name: '' }
       
    ];
    //$scope.attendenseVisible = $scope.attendense[0];

    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == $scope.$parent.steps.indexOf($scope.$parent.step) + 1) {
            savePeriod();
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
        }
    });




    $scope.okAtt = function () {

        angular.forEach($scope.items, function (item) {
            item.attendance = 0;
            item.duration = $scope.addDate.duration;
        });
        confAtend.modal('hide');
        $scope.nav.canNext = true;
        $scope.nav.canBack = true;
    };

    $scope.cancelAtt = function () {
        confAtend.modal('hide');
    };



    $scope.ssesionNotCompletedModal = function() {
        angular.element('#confNotCompl').appendTo('body').modal('show');
    }

    $scope.ssesionNotCompleted = function() {
        angular.element('#confNotCompl').modal('hide');
        angular.forEach($scope.items, function(item) {
            item.attendance = 1;
            item.duration = 0;
        });
        $scope.nav.canNext = true;
        $scope.nav.canBack = true;
    };

}]);