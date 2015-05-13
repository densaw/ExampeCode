var app = angular.module('MainApp');

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

app.controller('TrainingTeamController', ['$scope', '$http', 'toaster', function($scope, $http, toaster) {

    

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
    

    $scope.openModal = function() {
        target.modal('show');
    };

    



    $scope.send = function () {
        $scope.newMember.userStatus = 0;
        $scope.newMember.role = $scope.selectedRole.id;
        $scope.newMember.profilePicture = 'tmp.png';
        $http.post(urlTail, $scope.newMember).success(function(result) {
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
    };

    $http.get(urlTail)
           .success(function (result) {
               $scope.items = result;
           });

}]);

app.controller('ToDoController', ['$scope', '$http', 'toaster', function($scope, $http, toaster) {

    var needToDelete = -1;
    

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

    $scope.open = function() {
        target.modal('show');
    }

    $scope.showDelete = function (id) {
        needToDelete = id;
        deleteConf.modal('show');
    }

    $scope.cancel = function() {
        deleteConf.modal('hide');
    }

    $scope.delete = function () {
        $http.delete(urlTail + '/' + needToDelete).success(function () {
            getResults();
            needToDelete = 1;
            deleteConf.modal('hide');
        });
    }

    $scope.ok = function () {
        $scope.newNote.priority = $scope.selectedPriority.id;
        console.log($scope.newNote);
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

}]);

app.controller('ClubDiaryController', ['$scope', '$http', 'toaster', '$compile', 'uiCalendarConfig', function ($scope, $http, toaster, $compile, uiCalendarConfig) {

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    console.log(date);

    $scope.events = [
      { title: 'All Day Event', start: new Date(y, m, 1) },
      { title: 'Long Event', start: new Date(y, m, d - 5), end: new Date(y, m, d - 2) },
      { id: 999, title: 'Repeating Event', start: new Date(y, m, d - 3, 16, 0), allDay: false },
      { id: 999, title: 'Repeating Event', start: new Date(y, m, d + 4, 16, 0), allDay: false },
      { title: 'Birthday Party', start: new Date(y, m, d + 1, 19, 0), end: new Date(y, m, d + 1, 22, 30), allDay: false }
    ];


    $scope.alertOnEventClick = function (date, jsEvent, view) {
        $scope.alertMessage = (date.title + ' was clicked ');
    };
    /* alert on Drop */
    $scope.alertOnDrop = function (event, delta, revertFunc, jsEvent, ui, view) {
        $scope.alertMessage = ('Event Droped to make dayDelta ' + delta);
    };
    /* alert on Resize */
    $scope.alertOnResize = function (event, delta, revertFunc, jsEvent, ui, view) {
        $scope.alertMessage = ('Event Resized to make dayDelta ' + delta);
    };

    $scope.eventRender = function (event, element, view) {
        element.attr({
            'tooltip': event.title,
            'tooltip-append-to-body': true
        });
        $compile(element)($scope);
    };

    /* add and removes an event source of choice */
    $scope.addRemoveEventSource = function (sources, source) {
        var canAdd = 0;
        angular.forEach(sources, function (value, key) {
            if (sources[key] === source) {
                sources.splice(key, 1);
                canAdd = 1;
            }
        });
        if (canAdd === 0) {
            sources.push(source);
        }
    };
    /* add custom event*/
    $scope.addEvent = function () {
        $scope.events.push({
            title: 'Open Sesame',
            start: new Date(y, m, 28),
            end: new Date(y, m, 29),
            className: ['openSesame']
        });
    };
    /* remove event */
    $scope.remove = function (index) {
        $scope.events.splice(index, 1);
    };
    /* Change View */
    $scope.changeView = function (view, calendar) {
        uiCalendarConfig.calendars[calendar].fullCalendar('changeView', view);
    };
    /* Change View */
    $scope.renderCalender = function (calendar) {
        if (uiCalendarConfig.calendars[calendar]) {
            uiCalendarConfig.calendars[calendar].fullCalendar('render');
        }
    };

    $scope.uiConfig = {
        calendar: {
            height: 450,
            editable: true,
            header: {
                left: 'month basicWeek basicDay agendaWeek agendaDay',
                center: 'title',
                right: 'today prev,next'
            },
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize,
            eventRender: $scope.eventRender
        }
    };
}]);