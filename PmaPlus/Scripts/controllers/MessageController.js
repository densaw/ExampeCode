var app = angular.module('MainApp');

app.filter('orderObjectBy', function(){
 return function(input, attribute) {
    if (!angular.isObject(input)) return input;

    var array = [];
    for(var objectKey in input) {
        array.push(input[objectKey]);
    }

    array.sort(function(a, b){
        a = parseInt(a[attribute]);
        b = parseInt(b[attribute]);
        return a - b;
    });
    return array;
 }
});

app.controller('PrivateController', ['$scope', '$http','$q', '$filter',function ($scope, $http, $q, $filter) {

        var urlGroupTail = '/api/Message/Group';
        var target = angular.element('#sendMessage');
        var selectGroupId = -1;

        $scope.haveGroup = false;
        $scope.help = {};
        $scope.help.recipients = [];
        $scope.recents = [];
        $scope.groupMessagesList = [];
        $scope.messageInGroupSend = {};
        $scope.messageInGroupSend.messagePrivate = {};
        $scope.persons = [];

        $scope.modalTitle = 'Send Message';

        function shuffle(objArr) {
            var ids = [];
            angular.forEach(objArr, function (obj) {
                this.push(obj.id);
            }, ids);
            return ids;
        }

        $scope.sendMessage = function() {
            target.modal('show');
        };

        $scope.cancel = function(){
          target.modal('hide');  
        }

        $scope.selectGroup = function(group){
            console.log(group.messages)
            $scope.groupMessagesList = group.messages;
            $scope.haveGroup = true;
            selectGroupId = group.id;
        }

        $scope.sendMessageinGroup = function(){
            $http.post(urlGroupTail + '/' + selectGroupId, $scope.messageInGroupSend)
            .success(function(){
               getAllRecent();
               //getAllMessageForGroupById(selectGroupId);
            })
            .error(function (data, status, headers, config) {

            });
        }

        $scope.ok = function(){


            var promises = [];

                if ($scope.messagePic) {
                    var fd = new FormData();
                    fd.append('file', $scope.messagePic);
                    var promise = $http.post('/api/Files/Wall', fd, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    })
                        .success(function (data) {
                            $scope.newMessage.messagePrivate.image = data.name;
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
                    $scope.newMessage.usersInGroup = shuffle($scope.help.recipients);
                    console.log($scope.newMessage);
                    $http.post(urlGroupTail, $scope.newMessage)
                    .success(function(result){
                        getAllRecent();
                        target.modal('hide');
                    })
                    .error(function (data, status, headers, config) {

                    });
                });


        }

        function getAllRecent(){
            $http.get(urlGroupTail)
            .success(function(result){
                console.log(result);
                $scope.recents = $filter('orderBy')(result, 'updateAt', true);
                if($scope.haveGroup){
                    getAllMessageForGroupById(selectGroupId);
                    messageInGroupSend.messagePrivate.message = '';
                }
                
            })
            .error(function (data, status, headers, config) {

            });
        }

        function getAllPersonToSend(){
        	$http.get('/api/Users/List/Wself?role=1&role=2&role=3&role=4&role=5&role=6&role=7&role=8&role=9')
            .success(function (result) {
                $scope.persons = result;
            })
            .error(function (data, status, headers, config) {

            });
        };

        function getAllMessageForGroupById(groupId){
            $http.get('/api/Message/Group/'+ groupId)
            .success(function (result) {
                console.log(result);
                $scope.groupMessagesList = result;
            })
            .error(function (data, status, headers, config) {

            });
        }


        getAllPersonToSend();
        getAllRecent();

    }]);