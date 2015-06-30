var app = angular.module('MainApp');

app.controller('ClubDocumetsController', ['$scope', '$http', 'toaster', '$q', '$filter', function ($scope, $http, toaster, $q, $filter) {

    $scope.roles = [
      { id: 2, name: 'Head Of Academies' },
      { id: 3, name: 'Coach' },
      { id: 4, name: 'Head Of Education' },
      { id: 5, name: 'Welfare Officer' },
      { id: 6, name: 'Scout' },
      { id: 7, name: 'Physio' },
      { id: 8, name: 'Sports Scientist' },
      { id: 9, name: 'Player' }
    ];

    $scope.newDir = {
        name: 'NewFolder',
        roles: []
    }

    $scope.currentFolder = '';
    var folderModal = angular.element('#addFolder');
    var delModalFolder = angular.element('#confDeleteFolder');
    var delModalFile = angular.element('#confDeleteFile');

    $scope.getFolders = function () {
        $http.get('/api/Documents/Directories')
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
            }).success(function (data) {
                toaster.pop({
                    type: 'success',
                    title: 'Success',
                    body: 'File upload Successful!'
                });
                $scope.getFiles($scope.currentFolder);
            }).error(function () {
                toaster.pop({
                    type: 'error',
                    title: 'Error',
                    body: 'File upload ERROR!'
                });
            });
        }
    });

    $scope.confirmDelFolder = function (tFolder) {
        delModalFolder.modal('show');
        $scope.tFolder = tFolder;
    }

    $scope.cancelFolder = function() {
        delModalFolder.modal('hide');
        $scope.tFolder = '';
    };
    $scope.deleteFolder = function (folderName) {
        $http.delete('/api/Documents/' + folderName)
            .success(function () {
                $scope.getFolders();
            });
        delModalFolder.modal('hide');
    };


    $scope.confirmDelFile = function(tFile) {
        delModalFile.modal('show');
        $scope.tFile = tFile;
    };

    $scope.cancelFile = function () {
        delModalFile.modal('hide');
        $scope.tFile = '';
    };

    $scope.deleteFile = function (fileName) {
        $http.delete('/api/Documents/' + $scope.currentFolder + '/' + fileName)
            .success(function () {
                $scope.getFiles($scope.currentFolder);
            });
        delModalFile.modal('hide');
    };

    $scope.newFolder = function () {
        folderModal.modal('show');
    };

    $scope.addFolder = function () {
        $scope.newDir = {
            name: 'NewFolder',
            roles: []
        }
    };

    $scope.ok = function () {
        $http.post('/api/Documents/Directories', $scope.newDir).success(function () {
            $scope.getFolders();
        });
        folderModal.modal('hide');
    };

    $scope.cancel = function () {
        folderModal.modal('hide');

    };
    $scope.getFolders();
}]);