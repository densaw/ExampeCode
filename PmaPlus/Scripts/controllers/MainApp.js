(function () {
    var module = angular.module('MainApp', ['tc.chartjs', 'angularUtils.directives.dirPagination', 'ui.bootstrap', 'ngCookies']);

    module.filter('curr', function() {
        return function (v, yes, no) {
            return v ? yes : no;
        };
    });

    module.filter('nav', function() {
        return function (v, yes, no) {
            return v ? yes : no;
        };
    });

    module.controller('MainController', ['$scope', '$cookies', function ($scope, $cookies) {
        $scope.expanded = true;
        $scope.navExpand = function() {
            $scope.expanded = !$scope.expanded;
            $cookies.expanded = $scope.expanded;
        }
    }]);

    module.controller('ChartController', ['$scope', '$http', function ($scope, $http) {
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

        $http.get('api/dashboard/active/players').success(function (data) {

            var monthArray = new Array;
            var playerCountArray = new Array;
            data.forEach(function (val) {
                monthArray.push(monthNames[val.month - 1]);
                playerCountArray.push(val.activePlayers);
            });

            $scope.data = {
                labels: monthArray,
                datasets: [
                    {
                        label: "Example dataset",
                        fillColor: "rgba(66,139,202,0.5)",
                        strokeColor: "rgba(66,139,202,0.7)",
                        pointColor: "rgba(66,139,202,1)",
                        pointStrokeColor: "#fff",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: "rgba(26,179,148,1)",
                        data: playerCountArray
                    },
                    {
                        label: "My Second dataset",
                        fillColor: "rgba(151,187,205,0.2)",
                        strokeColor: "rgba(151,187,205,1)",
                        pointColor: "rgba(151,187,205,1)",
                        pointStrokeColor: "#fff",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: "rgba(151,187,205,1)",
                        data: [2500, 2500, 2500, 2500, 2500, 2500, 2500, 2500, 2500, 2500, 2500, 2500]
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
    }]);
    module.controller('PlayersBoxController', ['$scope', '$http', function ($scope, $http) {
        $http.get('api/dashboard/logged/players').success(function (data) {
            $scope.amountPlayers = data.amount;
            $scope.progressPlayer = data.progress;
            $scope.percentage = data.percentage;
        });
    }]);
    module.controller('CoachsBoxController', ['$scope', '$http', function ($scope, $http) {
        $http.get('api/dashboard/logged/coaches').success(function (data) {
            $scope.amountCoaches = data.amount;
            $scope.progressCoach = data.progress;
            $scope.percentage = data.percentage;
        });
    }]);
    module.controller('ClubsBoxController', ['$scope', '$http', function ($scope, $http) {
        $http.get('api/dashboard/logged/clubs').success(function (data) {
            $scope.amountClubs = data.amount;
            $scope.progressClub = data.progress;
            $scope.percentage = data.percentage;
        });
    }]);
    module.controller('UsersBoxController', ['$scope', '$http', function ($scope, $http) {
        $http.get('api/dashboard/logged/users').success(function (data) {
            $scope.amountUsers = data.amount;
            $scope.progressUser = data.progress;
            $scope.percentage = data.percentage;
        });
    }]);
    module.controller('PlayerLoginHistoryController', ['$scope', '$http', function ($scope, $http) {
        $http.get('api/dashboard/logged/players/10/weeks').success(function (data) {
            $scope.data = {
                //labels: ["January", "February", "March", "April", "May", "June", "July"],
                labels: ["", "", "", "", "", "", ""],
                //labels: new Array(7),
                datasets: [
                    {
                        label: "My First dataset",
                        fillColor: "rgba(26,179,148,1)",
                        strokeColor: "rgba(26,179,148,1)",
                        highlightFill: "#fff",
                        highlightStroke: "#fff",
                        data: [65, 59, 80, 81, 56, 55, 40]
                    }
                ]
            };
            $scope.options = {
                scaleBeginAtZero: true,
                scaleShowGridLines: false,
                scaleGridLineColor: "rgba(0,0,0,.00)",
                scaleGridLineWidth: 1,
                scaleShowHorizontalLines: false,
                scaleShowVerticalLines: false,
                barShowStroke: false,
                barStrokeWidth: 2,
                barValueSpacing: 5,
                barDatasetSpacing: 1,
                showXLabels: 2
            };

        });
    }]);
    module.controller('AllPlayerController', ['$scope', '$http', function ($scope, $http) {
        $http.get('api/dashboard/active/players/all').success(function (data) {
            $scope.playerCount = data;
        });
    }]);
    module.controller('FaCoursesController', ['$scope', '$http', function ($scope, $http) {

        var needToDelete = -1;

        function getResultsPage(pageNumber) {
            console.log($scope.coursePerPage);
            $http.get('/api/FaCourses/' + $scope.coursePerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.courses = result.items;
                    $scope.totalCourses = result.count;
                });
        }

        $scope.courses = [];
        $scope.totalCourses = 0;
        $scope.coursePerPage = 20; // this should match however many results your API puts on one page


        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };
        var target = angular.element('#addCurseModal');
        var confDelete = angular.element('#confDelete');

        $scope.ok = function (id) {
            if (id != null) {
                $http.put('/api/FaCourses/' + id, $scope.newCourse).success(function () {
                    getResultsPage($scope.pagination.current);
                    $scope.newCourse = null;
                    target.modal('hide');
                });
            } else {

                $http.post('/api/FaCourses', $scope.newCourse).success(function () {
                    getResultsPage($scope.pagination.current);
                    $scope.newCourse = null;
                    target.modal('hide');
                });
            }
            $scope.modalTitle = "Add a Course";
        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
            needToDelete = -1;
            $scope.newClub = null;
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Course";
            target.modal('show');
        };

        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };
        $scope.delete = function () {
            $http.delete('/api/FaCourses/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        };
        $scope.openEdit = function (id) {
            $http.get('/api/FaCourses/' + id)
                .success(function (result) {
                    $scope.newCourse = result;
                    $scope.modalTitle = "Update Course";
                    target.modal('show');

                });
        };

    }]);

    module.controller('ClubsController', ['$scope', '$http', function ($scope, $http) {

        var needToDelete = -1;

        $scope.statuses = [
            { id: 0, name: 'Active' },
            { id: 1, name: 'Blocked' },
            { id: 2, name: 'Closed' }
        ];

        $scope.selectedStatus = $scope.statuses[0];

        function getResultsPage(pageNumber) {
            $http.get('/api/Clubs/' + $scope.clubsPerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.clubs = result.items;
                    $scope.totalClubs = result.count;
                });
        }

        $scope.clubs = [];
        $scope.totalClubs = 0;
        $scope.clubsPerPage = 20; // this should match however many results your API puts on one page
        //$scope.newClub = {};

        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
        };
        var target = angular.element('#addClubModal');
        var confDelete = angular.element('#confDelete');

        $scope.ok = function (id) {

            if (id != null) {
                $scope.newClub.logo = 'tmp.jpeg';
                $scope.newClub.background = 'tmp.jpeg';
                $scope.newClub.status = $scope.selectedStatus.id;
                $http.put('/api/Clubs/' + id, $scope.newClub).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                    $scope.newClub = null;
                });
                target.modal('hide');

            } else {

                $scope.newClub.logo = 'tmp.jpeg';
                $scope.newClub.background = 'tmp.jpeg';
                $scope.newClub.status = $scope.selectedStatus.id;
                $http.post('/api/Clubs', $scope.newClub).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                    $scope.newClub = null;
                });
                target.modal('hide');
            };
        }
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
            $scope.newClub = null;
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Club";
            target.modal('show');
        };
        $scope.delete = function () {
            $http.delete('/api/Clubs/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        };
        $scope.openEdit = function (id) {
            $http.get('/api/Clubs/' + id)
                .success(function (result) {
                    $scope.newClub = result;
                    $scope.modalTitle = "Update Club";
                    target.modal('show');

                });
        };

    }]);

    module.controller('CurriculumTypesController', ['$scope', '$http', function ($scope, $http) {

        var needToDelete = -1;

        function getResultsPage(pageNumber) {
            $http.get('/api/CurriculumTypes/' + $scope.curriculumsPerPage + '/' + pageNumber)
                .success(function (result) {
                    console.log(result);
                    $scope.curriculums = result.items;
                    $scope.totalCurriculums = result.count;
                });
        }

        $scope.сurriculums = [];
        $scope.totalCurriculums = 0;
        $scope.curriculumsPerPage = 20; // this should match however many results your API puts on one page

        $scope.currName = '';

        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };
        var target = angular.element('#addTypeModal');
        var confDelete = angular.element('#confDelete');
        //Toggle
        var userblocktoggle = angular.element('#userblocktoggle');
        var userAttendance = angular.element('#userAttendance');
        var userObjectives = angular.element('#userObjectives');
        var userRatings = angular.element('#userRatings');
        var userReports = angular.element('#userReports');
        var week = angular.element('#week');
        var weekAttendance = angular.element('#weekAttendance');
        var weekObjectives = angular.element('#weekObjectives');
        var weekRetings = angular.element('#weekRetings');
        var weekReports = angular.element('#weekReports');
        var sessions = angular.element('#sessions');
        var sessionsAttendance = angular.element('#sessionsAttendance');
        var sessionsObjectives = angular.element('#sessionsObjectives');
        var sessionsRetings = angular.element('#sessionsRetings');
        var sessionsReports = angular.element('#sessionsReports');

        //Toggle end
        $scope.okCurr = function () {
            $http.post('/api/CurriculumTypes',
                {
                    "name": $scope.currName,
                    "usesBlocks": userblocktoggle.prop('checked'),
                    "usesBlocksForAttendance": userAttendance.prop('checked'),
                    "usesBlocksForObjectives": userObjectives.prop('checked'),
                    "usesBlocksForRatings": userRatings.prop('checked'),
                    "usesBlocksForReports": userReports.prop('checked'),
                    "usesWeeks": week.prop('checked'),
                    "usesWeeksForAttendance": weekAttendance.prop('checked'),
                    "usesWeeksForObjectives": weekObjectives.prop('checked'),
                    "usesWeeksForRatings": weekRetings.prop('checked'),
                    "usesWeeksForReports": weekReports.prop('checked'),
                    "usesSessions": sessions.prop('checked'),
                    "usesSessionsForAttendance": sessionsAttendance.prop('checked'),
                    "usesSessionsForObjectives": sessionsObjectives.prop('checked'),
                    "usesSessionsForRatings": sessionsRetings.prop('checked'),
                    "usesSessionsForReports": sessionsReports.prop('checked')
                }
                ).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
            target.modal('hide');
        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            needToDelete = id;
        };
        $scope.delete = function () {
            $http.delete('/api/CurriculumTypes/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        };
        $scope.openEdit = function (id) {
            $http.get('/api/CurriculumTypes/' + id)
                .success(function (result) {
                    $scope.newCourse = result;
                    $scope.modalTitle = "Update Course";
                    target.modal('show');

                });
        };
    }]);

    module.controller('SkillsKnowledgeController', ['$scope', '$http', function ($scope, $http) {

        var needToDelete = -1;

        function getResultsPage(pageNumber) {
            $http.get('/api/SkillLevels/' + $scope.skillsPerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.skills = result.items;
                    $scope.totalSkills = result.count;
                });
        }

        $scope.skills = [];
        $scope.totalSkills = 0;
        $scope.skillsPerPage = 20; // this should match however many results your API puts on one page


        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };

        var target = angular.element('#addLevels');
        var confDelete = angular.element('#confDelete');

        $scope.okLvl = function (id) {
            if (id != null) {
                $http.put('/api/SkillLevels/' + id, $scope.newLevel).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');

                });
                target.modal('hide');


            } else {
                $http.post('/api/SkillLevels', $scope.newLevel).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
                target.modal('hide');

            }
            $scope.newLevel = null;
        };
        $scope.cancel = function () {
            $scope.newLevel = null;
            target.modal('hide');
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Level";
            target.modal('show');
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };
        $scope.delete = function () {
            $http.delete('/api/SkillLevels/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        };

        $scope.openEdit = function (id) {
            $http.get('/api/SkillLevels/' + id)
                .success(function (result) {
                    $scope.newLevel = result;
                    $scope.modalTitle = "Update Level";
                    target.modal('show');

                });
        };
    }]);

    module.controller('ScienceTestsController', ['$scope', '$http', function ($scope, $http) {

        $scope.testTypes = [
            { id: 0, name: 'Agility' },
            { id: 1, name: 'Fitness' },
            { id: 2, name: 'Strength' },
            { id: 3, name: 'Acceleration' },
            { id: 4, name: 'Pace' }
        ];

        $scope.selectedType = $scope.testTypes[0];

        var needToDelete = -1;

        function getResultsPage(pageNumber) {
            $http.get('/api/SportsScienceTests/' + $scope.testsPerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.sportTests = result.items;
                    $scope.totalTests = result.count;
                });
        }

        $scope.sportTests = [];
        $scope.totalTests = 0;
        $scope.testsPerPage = 20; // this should match however many results your API puts on one page


        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };
        var target = angular.element('#addTest');
        var confDelete = angular.element('#confDelete');

        $scope.okTest = function (id) {
            $scope.newTest.type = $scope.selectedType.id;
            if (id != null) {
                $http.put('/api/SportsScienceTests/' + id, $scope.newTest).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
            } else {
                $http.post('/api/SportsScienceTests', $scope.newTest).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });

            }
            target.modal('hide');
        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
            needToDelete = -1;
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Test";
            $scope.newTest = null;
            target.modal('show');
        };
        $scope.delete = function () {
            $http.delete('/api/SportsScienceTests/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        };
        $scope.openEdit = function (id) {
            $http.get('/api/SportsScienceTests/' + id)
                .success(function (result) {
                    $scope.newTest = result;
                    $scope.modalTitle = "Update Test";
                    target.modal('show');
                });
        };
    }]);

    module.controller('ScienceExercisesController', ['$scope', '$http', function ($scope, $http) {
        $scope.exerciseTypes = [
           { id: 0, name: 'Agility' },
           { id: 1, name: 'Fitness' },
           { id: 2, name: 'Strength' },
           { id: 3, name: 'Acceleration' },
           { id: 4, name: 'Pace' }
        ];

        $scope.selectedType = $scope.exerciseTypes[0];

        var needToDelete = -1;

        function getResultsPage(pageNumber) {
            $http.get('/api/SportsScienceExercises/' + $scope.exercisesPerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.sportExercises = result.items;
                    $scope.totalExercises = result.count;
                });
        }

        $scope.sportExercises = [];
        $scope.totalExercises = 0;
        $scope.exercisesPerPage = 20; // this should match however many results your API puts on one page


        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };
        var target = angular.element('#addExercise');
        var confDelete = angular.element('#confDelete');

        $scope.okEx = function (id) {
            $scope.newExercise.type = $scope.selectedType.id;
            if (id != null) {
                $http.put('/api/SportsScienceExercises/' + id, $scope.newExercise).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
            } else {

                $http.post('/api/SportsScienceExercises', $scope.newExercise).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
            }

            target.modal('hide');
        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
            needToDelete = -1;
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add an Exercise";
            $scope.newExercise = null;
            target.modal('show');
        };
        $scope.delete = function () {
            $http.delete('/api/SportsScienceExercises/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        };

        $scope.openEdit = function (id) {
            $http.get('/api/SportsScienceExercises/' + id)
                .success(function (result) {
                    $scope.newExercise = result;
                    $scope.modalTitle = "Update Exercise";
                    target.modal('show');
                });
        };
    }]);

    module.controller('LoginSettingsController', ['$scope', '$http', function ($scope, $http) {

        function getResultsPage(pageNumber) {
            $http.get('/api/PasswordHistory/' + $scope.historyPerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.passwordHistory = result.items;
                    $scope.totalHistory = result.count;
                });
        }

        $scope.passwordHistory = [];
        $scope.totalHistory = 0;
        $scope.historyPerPage = 20; // this should match however many results your API puts on one page


        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };
        var target = angular.element('#updateLogin');

        $scope.ok = function () {
            $http.post('/api/UpdatePassword', $scope.newPassword).success(function () {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
            });
            target.modal('hide');
        };
        $scope.cancel = function () {
            target.modal('hide');
        };

    }]);

    module.controller('TargetController', ['$scope', '$http', function ($scope, $http) {

        function getResultsPage(pageNumber) {
            $http.get('/api/TargetHistory/' + $scope.targetsPerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.targets = result.items;
                    $scope.totalTargets = result.count;
                });
        }

        $scope.targets = [];
        $scope.totalTargets = 0;
        $scope.targetsPerPage = 20; // this should match however many results your API puts on one page


        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };

        var target = angular.element('#updateTarget');

        $scope.okTarget = function () {
            $http.post('/api/TargetHistory', $scope.newPassword).success(function () {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
            });
            target.modal('hide');
        };
        $scope.cancel = function () {
            target.modal('hide');
        };

    }]);

    function pageTableData($scope, $http, urlTail, modalName) {

        var needToDelete = -1;

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

        var target = angular.element(modalName);
        var confDelete = angular.element('#confDelete');

        $scope.okTarget = function () {
            $http.post(urlTail, $scope.newItem).success(function () {
                getResultsPage($scope.pagination.current);
                target.modal('hide');
            });
            target.modal('hide');
        };
        $scope.cancel = function () {
            target.modal('hide');
        };
    };

    module.controller('NFTController', ['$scope', '$http', function ($scope, $http) {

        $scope.foodType = [
             { id: 0, name: 'Fruit' },
             { id: 1, name: 'Vegetables' },
             { id: 2, name: 'Meat' },
             { id: 3, name: 'Fish' },
             { id: 4, name: 'Diary' },
             { id: 5, name: 'Grain' },
             { id: 6, name: 'Nuts' }
        ];
        $scope.when = [
            { id: 0, name: 'Breakfast' },
            { id: 1, name: 'Before Match\Training' },
            { id: 2, name: 'After Match\Training' },
            { id: 3, name: 'Dinner' },
            { id: 4, name: 'Snack' }
        ];

        $scope.selectedType = $scope.foodType[0];
        $scope.selectedWhen = $scope.when[0];

        var urlTail = '/api/NutritionFoodTypes';

        var needToDelete = -1;

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

        var target = angular.element('#addFoodType');
        var confDelete = angular.element('#confDelete');

        $scope.ok = function (id) {

            $scope.type = $scope.selectedType.id;
            $scope.when = $scope.selectedWhen.id;
            $scope.newFood.picture = 'tmp.png';
            if (id != null) {
                $http.put(urlTail + '/' + id, $scope.newFood).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
            } else {

                $http.post(urlTail, $scope.newFood).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
            }
            target.modal('hide');
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Food Type";
            $scope.newFood = null;
            target.modal('show');
        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
        };
        $scope.delete = function () {
            $http.delete('/api/NutritionFoodTypes/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        }
        $scope.openEdit = function (id) {
            $http.get('/api/NutritionFoodTypes/' + id)
                .success(function (result) {
                    $scope.newFood = result;
                    $scope.modalTitle = "Update Food Type";
                    target.modal('show');
                });
        };
    }]);

    module.controller('NAController', ['$scope', '$http', function ($scope, $http) {

        var needToDelete = -1;
        var urlTail = '/api/NutritionAlternatives';

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

        var target = angular.element('#addAlternative');
        var confDelete = angular.element('#confDelete');

        $scope.ok = function (id) {
            $scope.newAlt.badItemPicture = 'tmp.png';
            $scope.newAlt.alternativePicture = 'tmp.png';
            if (id != null) {

                $http.put(urlTail + '/' + id, $scope.newAlt).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
            } else {

                $http.post(urlTail, $scope.newAlt).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
            }
            target.modal('hide');
        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add an Alternative";
            $scope.newAlt = null;
            target.modal('show');
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
                    $scope.newAlt = result;
                    $scope.modalTitle = "Update Alternative";
                    target.modal('show');
                });
        };
    }]);

    module.controller('NRController', ['$scope', '$http', function ($scope, $http) {
        var needToDelete = -1;
        var urlTail = '/api/NutritionRecipes';

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

        var target = angular.element('#addRecipe');
        var confDelete = angular.element('#confDelete');

        $scope.ok = function (id) {
            $scope.newRecipt.picture = 'tmp.png';
            if (id != null) {
                $http.put(urlTail + '/' + id, $scope.newRecipt).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });

            } else {
                $http.post(urlTail, $scope.newRecipt).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });

            }

            target.modal('hide');
        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };

        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Recipe";
            $scope.newRecipt = null;
            target.modal('show');
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
                    $scope.newRecipt = result;
                    $scope.modalTitle = "Update Recipe";
                    target.modal('show');
                });
        };
    }]);

    module.controller('PhysioExerciseController', ['$scope', '$http', function ($scope, $http) {
        var needToDelete = -1;

        function getResultsPage(pageNumber) {
            console.log($scope.coursePerPage);
            $http.get('/api/PhysioExercise/' + $scope.exercisePerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.exercises = result.items;
                    $scope.totalExercises = result.count;
                });
        }

        $scope.exercises = [];
        $scope.totalExercises = 0;
        $scope.exercisePerPage = 20; // this should match however many results your API puts on one page


        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };
        var target = angular.element('#addExerciseOrStretch');
        var confDelete = angular.element('#confDelete');

        $scope.ok = function (id) {
            if (id != null) {

                $http.put('/api/PhysioExercise/' + id, $scope.newExercise).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });

            } else {

                $http.post('/api/PhysioExercise', $scope.newExercise).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
            }
            target.modal('hide');
        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
            needToDelete = -1;
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add Exercise or Stretch";
            $scope.newExercise = null;
            target.modal('show');
        };
        $scope.delete = function () {
            $http.delete('/api/PhysioExercise/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        };
        $scope.openEdit = function (id) {
            $http.get('/api/PhysioExercise/' + id)
                .success(function (result) {
                    $scope.newExercise = result;
                    $scope.modalTitle = "Update Exercise or Stretch";
                    target.modal('show');

                });
        };


    }]);

    module.controller('ScenariosController', ['$scope', '$http', function ($scope, $http) {
        var needToDelete = -1;

        $scope.scenarioType = [
           { id: 0, name: 'Attacking' },
           { id: 1, name: 'Ball Control' },
           { id: 2, name: 'Defending' },
           { id: 3, name: 'Goalkeeping' },
           { id: 4, name: 'Midfield' },
           { id: 0, name: 'Play Scenario' },
           { id: 1, name: 'Set Plays' }
        ];

        $scope.selectedType = $scope.scenarioType[0];

        function getResultsPage(pageNumber) {
            $http.get('/api/Scenarios/' + $scope.scenariosPerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.scenarios = result.items;
                    $scope.totalScenarios = result.count;
                });
        }

        $scope.scenarios = [];
        $scope.totalScenarios = 0;
        $scope.scenariosPerPage = 20; // this should match however many results your API puts on one page


        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };
        var target = angular.element('#add2D');
        var confDelete = angular.element('#confDelete');

        $scope.ok = function (id) {
            $scope.newScenario.picture = 'tmp.png';
            $scope.newScenario.scenarioType = $scope.selectedType.id;
            if (id != null) {
                $http.put('/api/Scenarios/' + id, $scope.newScenario).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
                target.modal('hide');

            } else {
                $http.post('/api/Scenarios', $scope.newScenario).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
                target.modal('hide');

            }
        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
            needToDelete = -1;
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };
        $scope.openAdd2d = function () {
            $scope.modalTitle = "Add a 2D Scenario";
            $scope.newScenario = null;
            target.modal('show');
        };
        $scope.delete = function () {
            $http.delete('/api/Scenarios/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        };
        $scope.openEdit = function (id) {
            $http.get('/api/Scenarios/' + id)
                .success(function (result) {
                    $scope.newScenario = result;
                    $scope.selectedType = $scope.scenarioType[result.scenarioType];
                    $scope.modalTitle = "Update 2D Scenario";
                    target.modal('show');

                });
        };

        $scope.viewVideo = function (videoLink) {
            console.log('Here');
            console.log(videoLink);
            var videoHolder = angular.element('#videoPlase');
            videoHolder.append('<div id="videoPlase" class="col-lg-6"><div class="ibox float-e-margins">' +
                '<div class="ibox-title">' +
                '<h5>Video window</h5>' +
                '<div class="ibox-tools">' +
                '<a class="close-link">' +
                '<i class="fa fa-times"></i></a>' +
                '</div></div><div class="ibox-content">' +
                '<iframe width="560" height="315" src="' + videoLink + '" frameborder="0" allowfullscreen></iframe>' +
                '</figure></div></div></div>');
        }


    }]);

    module.controller('BodyPartController', ['$scope', '$http', function ($scope, $http) {
        var needToDelete = -1;
        var urlTail = '/api/PhysioBodyParts';

        $scope.parts = [
           { id: 0, name: 'Ankle' },
           { id: 1, name: 'Arm' },
           { id: 2, name: 'Back' },
           { id: 3, name: 'Chest' },
           { id: 4, name: 'Foot' },
           { id: 0, name: 'Head' },
           { id: 1, name: 'Hip' },
           { id: 2, name: 'Knee' },
           { id: 0, name: 'Leg' },
           { id: 1, name: 'Shoulder' },
           { id: 2, name: 'Other' }
        ];

        $scope.selectedBPart = $scope.parts[0];

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

        var target = angular.element('#addBodyPart');
        var confDelete = angular.element('#confDelete');

        $scope.ok = function (id) {
            if (id != null) {

                $scope.newPart.picture = 'tmp.png';
                $scope.newPart.type = $scope.selectedBPart.id;
                $http.put(urlTail + '/' + id, $scope.newPart).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
                target.modal('hide');

            } else {

                $scope.newPart.picture = 'tmp.png';
                $scope.newPart.type = $scope.selectedBPart.id;
                $http.post(urlTail, $scope.newPart).success(function () {
                    getResultsPage($scope.pagination.current);
                    target.modal('hide');
                });
                target.modal('hide');
            }

        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Body Part";
            $scope.newPart = null;
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
            $http.get('/api/PhysioBodyParts/' + id)
                .success(function (result) {
                    $scope.newPart = result;
                    $scope.modalTitle = "Update Body Part";
                    target.modal('show');

                });
        };
    }]);

})();