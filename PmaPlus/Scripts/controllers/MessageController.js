var app = angular.module('MainApp');

app.controller('PrivateController', ['$scope', '$http','$q',function ($scope, $http, $q) {

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
            $scope.groupMessagesList = group.messages;
            $scope.haveGroup = true;
            selectGroupId = group.id;
        }

        $scope.sendMessageinGroup = function(){
            $http.post(urlGroupTail + '/' + selectGroupId, $scope.messageInGroupSend)
            .success(function(){
               getAllMessageForGroupById(selectGroupId)
            })
            .error(function (data, status, headers, config) {

            });
        }

        $scope.ok = function(){

            $scope.newMessage.usersInGroup = shuffle($scope.help.recipients);

            $http.post(urlGroupTail, $scope.newMessage)
            .success(function(result){
                getAllRecent();
                target.modal('hide');
            })
            .error(function (data, status, headers, config) {

            });
        }

        function getAllRecent(){
            $http.get(urlGroupTail)
            .success(function(result){

                $scope.recents = result;
            })
            .error(function (data, status, headers, config) {

            });
        }

        function getAllPersonToSend(){
        	$http.get('/api/Users/List?role=1&role=2&role=3&role=4&role=5&role=6&role=7&role=8&role=9')
            .success(function (result) {
                $scope.persons = result;
            })
            .error(function (data, status, headers, config) {

            });
        };

        function getAllMessageForGroupById(groupId){
            $http.get('/api/Message/Group/'+ groupId)
            .success(function (result) {
                $scope.groupMessagesList = result;
            })
            .error(function (data, status, headers, config) {

            });
        }


        getAllPersonToSend();
        getAllRecent();

    }]);