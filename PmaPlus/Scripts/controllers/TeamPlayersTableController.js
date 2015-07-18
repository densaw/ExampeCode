var app = angular.module('MainApp');


app.controller('TeamPlayersTableController', ['$scope', '$http', '$location', '$rootScope', '$filter', function ($scope, $http, $location, $rootScope, $filter) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];
    $scope.players = {};
    var sortArray = [];

  
    
    

    var urlTail = '/api/Curriculum/Players/Statistic/' + $scope.currId;

    

   

    function createTail(pageNumber) {
        if (sortArray.length > 0) {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
        } else {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber;
        }
    };

    function getResultsPage(pageNumber) {
        $http.get(createTail(pageNumber))
            .success(function (result) {
                $scope.items = result;
                $scope.totalItems = result.count;
            });
    };

    $scope.items = [];
    $scope.totalItems = 0;
    $scope.itemsPerPage = 20;

    
   

    

    $rootScope.$watchGroup(['orderField', 'revers'], function (newValue, oldValue, scope) {
        sortArray = newValue;
        $http.get(createTail($scope.pagination.current))
            .success(function (result) {
                $scope.items = result;
                $scope.totalItems = result.count;
            });
    });

    $scope.pagination = {
        current: 1
    };

    getResultsPage($scope.pagination.current);
    $scope.pageChanged = function (newPage) {
        getResultsPage(newPage);
        $scope.pagination.current = newPage;
    };

    getResultsPage();


}]);