var app = angular.module('MainApp');

app.controller('WizardPage3endController', ['$scope', '$http', '$q', '$location', '$rootScope', 'toaster', 'WizardHandler', function ($scope, $http, $q, $location, $rootScope, toaster, WizardHandler) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    $scope.$on('moveEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 3) {
            if ($scope.$parent.cuurrentMatch.periods < 1 || $scope.$parent.cuurrentMatch.periodDuration < 1) {
                $scope.nav.canNext = false;
            } else {
                $scope.nav.canNext = true;
            }

            $scope.nav.canBack = true;
            $scope.nav.last = false;
        }
    });

    $scope.$on('saveProgressEvent', function () {
        if (WizardHandler.wizard().currentStepNumber() == 3) {
            if (!$scope.$parent.cuurrentMatch.archived) {
                if (!$scope.matchDetailsForm.$valid) {
                    $scope.matchDetailsForm.form_Submitted = !$scope.matchDetailsForm.$valid;
                } else {
                    $scope.addMatchDetails();
                }
            }
        }
    });



    $scope.addMatchDetails = function () {
        $scope.$parent.obj.laddaLoading = true;


        var promises = [];


        if ($scope.pic) {

            var fd = new FormData();
            fd.append('file', $scope.pic);
            var promise = $http.post('/api/Files', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
                .success(function (data) {
                    $scope.$parent.cuurrentMatch.picture = data.name;
                    angular.element('.pma-fileupload').fileinput('clear');
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
            $http.put('/api/MatchReports/' + $scope.currId, $scope.$parent.cuurrentMatch)
                .success(function (result) {
                    $scope.$parent.obj.laddaLoading = false;
                    $http.get('/api/MatchReports/' + $scope.currId).success(function (result) {
                        $scope.$parent.cuurrentMatch.picture = result.picture;
                    });
                }).error(function (data, status, headers, config) {

                    if (status == 400) {
                        console.log(data);
                        toaster.pop({
                            type: 'error',
                            title: 'Error', bodyOutputType: 'trustedHtml',
                            body: 'Please complete the compulsory fields highlighted in red'
                        });
                    }
                    $scope.$parent.obj.laddaLoading = false;
                });




        });

    };
}]);