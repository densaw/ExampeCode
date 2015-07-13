var app = angular.module('MainApp');

app.controller('WizardPage3endController', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var confConfirm1 = angular.element('#confConfirm1');

    $scope.confirmModal1 = function () {
        confConfirm1.modal('show');
    }

    $scope.confirmCancel = function () {
        confConfirm1.modal('hide');
    }

    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
        $scope.matchDetails = result;
    });
  

    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 4) {
            $scope.nav.canNext = true;
            $scope.nav.canBack = true;
            $scope.nav.last = false;
            $scope.addMatchDetails();
        }
    });

    //$scope.addMatchDetails = function () {
    //    $scope.loginLoading = true;
    //    //$scope.myform.form_Submitted = !$scope.myform.$valid;    
    //    $scope.loginLoading = false;
    //    $http.put('/api/MatchReports/' + $scope.currId, $scope.matchDetails).success(function () {
    //        confConfirm1.modal('hide');
    //    }).error(function (data, status, headers, config) { });
    //};

    $scope.addMatchDetails = function (id) {
        $scope.loginLoading = true;
        $scope.myform.form_Submitted = !$scope.myform.$valid;    
        $scope.loginLoading = false;
        var promises = [];


        if ($scope.pic) {
            $scope.loginLoading = false;
            var fd = new FormData();
            fd.append('file', $scope.pic);
            var promise = $http.post('/api/Files', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
                .success(function (data) {
                    $scope.cuurrentMatch.picture = data.name;
                })
                .error(function () {
                    toaster.pop({
                        type: 'error',
                        title: 'Error',
                        body: 'File upload ERROR!'
                    });
                });
            promises.push(promise);
        }
        $q.all(promises).then(function () {
            //$scope.newMember.role = $scope.selectedRole.id;
            //$scope.newMember.needReport = needstoReport.prop('checked');
            //$scope.newMember.profilePicture = 'tmp.png';

            if (id != null) {

                $http.put('/api/MatchReports/' + +$scope.currId, $scope.matchDetails)
                .success(function (result) {
                    $scope.loginLoading = false;
                }).error(function (data, status, headers, config) {
                    console.log(data);
                    if (status == 400) {
                        console.log(data);
                        toaster.pop({
                            type: 'error',
                            title: 'Error', bodyOutputType: 'trustedHtml',
                            body: 'Please complete the compulsory fields highlighted in red'
                        });
                    }
                    $scope.loginLoading = false;
                });


            }

        });

    };
}]);