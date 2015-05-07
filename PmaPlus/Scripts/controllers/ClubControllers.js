var app = angular.module('MainApp');

app.controller('AttributesController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {

    var needToDelete = -1;
    var urlTail = '/api/Attributes';

    $scope.attrTypes = [
       { id: 0, name: 'Yes/No' },
       { id: 1, name: 'Rating' }
    ];

    $scope.selectedType = $scope.attrTypes[0];

    

    function getResultsPage(pageNumber) {
        $http.get(urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber)
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    }

    $scope.items = [];
    $scope.totalItems = 0;
    $scope.itemsPerPage = 20;
    $scope.newAttr = {};

    $scope.pagination = {
        current: 1
    };
    getResultsPage($scope.pagination.current);
    $scope.pageChanged = function (newPage) {
        getResultsPage(newPage);
        $scope.pagination.current = newPage;
    };

    var target = angular.element('#addAttrModal');
    var confDelete = angular.element('#confDelete');
    var maxScore = angular.element('#maxScore');

    $scope.$watch('selectedType', function (st) {
        if (st.id == 0) {
            maxScore.prop('disabled', true);
        } else {
            maxScore.prop('disabled', false);
        }
    });

    $scope.ok = function (id) {
        $scope.myform.form_Submitted = !$scope.myform.$valid;

        if (id != null) {

            $scope.newAttr.type = $scope.selectedType.id;
            $http.put(urlTail + '/' + id, $scope.newAttr).success(function () {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
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
            $scope.newAttr.type = $scope.selectedType.id;
            $http.post(urlTail, $scope.newAttr).success(function () {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
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
    $scope.cancel = function () {
        target.modal('hide');
        confDelete.modal('hide');
    };
    $scope.openAdd = function () {
        $scope.modalTitle = "Add an Attribute";
        $scope.newAttr = {};
        $scope.myform.form_Submitted = false;
        target.modal('show');
    };
    $scope.openDelete = function (id) {
        confDelete.modal('show');
        console.log(id);
        needToDelete = id;
    };
    $scope.delete = function () {
        $http.delete(urlTail + '/' + needToDelete).success(function () {
            getResultsPage($scope.pagination.current);
            needToDelete = -1;
            confDelete.modal('hide');
        });
    };
    $scope.openEdit = function (id) {
        $http.get(urlTail + '/' + id)
            .success(function (result) {
                $scope.newAttr = result;
                $scope.selectedType = $scope.attrTypes[result.type];
                $scope.modalTitle = "Update an Attribute";
                target.modal('show');

            });
    };
}]);

