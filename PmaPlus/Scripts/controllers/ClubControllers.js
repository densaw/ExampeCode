var app = angular.module('MainApp');

app.filter('todo', function () {
    return function (v, yes, no) {
        return v ? yes : no;
    };
});

app.filter('dayExp', function() {
    return function (v, yes, no) {
        console.log(moment.utc(v) > moment.utc());
        return moment.utc(v) > moment.utc() ? yes : no;
    };
});

var routing = function ($routeProvider) {
    $routeProvider.when('localhost:1292/ClubAdmin/Home/ProfilePage/:role', {
        templateUrl: function (params) { return '/ClubAdmin/Home/ProfilePage?role=' + params.role; },
        controller: 'ProfilePageController'
    });
}

routing.$inject = ['$routeProvider'];

app.config(routing);

app.controller('AttributesController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {

    var needToDelete = -1;
    var urlTail = '/api/Attributes';

    $scope.attrTypes = [
       { id: 0, name: 'Yes/No' },
       { id: 1, name: 'Rating' }
    ];

    $scope.selectedType = $scope.attrTypes[0];

    $scope.open = function () {

        $scope.opened = true;
    };

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

app.controller('TrainingTeamController', ['$scope', '$http', 'toaster', '$q', function ($scope, $http, toaster, $q) {


    function getResultsPage() {
        $http.get(urlTail)
           .success(function (result) {
               $scope.items = result;
           });
    }
    

    $scope.roles = [
       { id: 0, name: 'System Admin' },
       { id: 1, name: 'Club Admin' },
       { id: 2, name: 'Head Of Academies' },
       { id: 3, name: 'Coach' },
       { id: 4, name: 'Head Of Education' },
       { id: 5, name: 'Welfare Officer' },
       { id: 6, name: 'Scout' },
       { id: 7, name: 'Physiotherapist' },
       { id: 8, name: 'Sports Scientist' },
       { id: 9, name: 'Player' }
    ];

    $scope.rolesVisible = [
       { id: 2, name: 'Head Of Academies' },
       { id: 3, name: 'Coach' },
       { id: 4, name: 'Head Of Education' },
       { id: 5, name: 'Welfare Officer' },
       { id: 6, name: 'Scout' },
       { id: 7, name: 'Physiotherapist' },
       { id: 8, name: 'Sports Scientist' }
    ];

    $scope.selectedRole = $scope.rolesVisible[0];
    $scope.newMember = {};

    var urlTail = '/api/TrainingTeamMembers';
    var target = angular.element('#addTeamMember');
    var needstoReport = angular.element('#needstoReport');

    $scope.openModal = function() {
        target.modal('show');
    };

    
    $scope.parserJ = function(roleId, userId) {
        return { role: roleId, user: userId };
    }


    $scope.send = function () {

        //Files upload

        var promises = [];


        if ($scope.pic) {
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
            $scope.newMember.userStatus = 0;
            $scope.newMember.role = $scope.selectedRole.id;
            $scope.newMember.needReport = needstoReport.prop('checked');
            //$scope.newMember.profilePicture = 'tmp.png';
            console.log($scope.newMember);
            $http.post(urlTail, $scope.newMember).success(function(result) {
                getResultsPage();
                target.modal('hide');
            }).error(function (data, status, headers, config) {
                console.log(data);
                if (status == 400) {
                    console.log(data);
                    toaster.pop({
                        type: 'error',
                        title: 'Error', bodyOutputType: 'trustedHtml',
                        body: data.message.join("<br />")
                    });
                }
            }); 
        });
        //--
    };

    getResultsPage();
}]);

