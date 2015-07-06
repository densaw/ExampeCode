var app = angular.module('MainApp');


app.controller('ProfileController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {


    $http.get('/api/TrainingTeamMembers/-1').success(function (result) {
        $scope.newMember = result;
    });
    
}]);