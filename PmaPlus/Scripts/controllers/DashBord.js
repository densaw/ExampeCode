exports.inject = function (app) {
    app.controller('userController', exports.controller);
    return exports.controller;
};

exports.controller = function userController($scope, $http) {

    $http.get('/api/user').success(function(data) {
        $scope.users = data;
    });
};