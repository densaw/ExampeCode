var app = angular.module('MainApp');

app.controller('UsersDocumetsController', ['$scope', '$http', 'toaster', '$q', '$filter', function ($scope, $http, toaster, $q, $filter) {



    $scope.currentFolder = '';
    var folderModal = angular.element('#addFolder');
    var delModal = angular.element('#confDelete');

    $scope.getFolders = function () {
        $http.get('/api/Documents/Directories/Shared')
         .success(function (data) {
             $scope.folders = data;
             if (data.length > 0) {
                 $scope.getFiles(data[0].name);
                 $scope.currentFolder = data[0].name;
             }
         });
    };



    $scope.getFiles = function (folderName) {
        $http.get('/api/Documents/' + folderName)
            .success(function (data) {
                $scope.files = data;
            });
        $scope.currentFolder = folderName;
    };

    $scope.$watch('uploadFile', function (nfile) {
        if (nfile) {
            var fd = new FormData();
            fd.append('file', nfile);
            $http.post('/api/Documents/' + $scope.currentFolder, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
                .success(function (data) {
                    toaster.pop({
                        type: 'success',
                        title: 'Success',
                        body: 'File upload Successful!'
                    });
                    $scope.getFiles($scope.currentFolder);
                })
                .error(function () {
                    toaster.pop({
                        type: 'error',
                        title: 'Error',
                        body: 'File upload ERROR!'
                    });
                });
        }
    });

    $scope.getFolders();
}]);