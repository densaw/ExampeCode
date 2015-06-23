var app = angular.module('MainApp');

app.controller('AddReportController', ['$scope', '$http', '$q', '$location', function ($scope, $http, $q, $location) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var addMatchReports = angular.element('#addMatchReports');
    //var cancelInvite = angular.element('#cancelInvite');
    var urlTail = '/api/MatchReports/';

    $scope.newMatch = {};

    $scope.matchType = [
        
        { id: 0, name: 'Cup' },
        { id: 1, name: 'Friendly' },
        { id: 2, name: 'International' },
        { id: 3, name: 'League' },
        { id: 3, name: 'Tournament' }
    ];

    $scope.sideType = [

        { id: 0, name: 's5a_side' },
        { id: 1, name: 's6a_side' },
        { id: 2, name: 's7a_side' },
        { id: 3, name: 's9a_side' },
        { id: 3, name: 's11a_side' }
    ];

    $scope.addMatch = function () {
        $scope.modalTitle = 'Add Match Report';
        addMatchReports.modal('show');
    };

    $scope.cancelMatch = function () {
        addMatchReports.modal('hide');
    }

    $scope.okMatch = function (id) {      
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;
            if (id != null) {
                $scope.loginLoading = false;
                
                $http.put(urlTail + '/' + id, $scope.newMatch).success(function () {
                    //getResultsPage($scope.pagination.current);
                    addMatchReports.modal('hide');
                }).error(function (data, status, headers, config) {
                    if (status == 400) {
                        console.log(data);
                        toaster.pop({
                            type: 'error',
                            title: 'Error', bodyOutputType: 'trustedHtml',
                            body: data.message.join("<br />")
                        });
                    }
                });
            } else {
                $scope.loginLoading = false;
                //$scope.newAttr.type = $scope.selectedType.id;
                $http.post(urlTail, $scope.newMatch).success(function () {
                    //getResultsPage($scope.pagination.current);
                    addMatchReports.modal('hide');
                }).error(function (data, status, headers, config) {
                    if (status == 400) {
                        console.log(data);
                        toaster.pop({
                            type: 'error',
                            title: 'Error', bodyOutputType: 'trustedHtml',
                            body: data.message.join("<br />")
                        });
                    }
                });
            }

        };
    
  
}]);