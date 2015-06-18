var app = angular.module('MainApp');



app.controller('WizzardController', ['$scope', '$http', 'toaster', '$location', function ($scope, $http, toaster, $location) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

   


    $http.get('/api/Curriculum/Wizard/' + $scope.currId)
        .success(function (data) {
            $scope.steps = data;
       
    });




    


}]);