var app = angular.module('MainApp');


app.filter('utc', function () {

    return function (val) {
        var date = new Date(val);
        return new Date(date.getUTCFullYear(),
                        date.getUTCMonth(),
                        date.getUTCDate(),
                        date.getUTCHours(),
                        date.getUTCMinutes(),
                        date.getUTCSeconds());
    };

});

app.filter('getById', function () {
    return function (input, id) {
        var i = 0, len = input.length;
        for (; i < len; i++) {
            if (+input[i].id == +id) {
                return input[i];
            }
        }
        return null;
    }
});

app.filter('todo', function () {
    return function (v, yes, no) {
        return v ? yes : no;
    };
});

app.filter('dayExp', function () {
    return function (v, yes, no) {
        console.log(moment.utc(v) > moment.utc());
        return moment.utc(v) > moment.utc() ? yes : no;
    };
});

app.directive('styleParent', function () {
    return {
        restrict: 'A',
        link: function (scope, elem, attr) {
            elem.on('load', function () {
                var w = $(this).width(),
                    h = $(this).height();

                elem.addClass(w > h ? 'landscape' : 'portrait');
            });
        }
    };
});


function hexToRgb(hex) {
    // Expand shorthand form (e.g. "03F") to full form (e.g. "0033FF")
    var shorthandRegex = /^#?([a-f\d])([a-f\d])([a-f\d])$/i;
    hex = hex.replace(shorthandRegex, function (m, r, g, b) {
        return r + r + g + g + b + b;
    });

    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    return result ? {
        r: parseInt(result[1], 16),
        g: parseInt(result[2], 16),
        b: parseInt(result[3], 16)
    } : null;
}


var routing = function ($routeProvider) {
    $routeProvider.when('localhost:1292/ClubAdmin/Home/ProfilePage/:role', {
        templateUrl: function (params) { return '/ClubAdmin/Home/ProfilePage?role=' + params.role; },
        controller: 'ProfilePageController'
    });
}

routing.$inject = ['$routeProvider'];

app.config(routing);


app.controller('ClubAdminDashboardController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {

    var monthNames = ['Jan',
           'Feb',
           'Mar',
           'Apr',
           'May',
           'Jun',
           'Jul',
           'Aug',
           'Sep',
           'Oct',
           'Nov',
           'Dec'];

    var hexClub = "";

    $http.get('/api/clubs/color').success(function (data) {
        hexClub = data;


        $http.get('/api/ClubAdminDashboard/Players/ScoreGraph').success(function (data) {

            var monthArray = new Array;
            var playerCountArray = new Array;
            data.forEach(function (val) {
                monthArray.push(monthNames[val.month - 1]);
                playerCountArray.push(val.activePlayers);
            });


            var clubRgb = hexToRgb(hexClub);

            $scope.data = {
                labels: monthArray,
                datasets: [
                    {
                        label: "Example dataset",
                        fillColor: "rgba(" + clubRgb.r + "," + clubRgb.g + "," + clubRgb.b + ",0.5)",
                        strokeColor: hexClub,
                        pointColor: hexClub,
                        pointStrokeColor: "#fff",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: hexClub,
                        data: playerCountArray
                    }

                ]
            };

            $scope.options = {
                scaleShowGridLines: true,
                scaleGridLineColor: "rgba(0,0,0,.05)",
                scaleGridLineWidth: 1,
                bezierCurve: true,
                bezierCurveTension: 0.4,
                pointDot: true,
                pointDotRadius: 4,
                pointDotStrokeWidth: 1,
                pointHitDetectionRadius: 20,
                datasetStroke: true,
                datasetStrokeWidth: 2,
                datasetFill: true,
                responsive: true
            };
        });


    });
}]);

