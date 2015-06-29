var app = angular.module('MainApp');

app.controller('ClubDocumetsController', ['$scope', '$http', 'toaster', '$q', '$filter', function ($scope, $http, toaster, $q, $filter) {

    $scope.currentFolder = '';
    var folderModal = angular.element('#addFolder');
    var delModal = angular.element('#confDelete');

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
    };

    


    $scope.deleteFolder = function (folderName) {
        $http.delete('/api/Documents/' + folderName)
            .success(function () {
                $scope.getFolders();
            });
    };

    $scope.deleteFile = function (fileName) {
        $http.delete('' + $scope.currentFolder + '/' + fileName)
            .success(function () {
                $scope.getFiles($scope.currentFolder);
            });
    };

    $scope.newFolder = function() {
        folderModal.modal('show');
    };

    $scope.getFolders();
}]);