app.controller('ToDoController', ['$scope', '$http', 'toaster', function($scope, $http, toaster) {

    var needToDelete = -1;
    var needToUpdate = -1;

    var urlTail = '/api/ToDo';
    var target = angular.element('#addNote');
    var deleteConf = angular.element('#confDelete');

    $scope.Priority = [
       { id: 0, name: 'Urgent' },
       { id: 1, name: 'Normal' },
       { id: 2, name: 'Non-Urgent' }
    ];

   


    $scope.selectedPriority = $scope.Priority[0];
    $scope.newNote = {};

    function getResults() {
        $http.get(urlTail)
           .success(function (result) {
               $scope.items = result;
            console.log(result);
        });

    }

    getResults();

    $scope.check = function (item) {
        console.log(item);
        item.complete = !item.complete;
        $http.put(urlTail +'/'+item.id, item).success(function () {
            getResults();
        });
    }

    $scope.cancel = function () {
        $scope.newNote = {};
        needToUpdate = -1;
        needToDelete = -1;
        getResults();
        target.modal('hide');
        deleteConf.modal('hide');
    }

    $scope.open = function () {
        $scope.windowTitle = 'Add Note';
        target.modal('show');
    }

    $scope.showDelete = function (id) {
        needToDelete = id;
        deleteConf.modal('show');
    }

    $scope.delete = function () {
        $http.delete(urlTail + '/' + needToDelete).success(function () {
            getResults();
            needToDelete = -1;
            deleteConf.modal('hide');
        });
    }

    $scope.update = function(item) {
        $scope.windowTitle = 'Update Note';
        $scope.newNote = item;
        needToUpdate = item.id;
        target.modal('show');
    }

    $scope.ok = function () {
        $scope.newNote.priority = $scope.selectedPriority.id;
        console.log(needToUpdate);
        console.log(needToUpdate != -1);

        if (needToUpdate != -1) {
            $http.put(urlTail + '/' + needToUpdate, $scope.newNote).success(function () {
                needToUpdate = -1;
                getResults();
                target.modal('hide');
            });
        } else {
            $http.post(urlTail, $scope.newNote)
          .success(function (result) {
            getResults();
            target.modal('hide');
          }).error(function (data, status, headers, config) {
              console.log(data);
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

       
    }

}]);

app.controller('ClubDiaryController', ['$scope', '$http', 'toaster', '$compile', 'uiCalendarConfig', function ($scope, $http, toaster, $compile, uiCalendarConfig) {

    var target = angular.element('#addDiaryModal');


    $scope.newEvent = {};
    $scope.newEvent.attendeeTypes = [];
    $scope.newEvent.specificPersons = [];
    $scope.newEvent.allDay = false;

    /*
        HeadOfAcademies = 2,
        Coach = 3,
        HeadOfEducation = 4,
        WelfareOfficer = 5,
        Scout = 6,
        Physiotherapist = 7,
        SportsScientist = 8,
        Player = 9
        */
    $scope.specificPersons = [];
    $scope.attendeeTypes = [
        { id: 2, name: 'Head Of Academies' },
        { id: 3, name: 'Coach' },
        { id: 4, name: 'Head Of Education' },
        { id: 5, name: 'Welfare Officer' },
        { id: 6, name: 'Scout' },
        { id: 7, name: 'Physiotherapist' },
        { id: 8, name: 'Sports Scientist' },
        { id: 9, name: 'Player' }
    ];

    $scope.$watch('newEvent.attendeeTypes', function (result) {
        console.log(result);
        if (!result.length) {

        } else {
            
        }
    });



    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    var cal = angular.element('#calendar');

    var urlTail = '/api/Diary';

    function getResults() {
        $http.get(urlTail)
            .success(function (result) {
                console.log(result);
                $scope.items = result;
                angular.forEach(result, function(value) {
                    cal.fullCalendar('renderEvent', value);
                });
            });
    }

    getResults();

    $scope.open = function() {
        target.modal('show');
    }

    $scope.ok = function() {
        $http.post(urlTail, $scope.newEvent).success(function () {
            getResults();
            target.modal('hide');
        });
    }

    $scope.cancel = function () {
        target.modal('hide');
    }
}]);

app.controller('SkillVidController', ['$scope', '$http', 'toaster', '$location', function ($scope, $http, toaster, $location) {

    $scope.modalTitle = "Add a Skill";

    var needToDelete = -1;
    var urlTail = '/api/SkillVideos';

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


    $scope.pagination = {
        current: 1
    };
    getResultsPage($scope.pagination.current);
    $scope.pageChanged = function (newPage) {
        getResultsPage(newPage);
        $scope.pagination.current = newPage;
    };

    var target = angular.element('#addSkill');
    var confDelete = angular.element('#confDelete');
    var modalVideo = angular.element('#videoModal');
    $scope.modalVideoStart = function (src) {
        //var src = 'http://www.youtube.com/v/Qmh9qErJ5-Q&amp;autoplay=1';
        modalVideo.modal('show');
        $('#videoModal iframe').attr('src', src);
    }

    $scope.ok = function (id) {
        $scope.myform.form_Submitted = !$scope.myform.$valid;

        if (id != null) {
            $http.put(urlTail + '/' + id, $scope.newSkill).success(function () {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
            }).error(function (data, status, headers, config) {
                if (status == 400) {
                    console.log(data);
                    toaster.pop({
                        type: 'error',
                        title: 'Error', bodyOutputType: 'trustedHtml',
                        body: 'Please complete the compulsory fields highlighted in red'
                    });
                }
            });

        } else {
            console.log($scope.newSkill);
            $http.post(urlTail, $scope.newSkill).success(function () {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
            }).error(function (data, status, headers, config) {
                if (status == 400) {
                    console.log(data);
                    toaster.pop({
                        type: 'error',
                        title: 'Error', bodyOutputType: 'trustedHtml',
                        body: 'Please complete the compulsory fields highlighted in red'
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
        $scope.modalTitle = "Add a Skill";
        $scope.newSkill = {};
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
        console.log(id);
        $http.get('/api/SkillVideos/' + id)
            .success(function (result) {
                $scope.newSkill = result;
                $scope.modalTitle = "Update a Skill";
                target.modal('show');

            });
    };
}]);

app.controller('ToDoNotifyController', ['$scope', '$http', 'toaster', function($scope, $http, toaster) {

    var urlTail = '/api/ToDo/Today';
    $scope.itemCount = 0;


    function getResults() {
        $http.get(urlTail)
           .success(function (result) {
               $scope.items = result;
               $scope.itemCount = result.length;
        });

    }

    getResults();
}]);

app.controller('ClubProfileController', ['$scope', '$http', 'toaster', '$q', function($scope, $http, toaster, $q) {

    var urlTail = '/api/Clubs';

    $scope.statuses = [
            { id: 0, name: 'Active' },
            { id: 1, name: 'Blocked' },
            { id: 2, name: 'Closed' }
    ];
    $scope.selectedStatus = $scope.statuses[0];

    function getResults(tail) {
        $http.get(urlTail + tail)
           .success(function (result) {
               $scope.newClub = result;
               $scope.newClub.logo === '' ? $scope.oldLogo = false : $scope.oldLogo = true;
               $scope.newClub.background === '' ? $scope.oldBackground = false : $scope.oldBackground = true;
           });

    }

    getResults('/Current');

    $scope.ok = function (id) {

        $scope.myform.form_Submitted = !$scope.myform.$valid;
        if (!$scope.myform.$valid) {
            toaster.pop({
                type: 'error',
                title: 'Error',
                bodyOutputType: 'trustedHtml',
                body: 'Please complete the compulsory fields highlighted in red'
            });


        } else {




            //Files upload

            var promises = [];

            if ($scope.logoFile) {
                var fd = new FormData();
                fd.append('file', $scope.logoFile);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newClub.logo = data.name;
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

            if ($scope.backgroundFile) {
                var fd = new FormData();
                fd.append('file', $scope.backgroundFile);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newClub.background = data.name;
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


            //$scope.newClub.logo = 'tmp.jpeg';
            //$scope.newClub.background = 'tmp.jpeg';
            $q.all(promises).then(function () {

                $scope.newClub.status = $scope.selectedStatus.id;
                console.log($scope.newClub);
                if (id != null) {
                    $http.put('/api/Clubs/' + id, $scope.newClub)
                        .success(function () {
                            getResults('/Current');
                        }).error(function (data, status, headers, config) {
                            if (status == 400) {
                                console.log(data);
                                toaster.pop({
                                    type: 'error',
                                    title: 'Error',
                                    bodyOutputType: 'trustedHtml',
                                    body: 'Please complete the compulsory fields highlighted in red'
                                });
                            }
                        });

                } else {

                    $http.post('/api/Clubs', $scope.newClub)
                        .success(function () {
                            getResults('/Current');
                        }).error(function (data, status, headers, config) {
                            if (status == 400) {
                                console.log(data);
                                toaster.pop({
                                    type: 'error',
                                    title: 'Error',
                                    bodyOutputType: 'trustedHtml',
                                    body: 'Please complete the compulsory fields highlighted in red'
                                });
                            }
                        });
                };
            });
        }


    }

}]);

app.controller('ProfilePageController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', function ($scope, $http, toaster, $q, $routeParams, $location) {

    var paramArray = $location.$$absUrl.split("?par=");
    //console.log(parameters);
    var params = paramArray[1].split("/");

    function getProfileDate(userId) {
        $http.get('/api/TrainingTeamMembers/' + userId).success(function (result) {
            console.log(result);
            $scope.user = result;
        });
    }

    $scope.$watch('userId', function (val) {
        getProfileDate(val);
    });

    $scope.roleId = params[0];
    $scope.userId = params[1];



    $scope.roles = [
       { id: 0, name: 'System Admin' },
       { id: 1, name: 'Club Admin' },
       { id: 2, name: 'Head Of Academies' },
       { id: 3, name: 'Coach' },
       { id: 4, name: 'Head Of Education' },
       { id: 5, name: 'Welfare Officer' },
       { id: 6, name: 'Scout' },
       { id: 7, name: 'Physiotherapist' },
       { id: 8, name: 'Sports Scientist' },
       { id: 9, name: 'Player' }
    ];
}]);

app.controller('NavController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', function ($scope, $http, toaster, $q, $routeParams, $location) {

    

}]);

app.controller('StController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', function($scope, $http, toaster, $q, $routeParams, $location) {
    
}]);

app.controller('ClubPlayerController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', function($scope, $http, toaster, $q, $routeParams, $location) {
    
    var needToDelete = -1;
    var urlTail = '/api/Player';

    $scope.statuses = [
            { id: 0, name: 'Active' },
            { id: 1, name: 'Blocked' },
            { id: 2, name: 'Closed' }
    ];

    $scope.playingFoot = [
        { id: 0, name: 'Left' },
        { id: 1, name: 'Right' },
        { id: 2, name: 'Both' }
    ];

    $scope.selectedStatus = $scope.statuses[0];
    $scope.selectedFoot = $scope.playingFoot[0];

    

    $scope.newPlayer = {};
    $scope.newPlayer.teams = [];

    $http.get('/api/Teams/List').success(function (result) {
        console.log(result);
        $scope.teams = result;
    });


    $scope.open = function () {
        $scope.opened = true;
    };

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

    $scope.pagination = {
        current: 1
    };
    getResultsPage($scope.pagination.current);
    $scope.pageChanged = function (newPage) {
        getResultsPage(newPage);
        $scope.pagination.current = newPage;
    };

    var target = angular.element('#addPlayerModal');
    var confDelete = angular.element('#confDelete');
    var maxScore = angular.element('#maxScore');

    $scope.ok = function (id) {
        $scope.myform.form_Submitted = !$scope.myform.$valid;
        if (!$scope.myform.$valid) {
            toaster.pop({
                type: 'error',
                title: 'Error',
                bodyOutputType: 'trustedHtml',
                body: 'Please complete the compulsory fields highlighted in red'
            });


        } else {
            //Files upload
            var promises = [];

            if ($scope.pic) {
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

                console.log();

                $scope.newPlayer.status = $scope.selectedStatus.id;
                $scope.newPlayer.playingFoot = $scope.selectedFoot.id;
                if ($scope.newPlayer.teams == null) {
                    $scope.newPlayer.teams = [];
                }
                
                console.log($scope.newPlayer);
                if (id != null) {
                    $http.put(urlTail + '/' + id, $scope.newPlayer)
                        .success(function () {
                            getResultsPage($scope.pagination.current);
                            target.modal('hide');
                        })
                        .error(function (data, status, headers, config) {
                            if (status == 400) {
                                console.log(data);
                                toaster.pop({
                                    type: 'error',
                                    title: 'Error',
                                    bodyOutputType: 'trustedHtml',
                                    body: 'Please complete the compulsory fields highlighted in red'
                                });
                            }
                        });

                } else {


                    $scope.newPlayer.status = $scope.selectedStatus.id;
                    $scope.newPlayer.playingFoot = $scope.selectedFoot.id;

                    if ($scope.newPlayer.teams == null) {
                        $scope.newPlayer.teams = [];
                    }
                    $http.post(urlTail, $scope.newPlayer)
                        .success(function () {
                            getResultsPage($scope.pagination.current);
                            target.modal('hide');
                        })
                        .error(function (data, status, headers, config) {
                            if (status == 400) {
                                console.log(data);
                                toaster.pop({
                                    type: 'error',
                                    title: 'Error',
                                    bodyOutputType: 'trustedHtml',
                                    body: 'Please complete the compulsory fields highlighted in red'
                                });
                            }
                        });
                };
            });
        }
    };
    
    $scope.cancel = function () {
        target.modal('hide');
        confDelete.modal('hide');
    };
    $scope.openAdd = function () {
        $scope.modalTitle = "Add an Player";
        $scope.newPlayer = {};
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
        console.log(id);
        $http.get(urlTail + '/' + id)
            .success(function (result) {
                $scope.newPlayer = result;
                $scope.newPlayer.id = id;
                $scope.modalTitle = "Update an Player";
                target.modal('show');
            });
    };
}]);
