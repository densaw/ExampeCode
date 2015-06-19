var app = angular.module('MainApp');


app.controller('TalentIdController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', '$rootScope', function ($scope, $http, toaster, $q, $routeParams, $location, $rootScope) {


    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    var confInvite = angular.element('#confInvite');
    var cancelInvite = angular.element('#cancelInvite');
    var confaddNotes = angular.element('#confaddNotes');
    var cancelNotes = angular.element('#cancelNotes');
    var saveInvite = angular.element('#saveInvite');

    var sortArray = [];
    var urlTail = '/api/TalentPlayerAttributes/';
    //get Player Detail
    $scope.profileTalents = [];

    function getParentCurr() {
        $http.get('/api/TalentIdentification/Detail/' + $scope.currId).success(function (result) {
            $scope.profileTalents = result;

        });
    }
    //get Assesments
    $scope.profileAssesments = [];

    function getParentAssesmets() {
        $http.get('/api/TalentIdentificationNotes/' + $scope.currId).success(function (result) {
            $scope.profileAssesments = result;

        });
    }
    //get Attributes


    var toggleAttendance = angular.element('#toggleAttendance');
    var toggleInvite = angular.element('#toggleInvite');
    var toggleJoined = angular.element('#toggleJoined');


    //invite-------------------------------------------------------------------------
    $scope.currName = '';

    $scope.confInvite = function () {

        toggleInvite.bootstrapToggle($scope.profileTalents.invitedToTrial ? 'on' : 'off');
        toggleJoined.bootstrapToggle($scope.profileTalents.attendedTrail ? 'on' : 'off');
        toggleAttendance.bootstrapToggle($scope.profileTalents.joinedClub ? 'on' : 'off');

        $scope.modalTitle = 'Invite Player';
        getParentCurr();

        console.log($scope.currId);
        $scope.modalTitle = "Update Type";
        confInvite.modal('show');
    };

    $scope.cancelInvite = function () {
        $scope.modalTitle = 'Invite Player';
        confInvite.modal('hide');
    };

    $scope.okInvite = function () {
        $scope.loginLoading = true;
        $scope.myform.form_Submitted = !$scope.myform.$valid;

        $scope.loginLoading = false;
        $http.put('/api/TalentIdentification/Invite/' + $scope.currId,
            {
                "invitedToTrial": toggleInvite.prop('checked'),
                "joinedClub": toggleJoined.prop('checked'),
                "attendedTrail": toggleAttendance.prop('checked')
            }
        ).success(function () {

            getParentCurr();
            confInvite.modal('hide');
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

    };

    //ivite end---------------------------------------------------------------------

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

    $scope.pageChanged = function (newPage) {
        getResultsPage(newPage);
        $scope.pagination.current = newPage;
    };


    getParentAssesmets();
    getParentCurr();


    //Notes start-----------------------------------------------------------------------------------
    var sortArray1 = [];
    var aTail = '/api/TalentIdentificationNotes';

    $scope.items1 = [];
    $scope.totalItems1 = 0;
    $scope.itemsPerPage1 = 20;


    $scope.pagination1 = {
        current: 1
    };

    function eTail(pageNumber1) {
        if (sortArray1.length > 0) {
            return aTail + '/' + $scope.currId + '/' + $scope.itemsPerPage1 + '/' + pageNumber1 + '/' + sortArray1[0] + '/' + sortArray1[1];
        } else {
            return aTail + '/' + $scope.currId + '/' + $scope.itemsPerPage1 + '/' + pageNumber1;
        }
    }

    function getPage(pageNumber1) {
        $http.get(eTail(pageNumber1))
            .success(function (result1) {
                console.log("log");
                console.log(result1);
                $scope.items1 = result1.items;
                $scope.totalItems1 = result1.count;

            });
    }

    $rootScope.$watchGroup(['orderField', 'revers'], function (newValue, oldValue, scope) {
        sortArray1 = newValue;
        $http.get(eTail($scope.pagination1.current))
            .success(function (result1) {
                $scope.items1 = result1.items;
                $scope.totalItems1 = result1.count;
            });
    });



    getPage($scope.pagination1.current);

    $scope.pageC = function (newPage1) {
        getPage(newPage1);
        $scope.pagination1.current = newPage1;
    };
    $scope.scouts = [];

    $http.get('/api/Scouts/List').success(function (result) {
        $scope.scouts = result;
    });
    $scope.newNote = {};

    $scope.okNotes = function (id) {
        $scope.loginLoading = true;
        $scope.myform.form_Submitted = !$scope.myform.$valid;
        if (id != null) {

            //PUT it now have no url to Update date
            $http.put('/api/TalentIdentificationNotes' + id, $scope.newNote).success(function (result) {
                getPage($scope.pagination1.current);
                console.log(id);
                $scope.loginLoading = false;
                $scope.newNote = {};
                confaddNotes.modal('hide');
            }).error(function (data, status, headers, config) {
                $scope.loginLoading = false;
            });
        } else {
            //POST
            $scope.newNote.talentIdentificationId = $scope.currId;
            $http.post('/api/TalentIdentificationNotes', $scope.newNote).success(function (result) {
                getPage($scope.pagination1.current);
                confaddNotes.modal('hide');
                $scope.newNote = {};
                $scope.loginLoading = false;
            }).error(function (data, status, headers, config) {
                $scope.loginLoading = false;
            });
        }
    };
    $scope.addNotes = function () {
        $scope.modalTitle = 'Add Notes';
        confaddNotes.modal('show');

    };

    $scope.cancelNotes = function () {

        confaddNotes.modal('hide');
    };

    //Notes end----------------------------------------------------------------------------------------------------
    $scope.checkAtt = function (item) {

        $http.post('/api/TalentPlayerAttributes', item).success(function () {


        });

    };
    $scope.checkScore = function (item) {
        console.log(" lox");
        $http.post('/api/TalentPlayerAttributes', item).success(function () {


        });

    };
}]);


app.controller('TalentIdentificationController', ['$scope', '$http', 'toaster', '$q', '$routeParams', '$location', '$rootScope', function ($scope, $http, toaster, $q, $routeParams, $location, $rootScope) {
    var pathArray = $location.$$absUrl.split("/");
    $scope.currId = pathArray[pathArray.length - 1];

    //Variable section
    var date = new Date();


    var needToDelete = -1;

    var urlTail = '/api/TalentIdentification';
    var target = angular.element('#addScoutP');
    var confDelete = angular.element('#confDelete');


    var sortArray = [];
    //get scout


    $scope.scouts = [];

    $http.get('/api/Scouts/List').success(function (result) {
        $scope.scouts = result;
    });

    //end


    $scope.newScoutP = {};

    $scope.curriculumTypesList = [];


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
        $scope.modalTitle = 'Add Scouted Player';
        target.modal('show');
    };

    $scope.cancel = function () {
        target.modal('hide');
        confDelete.modal('hide');
        needToDelete = -1;
    };


    $scope.openEdit = function (id) {
        console.log(id);
        $http.get(urlTail + '/' + id)
            .success(function (result) {
                $scope.newScoutP = result;
                $scope.myform.form_Submitted = false;
                $scope.modalTitle = "Update Scouted Player";
                target.modal('show');
            });
    };

    $scope.ok = function (id) {
        $scope.loginLoading = true;
        $scope.myform.form_Submitted = !$scope.myform.$valid;
        if (id != null) {

            //PUT it now have no url to Update date
            $http.put(urlTail + '/' + id, $scope.newScoutP).success(function (result) {
                getResultsPage($scope.pagination.current);
                console.log(id);
                $scope.loginLoading = false;
                $scope.newScoutP = {};
                target.modal('hide');
            }).error(function (data, status, headers, config) {
                $scope.loginLoading = false;
            });
        } else {
            //POST
            $http.post(urlTail, $scope.newScoutP).success(function (result) {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
                $scope.newScoutP = {};
                $scope.loginLoading = false;
            }).error(function (data, status, headers, config) {
                $scope.loginLoading = false;
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




    $scope.check = function (playerObj) {
        $http.put(urlTail + '/Invite/' + playerObj.id, !playerObj.isLive).success(function (result) {
            getResultsPage($scope.pagination.current);
        }).error(function (data, status, headers, config) {

        });
    }

    function getClubName() {
        $http.get('/api/ClubAdminDashboard/ClubName').success(function (result) {
            $scope.clubName = result;
        }).error(function (data, status, headers, config) {

        });
    }
    getClubName();



}]);