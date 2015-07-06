var app = angular.module('MainApp');


app.controller('ProfileController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {


    var confirmModal = angular.element('#confirmModal');

    function getProfile() {
        $http.get('/api/TrainingTeamMembers/-1').success(function (result) {
            $scope.newMember = result;
        });
    };
    getProfile();

    $scope.save = function () {
        confirmModal.modal('show');

    };
    $scope.cancel = function () {
        confirmModal.modal('hide');

    };

    $scope.okay = function (id) {
        console.log('test');
        $scope.loginLoading = true;
        
        if (id != null) {

            //PUT it now have no url to Update date
            $http.put('/api/TrainingTeamMembers/' + id, $scope.newMember).success(function (result) {
                
                $scope.loginLoading = false;
                confirmModal.modal('hide');
                getProfile();
            }).error(function (data, status, headers, config) {

            });
        }
    };


}]);