app.controller('AttributesController', ['$scope', '$http', 'toaster', '$filter', '$rootScope', function ($scope, $http, toaster, $filter, $rootScope) {

    var needToDelete = -1;
    var urlTail = '/api/Attributes';
    var sortArray = [];

    $scope.attrTypes = [
       { id: 0, name: 'Yes/No' },
       { id: 1, name: 'Rating' }
    ];

    $scope.selectedType = $scope.attrTypes[0];

    $scope.open = function () {

        $scope.opened = true;
    };

    function createTail(pageNumber) {
        if (sortArray.length > 0) {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
        } else {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber;
        }
    }

    function getResultsPage(pageNumber) {
        $http.get(createTail(pageNumber))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    }

    $scope.items = [];
    $scope.totalItems = 0;
    $scope.itemsPerPage = 20;
    $scope.newAttr = {};

    $rootScope.$watchGroup(['orderField', 'revers'], function (newValue, oldValue, scope) {
        sortArray = newValue;
        $http.get(createTail($scope.pagination.current))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    });

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
        $scope.loginLoading = true;
        $scope.myform.form_Submitted = !$scope.myform.$valid;
        if (id != null) {
            $scope.loginLoading = false;
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
            $scope.loginLoading = false;
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

app.controller('ClubDocumetsController', ['$scope', '$http', 'toaster', '$q', '$filter', function ($scope, $http, toaster, $q, $filter) {


    $scope.add = function () {
        var f = document.getElementById('file').files[0],
            r = new FileReader();
        r.onloadend = function (e) {
            var data = e.target.result;
            //send you binary data via $http or $resource or do anything else with it
        }
        r.readAsBinaryString(f);
    }


}]);
app.controller('TrainingTeamController', ['$scope', '$http', 'toaster', '$q', '$filter', '$rootScope', function ($scope, $http, toaster, $q, $filter, $rootScope) {

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
       { id: 7, name: 'Physio' },
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

    $scope.openModal = function () {
        $scope.myform.form_Submitted = false;
        $scope.newMember = {};
        target.modal('show');
    };

    $scope.cancel = function () {
        $scope.newMember = {};
        target.modal('hide');
    }

    $scope.delete = function (id) {
        $http.delete(urlTail + '/' + id).success(function () {
            getResultsPage();
        });
        target.modal('hide');
    }


    $scope.parserJ = function (roleId, userId) {
        return { role: roleId, user: userId };
    }

    $scope.edit = function (id) {

        $scope.newMember = {};
        $scope.myform.form_Submitted = false;
        $http.get(urlTail + '/' + id)
            .success(function (result) {
                $scope.newMember = result;
                console.log(result);
                $scope.selectedRole = $filter('getById')($scope.rolesVisible, result.role);
                target.modal('show');
            });

    }

    $scope.send = function (id) {
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
            $scope.newMember.role = $scope.selectedRole.id;
            $scope.newMember.needReport = needstoReport.prop('checked');
            //$scope.newMember.profilePicture = 'tmp.png';

            if (id != null) {

                $http.put(urlTail + '/' + $scope.newMember.id, $scope.newMember)
                .success(function (result) {
                    getResultsPage();
                    target.modal('hide');
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


            } else {


                $http.post(urlTail, $scope.newMember)
                    .success(function (result) {
                        getResultsPage();
                        target.modal('hide');
                        $scope.loginLoading = false;
                    }).error(function (data, status, headers, config) {
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
        //--
    };

    getResultsPage();
}]);

app.controller('ToDoController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {

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
        $http.put(urlTail + '/' + item.id, item).success(function () {
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
        $scope.myform.form_Submitted = false;
        $scope.newNote = {};
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

    $scope.update = function (item) {
        $scope.myform.form_Submitted = false;
        $scope.windowTitle = 'Update Note';
        $scope.newNote = item;
        needToUpdate = item.id;
        console.log($scope.newNote.completionDateTime);
        target.modal('show');
    }

    $scope.ok = function () {
        $scope.loginLoading = true;
        $scope.myform.form_Submitted = !$scope.myform.$valid;
        $scope.newNote.priority = $scope.selectedPriority.id;

        if (needToUpdate != -1) {
            $http.put(urlTail + '/' + needToUpdate, $scope.newNote).success(function () {
                needToUpdate = -1;
                getResults();
                target.modal('hide');
                $scope.loginLoading = false;
            });
        } else {
            $http.post(urlTail, $scope.newNote)
          .success(function (result) {
              getResults();
              target.modal('hide');
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
                  $scope.loginLoading = false;
              }
          });
        }


    }

}]);

app.controller('MyCtrlDiary', ['$scope', '$rootScope', '$digest', '$watch', function ($scope, $rootScope, $digest, $watch) {
    $scope.cols = 0;
    $scope.colsChanged = function () {
        $scope.items = actual(+$scope.cols);
    };

    //optional: if you're starting with 1 or more "cols"
    $scope.colsChanged();
   


   
}]);

app.controller('ClubDiaryController', [
    '$scope', '$http', 'toaster', '$compile', 'uiCalendarConfig', function ($scope, $http, toaster, $compile, uiCalendarConfig) {




        var needToDelete = -1;
        var needToUpdate = -1;



        //$scope.selectedPriority = $scope.Priority[0];

        var target = angular.element('#addDiaryModal');


        function shuffle(objArr) {
            var ids = [];
            angular.forEach(objArr, function (obj) {
                this.push(obj.id);
            }, ids);
            return ids;
        }


        $scope.newEvent = {};
        $scope.newEvent.attendeeTypes = [];
        $scope.newEvent.specificPersons = [];
        $scope.newEvent.allDay = false;

        //Helper arrays
        $scope.help = {};
        $scope.help.helpAttend = [];
        $scope.help.helpSpecify = [];

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

        $scope.$watch('help.helpAttend', function (result) {
            console.log(result);
            if (!result.length) {

            } else {
                var stringPar = [];
                angular.forEach(result, function (value) {
                    stringPar.push('role=' + value.id);
                });
                console.log(stringPar.join('&'));
                $http.get('/api/Users/List?' + stringPar.join('&')).success(function (result) {
                    $scope.specificPersons = result;
                });
            }
        });


        var date = new Date();

        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();


        function getEv() {
            $scope.Evnotes = [];
            $http.get('/api/Diary/').success(function (result) {
                $scope.Evnotes = result;

            });
        }

        var now = new Date().toISOString();
        //var n = now.toISOString();


        function getactualEv() {
            console.log(moment());
            $scope.actualEvnotes = [];
            $http.get('/api/Diary/').success(function (result) {
                $scope.actualEvnotes = result;
                $scope.actual = [];

                angular.forEach($scope.actualEvnotes, function (item) {
                    if (moment(item.start).isAfter(moment())) {
                        this.push(item);
                    }
                }, $scope.actual);

            });
        }




        var cal = angular.element('#calendar');
        var urlTail = '/api/Diary';



        $scope.events = [];
        cal.fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            
            axisFormat: 'H:mm',
            timeFormat: {
                agenda: 'H:mm' //h:mm{ - h:mm}'
            },
            
            allDayDefault: true,
            defaultView: 'agendaWeek',
            aspectRatio: 1.5,
            events:
                function geteventData() {
                    $http.get(urlTail)
                            .success(function (result) {
                                cal.fullCalendar('removeEvents');

                                $scope.events = result;
                                angular.forEach(result, function (value) {
                                    cal.fullCalendar('renderEvent', value);
                                });
                            });
                },


            selectable: true,
            selectHelper: true,

            select: function (start, end, jsEvent, view) { //select cell (empty)

                //var allDay = !start.hasTime() && !end.hasTime();
                $scope.newEvent.start = {};
                $scope.newEvent.start = moment(start).format('YYYY-MM-DDTHH:mm');
                target.modal('show'); //open the modal

            },


            editable: false,
            droppable: false,
            drop: function () {
                // is the "remove after drop" checkbox checked?
                if ($('#drop-remove').is(':checked')) {
                    // if so, remove the element from the "Draggable Events" list
                    $(this).remove();
                }
            },


            eventRender: function (event, element) {
                $scope.openEdit = element.bind('dblclick', function () {

                    $http.get('/api/Diary/' + event.id)
                        .success(function (result) {

                            $scope.newEvent = result;

                            $scope.newEvent.start = moment(result.start).format('YYYY-MM-DDTHH:mm');
                            $scope.newEvent.end = moment(result.end).format('YYYY-MM-DDTHH:mm');

                            needToUpdate = event.id;
                            needToDelete = event.id;
                            $scope.modalTitle = "Edit Event";
                            target.modal('show');


                        });
                });
            }

        });

        $scope.$watch('newEvent.start', function (newValue, oldValue, scope) {
            console.log('Data');
            console.log(newValue);
        });

        getactualEv();
        getEv();


        var confDelete = angular.element('#confDelete');

        function getResults() {
            $http.get(urlTail)
                    .success(function (result) {
                        cal.fullCalendar('removeEvents');

                        console.log(result);
                        $scope.items = result;
                        angular.forEach(result, function (value) {
                            cal.fullCalendar('renderEvent', value);
                        });
                    });
        }

        getResults();

        $scope.open = function () {
            $scope.windowTitle = 'Add Event';
            $scope.myform.form_Submitted = false;
            target.modal('show');
        }


        $scope.update = function (event) {
            $scope.windowTitle = 'Update Event';
            $scope.newEvent = event;
            needToUpdate = event.id;
            $scope.myform.form_Submitted = false;
            target.modal('show');
        }
        $scope.ok = function () {
            $scope.myform.form_Submitted = !$scope.myform.$valid;
            $scope.loginLoading = true;


            $scope.newEvent.start = moment($scope.newEvent.start).format('YYYY-MM-DDTHH:mm');
            $scope.newEvent.end = moment($scope.newEvent.end).format('YYYY-MM-DDTHH:mm');

            console.log('start' + $scope.newEvent.start);
            console.log('end' + $scope.newEvent.end);

            $scope.newEvent.attendeeTypes = shuffle($scope.help.helpAttend);
            $scope.newEvent.specificPersons = shuffle($scope.help.helpSpecify);

            //put
            if (needToUpdate != -1) {
                $scope.loginLoading = false;
                $http.put(urlTail + '/' + needToUpdate, $scope.newEvent).success(function (data, status, headers, config) {
                    needToUpdate = -1;
                    getResults();
                    getEv();
                    getactualEv();
                    target.modal('hide');
                }).error(function (data, status, headers, config) {
                    if (status == 400) {
                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            bodyOutputType: 'trustedHtml',
                            body: 'Please complete the compulsory fields highlighted in red'
                        });
                        $scope.loginLoading = false;
                    }

                });

            } else {
                $http.post(urlTail, $scope.newEvent).success(function () {
                    getResults();
                    getEv();
                    getactualEv();
                    target.modal('hide');
                    $scope.loginLoading = false;

                }).error(function (data, status, headers, config) {
                    //$scope.event.id = $scope.selectedType.id;
                    if (status == 400) {
                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            bodyOutputType: 'trustedHtml',
                            body: 'Please complete the compulsory fields highlighted in red'
                        });

                    }
                    $scope.loginLoading = false;
                });
            }

        }


        $scope.cancel = function () {
            $scope.newEvent = {};
            needToUpdate = -1;
            needToDelete = -1;
            getResults();
            target.modal('hide');

        }
        $scope.delete = function () {
            $http.delete(urlTail + '/' + needToDelete).success(function () {
                getResults();
                needToDelete = -1;
                needToUpdate = -1;
                getEv();
                getactualEv();
                target.modal('hide');
            });
        };


    }]);

app.controller('SkillVidController', ['$scope', '$http', 'toaster', '$location', '$rootScope', function ($scope, $http, toaster, $location, $rootScope) {

    $scope.modalTitle = "Add a Skill";

    var needToDelete = -1;
    var urlTail = '/api/SkillVideos';
    var sortArray = [];

    function getResultsPage(pageNumber) {
        $http.get(createTail(pageNumber))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    }

    function createTail(pageNumber) {
        if (sortArray.length > 0) {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
        } else {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber;
        }
    }

    $rootScope.$watchGroup(['orderField', 'revers'], function (newValue, oldValue, scope) {
        sortArray = newValue;
        $http.get(createTail($scope.pagination.current))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    });


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

                console.log(data);
                toaster.pop({
                    type: 'error',
                    title: 'Error', bodyOutputType: 'trustedHtml',
                    body: 'Please complete the compulsory fields highlighted in red'
                });

            });

        } else {
            console.log($scope.newSkill);
            $http.post(urlTail, $scope.newSkill).success(function () {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
            }).error(function (data, status, headers, config) {

                console.log(data);
                toaster.pop({
                    type: 'error',
                    title: 'Error', bodyOutputType: 'trustedHtml',
                    body: 'Please complete the compulsory fields highlighted in red'
                });

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
app.controller('DairyNotifyController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {

    var urlTail = '/api/Diary/today';

    $scope.itemCount = 0;


    function getResults() {
        $http.get(urlTail)
           .success(function (result) {
               console.log(result);
               $scope.items = result;
               $scope.itemCount = result.length;
           });

    }

    getResults();
}]);
app.controller('ToDoNotifyController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {

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

app.controller('ClubProfileController', ['$scope', '$http', 'toaster', '$q', function ($scope, $http, toaster, $q) {

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
        $scope.loginLoading = true;
        $scope.myform.form_Submitted = !$scope.myform.$valid;
        if (!$scope.myform.$valid) {
            $scope.loginLoading = false;
            toaster.pop({
                type: 'error',
                title: 'Error',
                bodyOutputType: 'trustedHtml',
                body: 'Please complete the compulsory fields highlighted in red'
            });


        } else {


            $scope.newClub.status = $scope.selectedStatus.id;
            console.log($scope.newClub);
            if (id != null) {
                $http.put('/api/Clubs/' + id, $scope.newClub)
                    .success(function () {
                        getResults('/Current');
                        toaster.pop({
                            type: 'success',
                            title: 'Success',
                            bodyOutputType: 'trustedHtml',
                            body: 'Profile was updated'
                        });
                        $scope.loginLoading = false;
                    }).error(function (data, status, headers, config) {
                        if (status == 400) {
                            console.log(data);
                            toaster.pop({
                                type: 'error',
                                title: 'Error',
                                bodyOutputType: 'trustedHtml',
                                body: 'Please complete the compulsory fields highlighted in red'
                            });
                            $scope.loginLoading = false;
                        }
                    });

            } else {

                $http.post('/api/Clubs', $scope.newClub)
                    .success(function () {
                        getResults('/Current');
                        toaster.pop({
                            type: 'success',
                            title: 'Success',
                            bodyOutputType: 'trustedHtml',
                            body: 'Profile was updated'
                        });
                        $scope.loginLoading = false;
                    }).error(function (data, status, headers, config) {
                        if (status == 400) {
                            console.log(data);
                            toaster.pop({
                                type: 'error',
                                title: 'Error',
                                bodyOutputType: 'trustedHtml',
                                body: 'Please complete the compulsory fields highlighted in red'
                            });
                            $scope.loginLoading = false;
                        }
                    });
            };
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

app.controller('CurriculumsController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', '$rootScope', function ($scope, $http, toaster, $q, $routeParams, $location, $rootScope) {

    //Variable section
    var needToDelete = -1;
    var urlTail = '/api/Curriculums';
    var target = angular.element('#addCurrModal');
    var confDelete = angular.element('#confDelete');
    var inpSessions = angular.element('#inpSessions');
    var inpWeeks = angular.element('#inpWeeks');
    var inpBlocks = angular.element('#inpBlocks');
    var sortArray = [];

    $scope.inpSessions = false;
    $scope.inpWeeks = false;
    $scope.inpBlocks = false;
    $scope.clubName = '';

    $scope.newCurr = {};

    $scope.curriculumTypesList = [];
    $scope.ageGroups = [
        { id: 0, name: 'U6' },
        { id: 1, name: 'U7' },
        { id: 2, name: 'U8' },
        { id: 3, name: 'U9' },
        { id: 4, name: 'U10' },
        { id: 5, name: 'U11' },
        { id: 6, name: 'U12' },
        { id: 7, name: 'U13' },
        { id: 8, name: 'U14' },
        { id: 9, name: 'U15' },
        { id: 10, name: 'U16' },
        { id: 11, name: 'U17' },
        { id: 12, name: 'U18' },
        { id: 13, name: '+18' },
    ];
    $scope.selectedAgeGroup = $scope.ageGroups[0];

    $scope.$watch('selectedCurriculumTypeId', function (newValue) {
        if (newValue != null) {
            console.log(newValue);
            $http.get('/api/CurriculumTypes/' + newValue.id).success(function (result) {
                $scope.inpWeeks = !result.usesWeeks;
                $scope.inpBlocks = !result.usesBlocks;
                $scope.inpSessions = !result.usesSessions;
            });
        }
    });

    function getClubName() {
        $http.get('/api/ClubAdminDashboard/ClubName').success(function (result) {
            $scope.clubName = result;
        }).error(function (data, status, headers, config) {

        });
    }
    getClubName();

    function createTail(pageNumber) {
        if (sortArray.length > 0) {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
        } else {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber;
        }
    }

    function getResultsPage(pageNumber) {
        $http.get(createTail(pageNumber))
            .success(function (result) {
                console.log(result);
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    }

    $rootScope.$watchGroup(['orderField', 'revers'], function (newValue, oldValue, scope) {
        sortArray = newValue;
        $http.get(createTail($scope.pagination.current))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    });

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


    $scope.open = function () {
        $scope.modalTitle = 'Add Curriculums';
        target.modal('show');
    };

    $scope.cancel = function () {
        target.modal('hide');
        confDelete.modal('hide');
        needToDelete = -1;
    };

    $scope.ok = function (id) {
        $scope.loginLoading = true;
        $scope.newCurr.ageGroup = $scope.selectedAgeGroup.id;
        if (id != null) {

            //PUT it now have no url to Update date
            $http.put(urlTail + '/' + id, $scope.newCurr).success(function (result) {
                getResultsPage($scope.pagination.current);
                $scope.loginLoading = false;
                $scope.newCurr = {};
                target.modal('hide');
            }).error(function (data, status, headers, config) {

            });
        } else {
            //POST

            $http.post(urlTail, $scope.newCurr).success(function (result) {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
                $scope.newCurr = {};
                $scope.loginLoading = false;
            }).error(function (data, status, headers, config) {

            });
        }
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
                $scope.newCurr = result;
                $scope.selectedAgeGroup = $scope.ageGroups[result.ageGroup];
                $scope.modalTitle = "Update Curriculum";
                target.modal('show');
            });
    };

    $scope.check = function (currObj) {
        $http.put(urlTail + '/ToLive/' + currObj.id, !currObj.isLive).success(function (result) {
            getResultsPage($scope.pagination.current);
        }).error(function (data, status, headers, config) {

        });
    }

}]);

app.controller('StController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', function ($scope, $http, toaster, $q, $routeParams, $location) {

}]);

app.controller('ClubPlayerController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', '$filter', '$rootScope', function ($scope, $http, toaster, $q, $routeParams, $location, $filter, $rootScope) {

    var needToDelete = -1;
    var urlTail = '/api/Player';
    var sortArray = [];


    function shuffle(objArr) {
        var ids = [];
        angular.forEach(objArr, function (obj) {
            this.push(obj.id);
        }, ids);
        return ids;
    }

    function morph(arrayOfTeamIds, arrayAvibleTeams) {
        var connectedTeams = [];
        for (var i = 0; i < arrayAvibleTeams.length; i++) {
            for (var j = 0; j < arrayOfTeamIds.length; j++) {
                if (arrayAvibleTeams[i].id === arrayOfTeamIds[j]) {
                    connectedTeams.push(arrayAvibleTeams[i]);
                };
            };
        };
        return connectedTeams;
    }

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
    $scope.help = {};
    $scope.help.teams = [];

    $http.get('/api/Teams/List').success(function (result) {
        console.log(result);
        $scope.teams = result;
    });

    $scope.$watch('help.teams', function (newValue, oldValue, scope) {
        if (newValue.length > 2) {
            $scope.help.teams = oldValue;

        }
        console.log($scope);
    });


    $scope.open = function () {
        $scope.opened = true;
    };

    function createTail(pageNumber) {
        if (sortArray.length > 0) {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
        } else {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber;
        }
    }

    function getResultsPage(pageNumber) {
        $http.get(createTail(pageNumber))
            .success(function (result) {
                console.log(result);
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    }

    $rootScope.$watchGroup(['orderField', 'revers'], function (newValue, oldValue, scope) {
        sortArray = newValue;
        $http.get(createTail($scope.pagination.current))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    });

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
        $scope.loginLoading = true;
        $scope.myform.form_Submitted = !$scope.myform.$valid;
        if (!$scope.myform.$valid) {
            $scope.loginLoading = false;
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
                        $scope.loginLoading = false;
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
                if ($scope.help.teams == null) {
                    $scope.newPlayer.teams = [];
                } else {
                    $scope.newPlayer.teams = shuffle($scope.help.teams);
                }
                console.log($scope.newPlayer);
                if (id != null) {
                    $http.put(urlTail + '/' + id, $scope.newPlayer)
                        .success(function () {
                            getResultsPage($scope.pagination.current);
                            target.modal('hide');
                            $scope.loginLoading = false;
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
                                $scope.loginLoading = false;
                            }
                        });
                } else {

                    $scope.newPlayer.status = $scope.selectedStatus.id;
                    $scope.newPlayer.playingFoot = $scope.selectedFoot.id;

                    if ($scope.help.teams == null) {
                        $scope.newPlayer.teams = [];
                    } else {
                        $scope.newPlayer.teams = shuffle($scope.help.teams);
                    }
                    $http.post(urlTail, $scope.newPlayer)
                        .success(function () {
                            getResultsPage($scope.pagination.current);
                            target.modal('hide');
                            $scope.loginLoading = false;
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
                                $scope.loginLoading = false;
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
                console.log('ResArray');
                console.log($filter('filter')($scope.teams, result.teams));
                $scope.help.teams = morph(result.teams, $scope.teams);
                target.modal('show');
            });
    };
}]);


app.controller('TeamsController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', '$rootScope', function ($scope, $http, toaster, $q, $routeParams, $location, $rootScope) {
    var needToDelete = -1;
    var urlTail = '/api/Teams';
    var target = angular.element('#addTeamModal');
    var deleteModal = angular.element('#confDelete');
    var sortArray = [];

    $scope.isEditing = false;
    $scope.newTeam = {};
    $scope.curriculumTypesList = [];
    $scope.teamMembers = {};
    $scope.teamMembers.coaches = [];
    $scope.teamMembers.players = [];
    $scope.freeCoaches = [];
    $scope.freePlayers = [];
    $scope.allPlayers = [];

    function shuffle(objArr) {
        var ids = [];
        angular.forEach(objArr, function (obj) {
            this.push(obj.id);
        }, ids);
        return ids;
    }

    function morph(arrayOfTeamIds, arrayAvibleTeams) {
        var connectedTeams = [];
        for (var i = 0; i < arrayAvibleTeams.length; i++) {
            for (var j = 0; j < arrayOfTeamIds.length; j++) {
                if (arrayAvibleTeams[i].id === arrayOfTeamIds[j]) {
                    connectedTeams.push(arrayAvibleTeams[i]);
                };
            };
        };
        return connectedTeams;
    }

    function getCoachList() {
        $http.get('/api/Coaches/list').success(function (result) {
            $scope.freeCoaches = result;
        });
    }

    function getPlayerList() {
        $http.get('/api/Player/Free').success(function (result) {
            $scope.freePlayers = result;
        });
    }

    function getAllPlayer() {
        $http.get('/api/Player/List').success(function (result) {
            $scope.allPlayers = result;
        });
    }

    function createTail(pageNumber) {
        if (sortArray.length > 0) {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
        } else {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber;
        }
    }

    function getResultsPage(pageNumber) {
        $http.get(createTail(pageNumber))
            .success(function (result) {
                console.log(result);
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    }

    $rootScope.$watchGroup(['orderField', 'revers'], function (newValue, oldValue, scope) {
        sortArray = newValue;
        $http.get(createTail($scope.pagination.current))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    });

    function getCurrType() {
        $http.get('/api/Curriculums/List').success(function (result) {
            $scope.curriculumTypesList = result;
            $scope.selectedCurriculumTypeId = $scope.curriculumTypesList[0];
        });
    }

    $scope.items = [];
    $scope.totalItems = 0;
    $scope.itemsPerPage = 20;


    $scope.pagination = {
        current: 1
    };

    getResultsPage($scope.pagination.current);
    getCurrType();
    getPlayerList();
    getCoachList();
    getAllPlayer();

    $scope.pageChanged = function (newPage) {
        getResultsPage(newPage);
        $scope.pagination.current = newPage;
    };


    $scope.open = function () {
        $scope.isEditing = false;
        $scope.modalTitle = 'Add Team';
        target.modal('show');
    };

    $scope.openDelete = function (id) {
        needToDelete = id;
        deleteModal.modal('show');
    };

    $scope.delete = function () {
        $http.delete(urlTail + '/' + needToDelete).success(function () {
            needToDelete = -1;
            console.log('Delete DONE!');
            getResultsPage($scope.pagination.current);
            deleteModal.modal('hide');
        }).error(function (data, status, headers, config) {

        });
    }

    $scope.cancel = function () {
        needToDelete = -1;
        target.modal('hide');
        deleteModal.modal('hide');
    };

    $scope.ok = function (id) {
        $scope.loginLoading = true;
        $scope.ok = function (id) {

            $scope.newTeam.curriculumId = $scope.selectedCurriculumTypeId.id;
            $scope.newTeam.coaches = shuffle($scope.teamMembers.coaches);
            $scope.newTeam.players = shuffle($scope.teamMembers.players);
            console.log(id);
            if (id != null) {

                //PUT it now have no url to Update date
                $http.put(urlTail + '/' + id, $scope.newTeam).success(function () {
                    console.log('Team Update');
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                    $scope.newTeam = {};
                    $scope.teamMembers.coaches = [];
                    $scope.teamMembers.players = [];
                }).error(function (data, status, headers, config) {

                });
                $http.put().success().error();
                $scope.loginLoading = false;
            } else {
                //POST

                $http.post(urlTail, $scope.newTeam).success(function (result) {
                    console.log('Team Done');
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                    $scope.newTeam = {};
                    $scope.teamMembers.coaches = [];
                    $scope.teamMembers.players = [];
                    $scope.loginLoading = false;
                }).error(function (data, status, headers, config) {

                });
            }
        };

        $scope.openEdit = function (id) {
            $scope.isEditing = true;
            $http.get(urlTail + '/' + id)
                .success(function (result) {
                    console.log(result);
                    $scope.newTeam = result;
                    $scope.teamMembers.coaches = morph(result.coaches, $scope.freeCoaches);
                    $scope.teamMembers.players = morph(result.players, $scope.allPlayers);
                    target.modal('show');
                });
        };
    }
}]);

app.controller('CurrStatementsController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', '$filter', '$rootScope', function ($scope, $http, toaster, $q, $routeParams, $location, $filter, $rootScope) {

    var needToDelete = -1;
    var urlTail = '/api/CurriculumStatement';
    var target = angular.element('#addStateModal');
    var deleteModal = angular.element('#confDelete');
    var sortArray = [];

    $scope.help = {};
    $scope.help.usersType = [];
    $scope.revers = false;

    $scope.roles = [
       { id: 1, name: 'Club Admin' },
       { id: 2, name: 'Head Of Academies' },
       { id: 3, name: 'Coach' },
       { id: 4, name: 'Head Of Education' },
       { id: 5, name: 'Welfare Officer' },
       { id: 7, name: 'Physiotherapist' },
       { id: 8, name: 'Sports Scientist' }
    ];

    $scope.rolesDate = [
       { id: 0, name: 'SysAdmin' },
       { id: 1, name: 'Club Admin' },
       { id: 2, name: 'Head Of Academies' },
       { id: 3, name: 'Coach' },
       { id: 4, name: 'Head Of Education' },
       { id: 5, name: 'Welfare Officer' },
       { id: 7, name: 'Physiotherapist' },
       { id: 8, name: 'Sports Scientist' }
    ];

    function shuffle(objArr) {
        var ids = [];
        angular.forEach(objArr, function (obj) {
            this.push(obj.id);
        }, ids);
        return ids;
    }

    function reShuffle(idsArry) {
        var objs = []
        for (var i = 0; i < idsArry.length; i++) {
            for (var j = 0; j < $scope.roles.length; j++) {
                if (idsArry[i] === $scope.roles[j].id) {
                    objs.push($scope.roles[j]);
                }
            };
        };
        return objs;
    }

    function createTail(pageNumber) {
        if (sortArray.length > 0) {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
        } else {
            return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber;
        }
    }

    function getResultsPage(pageNumber) {
        $http.get(createTail(pageNumber))
            .success(function (result) {
                console.log(result);
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    }

    $rootScope.$watchGroup(['orderField', 'revers'], function (newValue, oldValue, scope) {
        sortArray = newValue;
        $http.get(createTail($scope.pagination.current))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    });

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


    $scope.open = function () {
        $scope.modalTitle = 'Add Curriculum Statement';
        $scope.newStatements = {};
        target.modal('show');
    };

    $scope.openDelete = function (id) {
        needToDelete = id;
        deleteModal.modal('show');
    };

    $scope.delete = function () {
        $http.delete(urlTail + '/' + needToDelete).success(function () {
            needToDelete = -1;
            getResultsPage($scope.pagination.current);
            deleteModal.modal('hide');
        }).error(function (data, status, headers, config) {

        });
    }

    $scope.cancel = function () {
        needToDelete = -1;
        target.modal('hide');
        deleteModal.modal('hide');
    };

    $scope.ok = function (id) {

        $scope.newStatements.roles = shuffle($scope.help.usersType);

        console.log($scope.newStatements);
        if (id != null) {

            //PUT it now have no url to Update date
            $http.put(urlTail + '/' + id, $scope.newStatements).success(function () {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
            }).error(function (data, status, headers, config) {

            });
        } else {
            //POST

            $http.post(urlTail, $scope.newStatements).success(function (result) {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
            }).error(function (data, status, headers, config) {

            });
        }
    };

    $scope.openEdit = function (id) {
        $http.get(urlTail + '/' + id)
            .success(function (result) {
                console.log(result);
                $scope.newStatements = result;
                $scope.help.usersType = reShuffle(result.roles);
                target.modal('show');
            });
    };

}]);

app.controller('CurrDetailsController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', '$rootScope', function ($scope, $http, toaster, $q, $routeParams, $location, $rootScope) {

    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];
    $scope.scenarios = [];
    var sortArray = [];

    var needToDelete = -1;
    var urlTail = '/api/Sessions';
    var target = angular.element('#addCurrDetalModal');
    var deleteModal = angular.element('#confDelete');

    //toggle
    var toggleAttendance = angular.element('#toggleAttendance');
    var toggleObjectives = angular.element('#toggleObjectives');
    var toggleRating = angular.element('#toggleRating');
    var toggleReport = angular.element('#toggleReport');
    var toggleObjectiveReport = angular.element('#toggleObjectiveReport');
    var toggleCoachDetails = angular.element('#toggleCoachDetails');
    var togglePlayerDetails = angular.element('#togglePlayerDetails');
    var toggleStartofReviewPeriod = angular.element('#toggleStartofReviewPeriod');
    var toggleEndofReviewPeriod = angular.element('#toggleEndofReviewPeriod');
    var toggleNeedScenarios = angular.element('#toggleNeedScenarios');


    $scope.help = {};
    $scope.help.scenarios = [];

    $scope.roles = [
       { id: 1, name: 'Club Admin' },
       { id: 2, name: 'Head Of Academies' },
       { id: 3, name: 'Coach' },
       { id: 4, name: 'Head Of Education' },
       { id: 5, name: 'Welfare Officer' },
       { id: 7, name: 'Physiotherapist' },
       { id: 8, name: 'Sports Scientist' }
    ];

    function shuffle(objArr) {
        var ids = [];
        angular.forEach(objArr, function (obj) {
            this.push(obj.id);
        }, ids);
        return ids;
    }

    function reShuffle(idsArry) {
        var objs = []
        for (var i = 0; i < idsArry.length; i++) {
            for (var j = 0; j < $scope.scenarios.length; j++) {
                if (idsArry[i] === $scope.scenarios[j].id) {
                    objs.push($scope.scenarios[j]);
                }
            };
        };
        return objs;
    }

    function getScenarios() {
        $http.get('/api/Scenarios/List').success(function (result) {
            $scope.scenarios = result;
            $scope.selectedScenario = result[0];
        }).error();
    }

    function getParentCurr() {
        $http.get('/api/Curriculums/' + $scope.currId).success(function (result) {
            $scope.parentCurr = result;
        });
    }

    function createTail(pageNumber) {
        if (sortArray.length > 0) {
            return urlTail + '/' + $scope.currId + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
        } else {
            return urlTail + '/' + $scope.currId + '/' + $scope.itemsPerPage + '/' + pageNumber;
        }
    }

    function getResultsPage(pageNumber) {
        $http.get(createTail(pageNumber))
            .success(function (result) {
                console.log(result);
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    }

    $rootScope.$watchGroup(['orderField', 'revers'], function (newValue, oldValue, scope) {
        sortArray = newValue;
        $http.get(createTail($scope.pagination.current))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    });

    $scope.items = [];
    $scope.totalItems = 0;
    $scope.itemsPerPage = 20;


    $scope.pagination = {
        current: 1
    };

    getResultsPage($scope.pagination.current);
    getParentCurr();
    getScenarios();

    $scope.pageChanged = function (newPage) {
        getResultsPage(newPage);
        $scope.pagination.current = newPage;
    };


    $scope.open = function () {
        $scope.modalTitle = 'Add Curriculum Session';
        target.modal('show');
    };

    $scope.openDelete = function (id) {
        needToDelete = id;
        deleteModal.modal('show');
    };

    $scope.delete = function () {
        $http.delete(urlTail + '/' + needToDelete).success(function () {
            needToDelete = -1;
            getResultsPage($scope.pagination.current);
            deleteModal.modal('hide');
        }).error(function (data, status, headers, config) {

        });
    }

    $scope.cancel = function () {
        needToDelete = -1;
        $scope.newCurrDet = {}
        target.modal('hide');
        deleteModal.modal('hide');
    };

    $scope.ok = function (id) {
        
        //Files upload
        var promises = [];

        if ($scope.picC) {
            var fd = new FormData();
            fd.append('file', $scope.picC);
            var promise = $http.post('/api/Files', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
                .success(function (data) {
                    $scope.newCurrDet.coachPicture = data.name;
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

        if ($scope.picPF) {
            var fd = new FormData();
            fd.append('file', $scope.picPF);
            var promise = $http.post('/api/Files', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
                .success(function (data) {
                    $scope.newCurrDet.playerPicture = data.name;
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
            $scope.newCurrDet.scenarioId = $scope.selectedScenario.id;
            $scope.newCurrDet.attendance = toggleAttendance.prop('checked');
            $scope.newCurrDet.objectives = toggleObjectives.prop('checked');
            $scope.newCurrDet.rating = toggleRating.prop('checked');
            $scope.newCurrDet.report = toggleReport.prop('checked');
            $scope.newCurrDet.objectiveReport = toggleObjectiveReport.prop('checked');
            $scope.newCurrDet.coachDetails = toggleCoachDetails.prop('checked');
            $scope.newCurrDet.startOfReviewPeriod = toggleStartofReviewPeriod.prop('checked');
            $scope.newCurrDet.endOfReviewPeriod = toggleEndofReviewPeriod.prop('checked');
            $scope.newCurrDet.playerDetails = togglePlayerDetails.prop('checked');
            $scope.newCurrDet.needScenarios = toggleNeedScenarios.prop('checked');
            $scope.newCurrDet.scenarios = shuffle($scope.help.scenarios);

            console.log($scope.newCurrDet);
            if (id != null) {
                //PUT it now have no url to Update date
                $http.put(urlTail + '/' + id, $scope.newCurrDet).success(function () {
                    $scope.newCurrDet = {}
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                }).error(function (data, status, headers, config) {

                });
            } else {
                //POST
                $http.post(urlTail + '/' + $scope.currId, $scope.newCurrDet).success(function (result) {
                    getResultsPage($scope.pagination.current);
                    $scope.newCurrDet = {};
                    target.modal('hide');
                }).error(function (data, status, headers, config) {

                });
            }
        });

    };

    $scope.openEdit = function (id) {
        $scope.modalTitle = 'Update Curriculum Session';
        $http.get(urlTail + '/' + id)
            .success(function (result) {
                
                
                console.log(result);

                $scope.newCurrDet = result;
                $scope.help.scenarios = reShuffle(result.scenarios);

                toggleAttendance.bootstrapToggle(result.attendance ? 'on' : 'off');
                toggleObjectives.bootstrapToggle(result.objectives ? 'on' : 'off');
                toggleRating.bootstrapToggle(result.rating ? 'on' : 'off');
                toggleReport.bootstrapToggle(result.report ? 'on' : 'off');
                toggleObjectiveReport.bootstrapToggle(result.objectiveReport ? 'on' : 'off');
                toggleCoachDetails.bootstrapToggle(result.coachDetails ? 'on' : 'off');
                togglePlayerDetails.bootstrapToggle(result.playerDetails ? 'on' : 'off');
                toggleStartofReviewPeriod.bootstrapToggle(result.startOfReviewPeriod ? 'on' : 'off');
                toggleEndofReviewPeriod.bootstrapToggle(result.endOfReviewPeriod ? 'on' : 'off');
                toggleNeedScenarios.bootstrapToggle(result.needScenarios ? 'on' : 'off');

                target.modal('show');
            });
    };
}]);