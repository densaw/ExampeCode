var app = angular.module('MainApp');

app.controller('UserAvatarController', ['$scope', '$http',  '$q', function($scope, $http, $q) {
    

    var getAvatar =function() {
        $http.get("/api/Users/Avatar")
            .success(function(result) {
            $scope.avatar = result;
        });
    }


    getAvatar();
}]);