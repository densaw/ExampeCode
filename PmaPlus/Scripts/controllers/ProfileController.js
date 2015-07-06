var app = angular.module('MainApp');


app.controller('ProfileController', ['$scope', '$http', 'toaster', '$q', function ($scope, $http, toaster, $q) {


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
        $scope.loginLoading = true;
        $scope.myform.form_Submitted = !$scope.myform.$valid;
        //Files upload

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
                    $scope.newMember.profilePicture = data.name;
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

                $http.put('/api/TrainingTeamMembers/' + id, $scope.newMember)
                .success(function (result) {
                    getProfile();
                    confirmModal.modal('hide');
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
    getProfile();

}]);

app.controller('PlayerProfileController', ['$scope', '$http', 'toaster', '$q', function ($scope, $http, toaster, $q) {


    var confirmModal = angular.element('#confirmModal');

    function getProfile() {
        $http.get('/api/Player/-1').success(function (result) {
            $scope.newPlayer = result;
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
        $scope.loginLoading = true;
        $scope.myform.form_Submitted = !$scope.myform.$valid;
        //Files upload

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
                    $scope.newPlayer.profilePicture = data.name;
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
            if (id != null) {

                $http.put('/api/TrainingTeamMembers/' + id, $scope.newPlayer)
                .success(function (result) {
                    getProfile();
                    confirmModal.modal('hide');
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
    getProfile();

}]);