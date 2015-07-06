var app = angular.module('MainApp');

app.controller('AddReportController', ['$scope', '$http', '$q', '$location', '$rootScope', function ($scope, $http, $q, $location, $rootScope) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var addMatchReports = angular.element('#addMatchReports');
    //var cancelInvite = angular.element('#cancelInvite');
    var urlTail = '/api/MatchReports';
   

    $scope.newMatch = {};

    $scope.teamList = [];

    

    $http.get('/api/Teams/Coach/List').success(function (result) {
        $scope.teamList = result;
    });

    $scope.matchType = [
        
        { id: 0, name: 'Cup' },
        { id: 1, name: 'Friendly' },
        { id: 2, name: 'International' },
        { id: 3, name: 'League' },
        { id: 3, name: 'Tournament' }
    ];

    $scope.sideType = [

        { id: 0, name: '5A' },
        { id: 1, name: '6A' },
        { id: 2, name: '7A' },
        { id: 3, name: '9A' },
        { id: 3, name: '11A' }
    ];

    $scope.addMatch = function () {
        $scope.newMatch = {};
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
                    getResultsPage($scope.pagination.current);
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
                    getResultsPage($scope.pagination.current);
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

    var sortArray = [];
    $scope.items = [];
    $scope.totalItems = 0;
    $scope.itemsPerPage = 20;
    $scope.flag = true;

    $scope.pagination = {
        current: 1
    };

    function createTail(pageNumber) {
        if (sortArray.length > 0) {
            return urlTail +  '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
        } else {
            return urlTail +  '/' + $scope.itemsPerPage + '/' + pageNumber;
        }
    }

    function getResultsPage(pageNumber) {
        $http.get(createTail(pageNumber))
            .success(function (result) {
                console.log(result);
                $scope.items = result.items;
                $scope.totalItems = result.count;

            });
    }

    $rootScope.$watchGroup(['orderField', 'revers'], function (newValue, oldValue, scope) {
        sortArray = newValue;
        $http.get(createTail($scope.pagination.current))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    });

    


    getResultsPage($scope.pagination.current);

    $scope.pageChanged = function (newPage) {
        getResultsPage(newPage);
        $scope.pagination.current = newPage;
    };

    var matchReportsResult = angular.element('#matchReportsResult');

    

    $scope.reportResult = function (id) {
        $http.get('/api/MatchReports/'+ id).success(function (result) {
            $scope.matchReport = result;
            $scope.modalTitle = "Edit";
            matchReportsResult.modal('show');
        });
        $http.get('/api/PlayerMatchStatistic/' + id).success(function (result) {
            $scope.reportTable = result;
        });
    };

    $scope.closeResult = function () {
        matchReportsResult.modal('hide');
    }
    
    
}]);