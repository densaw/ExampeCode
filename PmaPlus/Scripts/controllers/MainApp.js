(function () {
    var module = angular.module('MainApp', ['tc.chartjs', 'angularUtils.directives.dirPagination', 'ui.bootstrap', 'ngCookies', 'toaster', 'file-model', 'ngSanitize', 'ui.select', 'ui.bootstrap.datetimepicker', 'ui.calendar', 'ngRoute', 'ladda', 'mgo-angular-wizard', 'checklist-model', 'ui.bootstrap.modal']);

    module.run(['$rootScope', function($rootScope){
        $rootScope.revers = true;
        $rootScope.preOrderField = '';
    }]);

    module.directive('ratingUi', function () {
        return {

            restrict: 'A',

            link: function (scope, elem, attrs) {
                scope.$watch(attrs.ngModel, function () {
                    if (elem.html() >= 0 && elem.html() <= 5) {
                        elem.css('background', '#E60000');
                    }
                    else if (elem.html() >= 5, 1 && elem.html() <= 6) {
                        elem.css('background', '#ED7800');
                    }
                    else if (elem.html() >= 6, 1 && elem.html() <= 6, 5) {
                        elem.css('background', '#F4D600');
                    }
                    else if (elem.html() >= 6, 6 && elem.html() <= 7) {
                        elem.css('background', '#8AB40C');
                    }
                    else if (elem.html() >= 7, 1 && elem.html() <= 8) {
                        elem.css('background', '#617E08');
                    }
                    else {
                        elem.css('background', '#303F04');
                    }
                   
                    
                });
            }
        };
    });
    module.directive('ratingAi', function () {
        return {

            restrict: 'A',

            link: function (scope, elem, attrs) {
                scope.$watch(attrs.ngModel, function () {
                    if (parseFloat(elem.html()) >= 0 && parseFloat(elem.html()) <= 75) {
                    elem.css('background', '#E60000');
                }
                    else if (parseFloat(elem.html()) >= 76 && parseFloat(elem.html()) <= 80) {
                    elem.css('background', '#ED7800');
                }
                else if (parseFloat(elem.html()) >= 81 && parseFloat(elem.html()) <= 90) {
                    elem.css('background', '#F4D600');
                }
                else if (parseFloat(elem.html()) >= 91 && parseFloat(elem.html()) <= 93) {
                    elem.css('background', '#8AB40C');
                }
                else if (parseFloat(elem.html()) >= 94 && parseFloat(elem.html()) <= 97) {
                    elem.css('background', '#617E08');
                }
                else {
                    elem.css('background', '#303F04');
                }
                console.log(elem.html());
                console.log(elem);
                });
            }
        };
    });
    module.directive('ratingOi', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                element.on('click', function () {
                    if (ngModel.$modelValue === '0' || ngModel.$modelValue === '1' || ngModel.$modelValue === '2' || ngModel.$modelValue === '3' || ngModel.$modelValue === '4') {
                        element.css('background', '#ffb9b9');
                        console.log(ngModel.$modelValue);

                    }
                    else if (ngModel.$modelValue === '5' || ngModel.$modelValue === '6') {
                        element.css('background', '#feff9e');
                    }
                    else {
                        element.css('background', '#caffb9');
                    }
                });
            }
        };

    });
    //module.directive('ratingOi', function () {
    //    return {
    //       require: 'ngModel',
    //       link: function (scope, element, attrs, ngModel) {
    //           element.on('click', function () {
    //               if (ngModel.$modelValue >= 0 && ngModel.$modelValue <= 5) {
    //                   element.css('background', '#E60000');
    //               }
    //               else if (ngModel.$modelValue >= 5, 1 && ngModel.$modelValue <= 6) {
    //                   element.css('background', '#ED7800');
    //               }
    //               else if (ngModel.$modelValue >= 6, 1 && ngModel.$modelValue <= 6, 5) {
    //                   element.css('background', '#F4D600');
    //               }
    //               else if (ngModel.$modelValue >= 6, 6 && ngModel.$modelValue <= 7) {
    //                   element.css('background', '#8AB40C');
    //               }
    //               else if (ngModel.$modelValue >= 7, 1 && ngModel.$modelValue <= 8) {
    //                   element.css('background', '#617E08');
    //               }
    //               else {
    //                   element.css('background', '#303F04');
    //               }
    //           });
    //        }
    //    };

    //});

  

    module.directive('toggleCheckbox', function() {
 
        /**
         * Directive
         */
        return {
            restrict: 'A',
            transclude: true,
            replace: false,
            require: 'ngModel',
            link: function ($scope, $element, $attr, require) {
 
                var ngModel = require;
 
                // update model from Element
                var updateModelFromElement = function() {
                    // If modified

                    var checked = $element.prop('checked');
                    if (checked != ngModel.$viewValue) {
                        // Update ngModel
                        ngModel.$setViewValue(checked);
                        $scope.$apply();
                    }
                };
 
                // Update input from Model
                var updateElementFromModel = function() {
                    // Update button state to match model
                    $element.trigger('change');
                };
 
                // Observe: Element changes affect Model
                $element.on('change', function() {
                    updateModelFromElement();
                });
 
                // Observe: ngModel for changes
                $scope.$watch(function() {
                    return ngModel.$viewValue;
                }, function() {
                    updateElementFromModel();
                });
 
                // Initialise BootstrapToggle
                $element.bootstrapToggle({
                    on: 'Yes',
                    off: 'No'
                });
            }
        };
    });


    module.directive('backImg', function () {
        return function (scope, element, attrs) {
            var url = attrs.backImg;
            console.log(url);
            element.css({
                'background-image': 'url(' + url + ') no-repeat;',
                'background-size': 'cover;',
                'padding': '33px 25px;'
            });
        };
    });

    module.directive('ngEnter', function () {
        return function (scope, element, attrs) {
            element.bind("keydown keypress", function (event) {
                if(event.which === 13) {
                    scope.$apply(function (){
                        scope.$eval(attrs.ngEnter);
                    });

                    event.preventDefault();
                }
            });
        };
    });

    
    module.config(['uiSelectConfig', function (uiSelectConfig) {
        uiSelectConfig.theme = 'bootstrap';
    }]);

    module.factory('tableHttpOrderBy', ['$http', function($http){
        return {
            orderBy: function(url, orderField){
                var revers = false;
                return $http.get(url /*+ '/' + orderField + '/' + revers*/);
            }
        };
    }]);


    module.controller('orderFieldsController', ['$scope', '$filter', '$rootScope',  function($scope, $filter, $rootScope){
        $scope.order = function(orderFieldName){
            if($rootScope.preOrderField === orderFieldName){
                $rootScope.revers = !$rootScope.revers;
            }else{
                $rootScope.revers = false;
                $rootScope.preOrderField = orderFieldName;
            }
            $rootScope.orderField = orderFieldName;
            console.log($rootScope.revers);
        }
    }]);

    module.directive('orderHeader', ['tableHttpOrderBy', function(tableHttpOrderBy){
        return{
            restrict: 'A',
            scope: true,
            scope: {
              header: '@head',
              orderField: '@order'
            },
            template: '<p ng-click="order(orderField)" style="cursor: pointer;">{{header}}</p>'/* + '<i class="fa fa-cog"></i>'*/,
            controller: 'orderFieldsController'
        }
    }]);



    module.filter('curr', function () {
        return function (v, yes, no) {
            return v ? yes : no;
        };
    });

    module.filter('nav', function () {
        return function (v, yes, no) {
            return v ? yes : no;
        };
    });

    module.service('fileUpload', ['$http', function ($http) {
        this.uploadFileToUrl = function (file, uploadUrl) {
            var fd = new FormData();
            fd.append('file', file);
            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            })
            .success(function () {
                console.log('good');
            })
            .error(function (date) {
                console.log(date);
            });
        }
    }]);

    module.controller('MessageWallController', ['$scope', '$cookies', 'toaster', '$http', '$q', function ($scope, $cookies, toaster, $http, $q) {

        var sendMessageModal = angular.element('#addMessage');

        $scope.message = [];
        $scope.currentUser = {};
        $scope.tmpComment = [];
        $scope.newComment = {};
        $scope.newRating = {};
        $scope.newMessage = {};
        $scope.statmentsRev = [];

        $scope.currentPage = 0;
        $scope.isLastPage = false;

        function getCurrentUser(){
            $http.get('/api/Users/Avatar')
            .success(function(result){
                
                if (result.picture.length === 0) {
                    result.picture = '/api/Clubs/logo';
                };
                console.log(result);
                $scope.currentUser = result;
            });
        }

        function getResultsPage(pageNumber) {
            $http.get('/api/Message?page=' + pageNumber)
                .success(function (result) {
                    console.log(result);
                    angular.forEach(result.items, function(item){
                        this.push(item);
                    }, $scope.message);
                    $scope.isLastPage = $scope.currentPage == result.pages-1;
                });
        }

        $scope.rateMessage = function(message, rating){
            $scope.newRating.rating = rating; 
            $http.post('/api/Message/Rating/' + message.id, $scope.newRating)
            .success(function(result){
                if(result.rating){
                    message.ratingPositive.push(result);
                }else{
                    message.ratingNegative.push(result);
                }
            })
        }

        $scope.sendComment = function(message, index){
            console.log(message);
            $scope.newComment.comment =  $scope.tmpComment[index].comment
            $http.post('/api/Message/Comment/'+message.id, $scope.newComment)
                .success(function(result){
                    $scope.tmpComment = [];
                    message.comments.push(result);
                    console.log(result);
                    //getResultsPage($scope.currentPage);
                });
        }

        $scope.openMessage = function () {
            angular.element('.pma-fileupload').fileinput('clear');
            $scope.newMessage = {};
            $scope.modalTitle = 'Send new message';
            sendMessageModal.modal('show');
        }

        $scope.cancel = function(){
            sendMessageModal.modal('hide');
        }

        $scope.sendMessage = function(){

             //Files upload

                var promises = [];

                if ($scope.messagePic) {
                    var fd = new FormData();
                    fd.append('file', $scope.messagePic);
                    var promise = $http.post('/api/Files/Wall', fd, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    })
                        .success(function (data) {
                            $scope.newMessage.image = data.name;
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
                    $http.post('/api/Message', $scope.newMessage)
                    .success(function(result){
                        //getResultsPage($scope.currentPage);
                        $scope.message.unshift(result);
                        sendMessageModal.modal('hide');
                    })
                });



            
        }

        $scope.showComments = function(index){
            $scope.statmentsRev[index].comments = true;
            $scope.statmentsRev[index].like = false;
            $scope.statmentsRev[index].dislike = false;
        }

        $scope.showLike = function(index){
            $scope.statmentsRev[index].comments = false;
            $scope.statmentsRev[index].like = true;
            $scope.statmentsRev[index].dislike = false;
        }

        $scope.showDislike = function(index){
            $scope.statmentsRev[index].comments = false;
            $scope.statmentsRev[index].like = false;
            $scope.statmentsRev[index].dislike = true;
        }

        $scope.loadMore = function(){
            $scope.currentPage++;
            getResultsPage($scope.currentPage);
        }

        getCurrentUser();
        getResultsPage(0);
    }]);

    module.controller('MainController', ['$scope', '$cookies', 'toaster', '$http', function ($scope, $cookies, toaster, $http) {
        $scope.showTost = function () {
            toaster.pop({
                type: 'error',
                title: 'Title text',
                body: 'Body text',
                showCloseButton: true
            });
        }
        $scope.expanded = true;
        $scope.navExpand = function () {
            $scope.expanded = !$scope.expanded;
            $cookies.expanded = $scope.expanded;
        }

        $scope.userName = "";

        $http.get("/api/User/Name").success(function(result) {
            $scope.userName = result;
        });


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
        $http.get('/api/ActualTarget')
            .success(function (data) {
                $scope.targets = data;

            })
            .error(function () {
                $scope.targets.target = 2500;
                $scope.targets.value = 4.00;
            });

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
                        data: [$scope.targets.target, $scope.targets.target, $scope.targets.target, $scope.targets.target, $scope.targets.target, $scope.targets.target, $scope.targets.target, $scope.targets.target, $scope.targets.target, $scope.targets.target, $scope.targets.target, $scope.targets.target]
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

    module.controller('FaCoursesController', ['$scope', '$http', 'toaster', '$rootScope', function ($scope, $http, toaster, $rootScope) {
        var sortArray = [];

        var urlTail = '/api/FaCourses';

        var needToDelete = -1;

        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.coursePerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
                return urlTail + '/' + $scope.coursePerPage + '/' + pageNumber;
            }
        }

        function getResultsPage(pageNumber) {
            console.log(pageNumber);
            $http.get(createTail(pageNumber))
                .success(function (result) {
                    $scope.courses = result.items;
                    $scope.totalCourses = result.count;
                });
        }

         $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
            sortArray = newValue;
            $http.get(createTail($scope.pagination.current))
                .success(function (result) {
                    $scope.courses = result.items;
                    $scope.totalCourses = result.count;
                });
        });  

        $scope.courses = [];
        $scope.totalCourses = 0;
        $scope.coursePerPage = 20; // this should match however many results your API puts on one page
        $scope.newCourse = {};

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
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;

            if (id != null) {
                $scope.loginLoading = false;
                $http.put('/api/FaCourses/' + id, $scope.newCourse)
                    .success(function (data, status, headers, config) {
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
                $scope.loginLoading = false;
                $http.post('/api/FaCourses', $scope.newCourse)
                    .success(function (data, status, headers, config) {
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
            needToDelete = -1;

        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Course";
            $scope.myform.form_Submitted = false;
            $scope.newCourse = {};
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

    module.controller('ClubsController', ['$scope', '$http', '$q', 'toaster', '$rootScope', function ($scope, $http, $q, toaster, $rootScope) {

        var needToDelete = -1;
        var urlTail = '/api/Clubs';
        var sortArray = [];

        $scope.statuses = [
            { id: 0, name: 'Active' },
            { id: 1, name: 'Blocked' },
            { id: 2, name: 'Closed' }
        ];
        $scope.selectedStatus = $scope.statuses[0];

        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.clubsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
                return urlTail + '/' + $scope.clubsPerPage + '/' + pageNumber;
            }
        }

        function getResultsPage(pageNumber) {
            $http.get(pageNumber)
                .success(function (result) {
                    $scope.clubs = result.items;
                    $scope.totalClubs = result.count;
                });
        }

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
            sortArray = newValue;
            $http.get(createTail($scope.pagination.current))
                .success(function (result) {
                    $scope.clubs = result.items;
                    $scope.totalClubs = result.count;
                });
        });    

        $scope.clubs = [];
        $scope.totalClubs = 0;
        $scope.clubsPerPage = 20; // this should match however many results your API puts on one page
        $scope.newClub = {};
        $scope.oldLogo = false;
        $scope.oldBackground = false;

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
                                getResultsPage($scope.pagination.current);
                                target.modal('hide');
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
                                getResultsPage($scope.pagination.current);
                                target.modal('hide');
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
            $scope.modalTitle = "Add a Club";
            $scope.newClub = {};
            angular.element('.pma-fileupload').fileinput('clear');
            $scope.backgroundFile = null;
            $scope.logoFile = null;
            $scope.myform.form_Submitted = false;
            $scope.oldLogo = false;
            $scope.oldBackground = false;
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
                   angular.element('.pma-fileupload').fileinput('clear');
                   $scope.selectedStatus = $scope.statuses[result.status];
                   $scope.modalTitle = "Update Club";
                   $scope.newClub.logo === '' ? $scope.oldLogo = false : $scope.oldLogo = true;
                   $scope.newClub.background === '' ? $scope.oldBackground = false : $scope.oldBackground = true;
                   target.modal('show');
               });


        };

    }]);

    module.controller('CurriculumTypesController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {

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
        $scope.okCurr = function (id) {
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;
            if (id != null) {
                $scope.loginLoading = false;
                $http.put('/api/CurriculumTypes/' + id,
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
                $scope.loginLoading = false;
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
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            needToDelete = id;
        };
        $scope.openAdd = function () {
            $scope.currName = "";
            $scope.modalTitle = "Add a Type";
            userblocktoggle.bootstrapToggle('on');
            userAttendance.bootstrapToggle('on');
            userObjectives.bootstrapToggle('on');
            userRatings.bootstrapToggle('on');
            userReports.bootstrapToggle('on');
            week.bootstrapToggle('on');
            weekAttendance.bootstrapToggle('on');
            weekObjectives.bootstrapToggle('on');
            weekRetings.bootstrapToggle('on');
            weekReports.bootstrapToggle('on');
            sessions.bootstrapToggle('on');
            sessionsAttendance.bootstrapToggle('on');
            sessionsObjectives.bootstrapToggle('on');
            sessionsRetings.bootstrapToggle('on');
            sessionsReports.bootstrapToggle('on');
            target.modal('show');
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
                    console.log(result);
                    $scope.currId = result.id;
                    $scope.currName = result.name;
                    userblocktoggle.bootstrapToggle(result.usesBlocks ? 'on' : 'off');
                    userAttendance.bootstrapToggle(result.usesBlocksForAttendance ? 'on' : 'off');
                    userObjectives.bootstrapToggle(result.usesBlocksForObjectives ? 'on' : 'off');
                    userRatings.bootstrapToggle(result.usesBlocksForRatings ? 'on' : 'off');
                    userReports.bootstrapToggle(result.usesBlocksForReports ? 'on' : 'off');
                    week.bootstrapToggle(result.usesWeeks ? 'on' : 'off');
                    weekAttendance.bootstrapToggle(result.usesWeeksForAttendance ? 'on' : 'off');
                    weekObjectives.bootstrapToggle(result.usesWeeksForObjectives ? 'on' : 'off');
                    weekRetings.bootstrapToggle(result.usesWeeksForRatings ? 'on' : 'off');
                    weekReports.bootstrapToggle(result.usesWeeksForReports ? 'on' : 'off');
                    sessions.bootstrapToggle(result.usesSessions ? 'on' : 'off');
                    sessionsAttendance.bootstrapToggle(result.usesSessionsForAttendance ? 'on' : 'off');
                    sessionsObjectives.bootstrapToggle(result.usesSessionsForObjectives ? 'on' : 'off');
                    sessionsRetings.bootstrapToggle(result.usesSessionsForRatings ? 'on' : 'off');
                    sessionsReports.bootstrapToggle(result.usesSessionsForReports ? 'on' : 'off');

                    $scope.modalTitle = "Update Type";
                    target.modal('show');

                });
        };
    }]);

    module.controller('SkillsKnowledgeController', ['$scope', '$http', 'toaster', '$location', '$rootScope', function ($scope, $http, toaster, $location, $rootScope) {

        
        var needToDelete = -1;
        var urlTail = '/api/SkillLevels';
        var sortArray = [];


        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.skillsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
                return urlTail + '/' + $scope.skillsPerPage + '/' + pageNumber;
            }
        }

        function getResultsPage(pageNumber) {
            $http.get(createTail(pageNumber))
                .success(function (result) {
                    $scope.skills = result.items;
                    $scope.totalSkills = result.count;
                });
        }

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
            sortArray = newValue;
            $http.get(createTail($scope.pagination.current))
                .success(function (result) {
                    $scope.skills = result.items;
                    $scope.totalSkills = result.count;
                });
        });    

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
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;

            if (id != null) {
                $scope.loginLoading = false;
                $http.put('/api/SkillLevels/' + id, $scope.newLevel).success(function () {
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
                $scope.loginLoading = false;
                $http.post('/api/SkillLevels', $scope.newLevel).success(function () {
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
            $scope.modalTitle = "Add a Level";
            $scope.myform.form_Submitted = false;
            $scope.newLevel = {};
            target.modal('show');
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
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

    module.controller('ScienceTestsController', ['$scope', '$http', 'toaster', '$rootScope', function ($scope, $http, toaster, $rootScope) {

        var sortArray = [];
        var urlTail = '/api/SportsScienceTests';

        $scope.testTypes = [
            { id: 0, name: 'Agility' },
            { id: 1, name: 'Fitness' },
            { id: 2, name: 'Strength' },
            { id: 3, name: 'Acceleration' },
            { id: 4, name: 'Pace' }
        ];
        $scope.zFormulaTypes = [
            { id: 0, name: 'HighLow' },
            { id: 1, name: 'LowHigh' }

        ];
        $scope.selectedFormula = $scope.zFormulaTypes[0];
        $scope.selectedType = $scope.testTypes[0];

        var needToDelete = -1;

        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.testsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
                return urlTail + '/' + $scope.testsPerPage + '/' + pageNumber;
            }
        }

        function getResultsPage(pageNumber) {
            $http.get(createTail(pageNumber))
                .success(function (result) {
                    $scope.sportTests = result.items;
                    $scope.totalTests = result.count;
                });
        }

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
            sortArray = newValue;
            $http.get(createTail($scope.pagination.current))
                .success(function (result) {
                    $scope.sportTests = result.items;
                    $scope.totalTests = result.count;
                });
        });    

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
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;

            $scope.newTest.type = $scope.selectedType.id;
            $scope.newTest.zScoreFormula = $scope.selectedFormula.id;
            if (id != null) {
                
                $http.put('/api/SportsScienceTests/' + id, $scope.newTest).success(function () {
                    getResultsPage($scope.pagination.current);
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
            } else {
               
                $http.post('/api/SportsScienceTests', $scope.newTest).success(function () {
                    getResultsPage($scope.pagination.current);
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

        };

        var modalVideo = angular.element('#videoModal');
        $scope.modalVideoStart = function (src) {
            console.log(src);
            //var src = 'http://www.youtube.com/v/Qmh9qErJ5-Q&amp;autoplay=1';
            modalVideo.modal('show');
            $('#videoModal iframe').attr('src', src);
        }

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
            $scope.newTest = {};
            $scope.myform.form_Submitted = false;
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
                    $scope.selectedType = $scope.testTypes[result.type];
                    $scope.selectedFormula = $scope.zFormulaTypes[result.zScoreFormula];
                    $scope.modalTitle = "Update Test";
                    target.modal('show');
                });
        };
    }]);

    module.controller('ScienceExercisesController', ['$scope', '$http', 'toaster', '$q', '$rootScope', function ($scope, $http, toaster, $q, $rootScope) {
        
        var sortArray = [];
        var urlTail = '/api/SportsScienceExercises';

        $scope.exerciseTypes = [
           { id: 0, name: 'Mobility' },
           { id: 1, name: 'Movement' },
           { id: 2, name: 'Stability' }
        ];

        $scope.selectedType = $scope.exerciseTypes[0];

        var needToDelete = -1;

        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.exercisesPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
                return urlTail + '/' + $scope.exercisesPerPage + '/' + pageNumber;
            }
        }

        function getResultsPage(pageNumber) {
            $http.get(createTail(pageNumber))
                .success(function (result) {
                    $scope.sportExercises = result.items;
                    $scope.totalExercises = result.count;
                });
        }

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
            sortArray = newValue;
            $http.get(createTail($scope.pagination.current))
                .success(function (result) {
                    $scope.sportExercises = result.items;
                    $scope.totalExercises = result.count;
                });
        });  

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
        var picModal = angular.element('#photoModal');
        var modalVideo = angular.element('#videoModal');

        $scope.modalVideoStart = function (src) {
            console.log(src);
            //var src = 'http://www.youtube.com/v/Qmh9qErJ5-Q&amp;autoplay=1';
            modalVideo.modal('show');
            $('#videoModal iframe').attr('src', src);
        }


        $scope.okEx = function (id) {
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;


            //---
            //Files upload

            var promises = [];

            if ($scope.pic1/*File model name*/) {
                var fd = new FormData();
                fd.append('file', $scope.pic1);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newExercise.picture1 = data.name;
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

            if ($scope.pic2/*File model name*/) {
                var fd = new FormData();
                fd.append('file', $scope.pic2);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newExercise.picture2 = data.name;
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

            if ($scope.pic3) {
                var fd = new FormData();
                fd.append('file', $scope.pic3);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newExercise.picture3 = data.name;
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
                $scope.loginLoading = false;
                $scope.newExercise.type = $scope.selectedType.id;
                if (id != null) {
                    $http.put('/api/SportsScienceExercises/' + id, $scope.newExercise).success(function () {
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
                    $scope.loginLoading = false;
                    $http.post('/api/SportsScienceExercises', $scope.newExercise).success(function () {
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
            });
            //--

            
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
            $scope.newExercise = {};
            $scope.myform.form_Submitted = false;
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
                    $scope.selectedType = $scope.exerciseTypes[result.type];
                    $scope.modalTitle = "Update Exercise";
                    target.modal('show');
                });
        };
        $scope.openPic = function (id) {
            $http.get('/api/SportsScienceExercises/' + id)
                .success(function (result) {
                    $scope.newExercise = result;
                    picModal.modal('show');
                });
        };

    }]);

    module.controller('LoginSettingsController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {

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
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;

            $http.post('/api/UpdatePassword', $scope.newPassword).success(function () {
                $scope.loginLoading = false;
                getResultsPage($scope.pagination.current);
                target.modal('hide');
            }).error(function (data, status, headers, config) {
                $scope.loginLoading = false;
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
        $scope.cancel = function () {
            target.modal('hide');
        };
        $scope.openAdd = function () {
            $scope.newPassword = {};
            $scope.myform.form_Submitted = false;
            target.modal('show');
        };


    }]);

    module.controller('TargetController', ['$scope', '$http', 'toaster', function ($scope, $http, toaster) {

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
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;

            $http.post('/api/TargetHistory', $scope.newTarget).success(function () {
                $scope.loginLoading = false;
                getResultsPage($scope.pagination.current);
                target.modal('hide');
            }).error(function (data, status, headers, config) {
                $scope.loginLoading = false;
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
        $scope.cancel = function () {
            target.modal('hide');
        };
        $scope.openAdd = function () {
            $scope.newTarget = {};
            $scope.myform.form_Submitted = false;
            target.modal('show');
        };
    }]);

    module.controller('NFTController', ['$scope', '$http', 'toaster', '$q', '$rootScope', function ($scope, $http, toaster, $q, $rootScope) {


        var sortArray = [];

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

        $scope.multipleDemo = {};
        $scope.multipleDemo.selectedWhen = [];

        $scope.selectedType = $scope.foodType[0];
        $scope.selectedWhen = [$scope.when[2]];
        $scope.$watch('selectedType', function (newVal) {
            console.log(newVal);
        });
        var urlTail = '/api/NutritionFoodTypes';

        var needToDelete = -1;

        function createTail(pageNumber){
                if(sortArray.length > 0){
                    return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
                }else{
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

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
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

        $scope.check = function (item) {
            item.selected = !item.selected;
        }

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
        var picModal = angular.element('#photoModal');

        $scope.ok = function (id) {
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;

            //---
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
                        $scope.newFood.picture = data.name;
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
                $scope.loginLoading = false;
                $scope.newFood.whens = [];
                angular.forEach($scope.multipleDemo.selectedWhen, function(wh) {
                    this.push(wh.id);
                }, $scope.newFood.whens);
                $scope.newFood.type = $scope.selectedType.id;
                if (id != null) {
                    $http.put(urlTail + '/' + id, $scope.newFood).success(function () {
                        getResultsPage($scope.pagination.current);
                        target.modal('hide');
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
                    });
                } else {
                    $scope.loginLoading = false;
                    $http.post(urlTail, $scope.newFood).success(function () {
                        getResultsPage($scope.pagination.current);
                        target.modal('hide');
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
                    });
                }
            });
            //--

            
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            console.log(id);
            needToDelete = id;
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Food Type";
            $scope.newFood = {};
            angular.element('.pma-fileupload').fileinput('clear');
            $scope.multipleDemo.selectedWhen = [];
            $scope.myform.form_Submitted = false;
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
                    $scope.multipleDemo.selectedWhen = [];
                    angular.forEach($scope.newFood.whens, function(index) {
                        this.push($scope.when[index]);
                    }, $scope.multipleDemo.selectedWhen);
                    $scope.selectedType = $scope.foodType[result.type];
                    $scope.selectedWhen = $scope.when[result.when];
                    $scope.modalTitle = "Update Food Type";
                    target.modal('show');
                });
        };
        $scope.openPic = function(id) {
            $http.get('/api/NutritionFoodTypes/' + id)
               .success(function (result) {
                   $scope.newFood = result;
                   picModal.modal('show');
               });
        }
    }]);

    module.controller('NAController', ['$scope', '$http', 'toaster', '$q', '$rootScope', function ($scope, $http, toaster, $q, $rootScope) {

        var needToDelete = -1;
        var urlTail = '/api/NutritionAlternatives';
        var sortArray = [];

        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
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

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
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

        var target = angular.element('#addAlternative');
        var confDelete = angular.element('#confDelete');
        var picModal = angular.element('#photoModal');
        var modalVideo = angular.element('#videoModal');

        $scope.modalVideoStart = function (src) {
            console.log(src);
            //var src = 'http://www.youtube.com/v/Qmh9qErJ5-Q&amp;autoplay=1';
            modalVideo.modal('show');
            $('#videoModal iframe').attr('src', src);
        }

        $scope.ok = function (id) {
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;

            //---
            //Files upload

            var promises = [];

            if ($scope.badPic/*File model name*/) {
                var fd = new FormData();
                fd.append('file', $scope.badPic);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newAlt.badItemPicture = data.name;
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

            if ($scope.altPic) {
                var fd = new FormData();
                fd.append('file', $scope.altPic);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newAlt.alternativePicture = data.name;
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
                $scope.loginLoading = false;
                if (id != null) {
                    $http.put(urlTail + '/' + id, $scope.newAlt).success(function () {
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
                    $scope.loginLoading = false;
                    $http.post(urlTail, $scope.newAlt).success(function () {
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
               });
            //--

           
           
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
            $scope.newAlt = {};
            $scope.myform.form_Submitted = false;
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
        $scope.openPic = function (id) {
            $http.get(urlTail + '/' + id)
               .success(function (result) {
                   $scope.newAlt = result;
                   picModal.modal('show');
               });
        }

    }]);

    module.controller('NRController', ['$scope', '$http', 'toaster', '$q', '$rootScope', function ($scope, $http, toaster, $q, $rootScope) {
        var needToDelete = -1;
        var urlTail = '/api/NutritionRecipes';
        var sortArray = [];

        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
                return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber;
            }
        }

        function getResultsPage(pageNumber) {
            $http.get(urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.items = result.items;
                    $scope.totalItems = result.count;
                });
        }

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
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

        $scope.check = function (item) {
            item.selected = !item.selected;
        }

        var target = angular.element('#addRecipe');
        var confDelete = angular.element('#confDelete');
        var picModal = angular.element('#photoModal');

        $scope.ok = function (id) {
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;

            //---
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
                        $scope.newRecipt.picture = data.name;
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
                if (id != null) {
                    $scope.loginLoading = false;
                    $http.put(urlTail + '/' + id, $scope.newRecipt).success(function () {
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
                    $scope.loginLoading = false;
                    $http.post(urlTail, $scope.newRecipt).success(function () {
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
            });
            //--
           
        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
        };
        $scope.openDelete = function (id) {
            confDelete.modal('show');
            needToDelete = id;
        };

        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Recipe";
            $scope.newRecipt = {};
            $scope.myform.form_Submitted = false;
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
        $scope.openPic = function(id) {
            $http.get(urlTail + '/' + id)
                .success(function(result) {
                    $scope.newRecipt = result;
                    picModal.modal('show');
                });
        };
    }]);

    module.controller('PhysioExerciseController', ['$scope', '$http', 'toaster', '$q', '$rootScope', function ($scope, $http, toaster, $q, $rootScope) {

        var urlTail = '/api/PhysioExercise';
        var sortArray = [];

        $scope.exType = [
            { id: 0, name: 'Exercise' },
            { id: 1, name: 'Stretch' }
        ];

        $scope.selectedType = $scope.exType[0];

        var needToDelete = -1;

        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.exercisePerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
                return urlTail + '/' + $scope.exercisePerPage + '/' + pageNumber;
            }
        }

        function getResultsPage(pageNumber) {
            console.log($scope.exercisePerPage);
            $http.get(createTail(pageNumber))
                .success(function (result) {
                    $scope.exercises = result.items;
                    $scope.totalExercises = result.count;
                });
        }

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
            sortArray = newValue;
            $http.get(createTail($scope.pagination.current))
                .success(function (result) {
                    $scope.exercises = result.items;
                    $scope.totalExercises = result.count;
                });
        });    

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
        var modalVideo = angular.element('#videoModal');
        var picModal = angular.element('#photoModal');

        $scope.modalVideoStart = function (src) {
            console.log(src);
            //var src = 'http://www.youtube.com/v/Qmh9qErJ5-Q&amp;autoplay=1';
            modalVideo.modal('show');
            $('#videoModal iframe').attr('src', src);
        }

        $scope.ok = function (id) {
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;


            //---
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
                        $scope.newExercise.picture = data.name;
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
                $scope.loginLoading = false;
                if (id != null) {

                    $http.put('/api/PhysioExercise/' + id, $scope.newExercise).success(function () {
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
                    $scope.loginLoading = false;
                    $scope.newExercise.type = $scope.selectedType.id;
                    $scope.newExercise.picture = 'tmp.png';
                    console.log($scope.newExercise);
                    $http.post('/api/PhysioExercise', $scope.newExercise).success(function () {
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
            });
            //--

            
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
            $scope.newExercise = {};
            $scope.myform.form_Submitted = false;
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
        $scope.openPic = function (id) {
            $http.get('/api/PhysioExercise/' + id)
                .success(function (result) {
                    $scope.newExercise = result;
                    picModal.modal('show');
                });
        };

    }]);

    module.controller('ScenariosController', ['$scope', '$http', 'toaster', '$q', '$rootScope', function ($scope, $http, toaster, $q, $rootScope) {
        var needToDelete = -1;
        var urlTail = '/api/Scenarios';
        var sortArray = [];

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

        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.scenariosPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
                return urlTail + '/' + $scope.scenariosPerPage + '/' + pageNumber;
            }
        }

        function getResultsPage(pageNumber) {
            $http.get(createTail(pageNumber))
                .success(function (result) {
                    $scope.scenarios = result.items;
                    $scope.totalScenarios = result.count;
                });
        }

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
            sortArray = newValue;
            $http.get(createTail($scope.pagination.current))
                .success(function (result) {
                    $scope.scenarios = result.items;
                    $scope.totalScenarios = result.count;
                });
        });   

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
        var picModal = angular.element('#photoModal');
        var modalVideo = angular.element('#videoModal');

        $scope.modalVideoStart = function (src) {
            console.log(src);
            //var src = 'http://www.youtube.com/v/Qmh9qErJ5-Q&amp;autoplay=1';
            modalVideo.modal('show');
            $('#videoModal iframe').attr('src', src);
        }


        $scope.ok = function (id) {
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;


            //---
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
                        $scope.newScenario.picture = data.name;
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
                $scope.loginLoading = false;
                $scope.newScenario.scenarioType = $scope.selectedType.id;
                if (id != null) {
                    $http.put('/api/Scenarios/' + id, $scope.newScenario).success(function () {
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
                    $scope.loginLoading = false;
                    $http.post('/api/Scenarios', $scope.newScenario).success(function () {
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
            });
            //--
           
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
            angular.element('.pma-fileupload').fileinput('clear');
            $scope.modalTitle = "Add a 2D Scenario";
            $scope.newScenario = {};
            $scope.selectedType = $scope.scenarioType[0];
            $scope.myform.form_Submitted = false;
            $scope.newScenario.minAge = 6;
            $scope.newScenario.maxAge = 18;
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
        $scope.openPic = function (id) {
            $http.get('/api/Scenarios/' + id)
                .success(function (result) {
                    $scope.newScenario = result;
                    picModal.modal('show');
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

    module.controller('BodyPartController', ['$scope', '$http', 'toaster', '$q', '$rootScope', function ($scope, $http, toaster, $q, $rootScope) {
        var needToDelete = -1;
        var urlTail = '/api/PhysioBodyParts';
        var sortArray = [];

        $scope.parts = [
           { id: 0, name: 'Ankle' },
           { id: 1, name: 'Arm' },
           { id: 2, name: 'Back' },
           { id: 3, name: 'Chest' },
           { id: 4, name: 'Foot' },
           { id: 5, name: 'Head' },
           { id: 6, name: 'Hip' },
           { id: 7, name: 'Knee' },
           { id: 8, name: 'Leg' },
           { id: 9, name: 'Shoulder' },
           { id: 10, name: 'Other' }
        ];

        $scope.selectedBPart = $scope.parts[0];

        function createTail(pageNumber){
                if(sortArray.length > 0){
                    return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
                }else{
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

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
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

        var target = angular.element('#addBodyPart');
        var confDelete = angular.element('#confDelete');
        var picModal = angular.element('#photoModal');

        $scope.ok = function (id) {
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;


            //---
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
                        $scope.newPart.picture = data.name;
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
                if (id != null) {
                    $scope.loginLoading = false;
                    $scope.newPart.type = $scope.selectedBPart.id;
                    $http.put(urlTail + '/' + id, $scope.newPart).success(function () {
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
                    $scope.loginLoading = false;
                    $scope.newPart.picture = 'tmp.png';
                    $scope.newPart.type = $scope.selectedBPart.id;
                    $http.post(urlTail, $scope.newPart).success(function () {
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
            });
            //--

            

        };
        $scope.cancel = function () {
            target.modal('hide');
            confDelete.modal('hide');
        };
        $scope.openAdd = function () {
            $scope.modalTitle = "Add a Body Part";
            $scope.newPart = {};
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
            $http.get('/api/PhysioBodyParts/' + id)
                .success(function (result) {
                    $scope.newPart = result;
                    $scope.selectedBPart = $scope.parts[result.type];
                    $scope.modalTitle = "Update Body Part";
                    target.modal('show');

                });
        };
        $scope.openPic = function (id) {
            $http.get(urlTail + '/' + id)
                .success(function (result) {
                    $scope.newPart = result;
                    picModal.modal('show');
                });
        };
    }]);

    module.controller('SkillLevelsController', ['$scope', '$http', 'toaster', '$location', '$rootScope', function ($scope, $http, toaster, $location, $rootScope) {
        
        $scope.modalTitle = "Add a Skill";
        var pathArray = $location.$$absUrl.split("/");
        $scope.ids = pathArray[pathArray.length - 1];

        var needToDelete = -1;
        var urlTail = '/api/SkillVideos';
        var sortArray = [];

        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
                return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber;
            }
        }

        function getResultsPage(pageNumber) {
            $http.get(urlTail + '/' + $scope.ids + '/' + $scope.itemsPerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.items = result.items;
                    $scope.totalItems = result.count;
                });
        }
        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
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
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;
            if (id != null) {
                $scope.loginLoading = false;
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
                $scope.loginLoading = false;
                console.log($scope.newSkill);
                $http.post(urlTail + '/' + $scope.ids, $scope.newSkill).success(function () {
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

    //News Controllers

    module.controller('ExerciseNewsController', ['$scope', '$http', 'toaster', '$q', '$rootScope', function ($scope, $http, toaster, $q, $rootScope) {
       
        var urlTail = '/api/ExerciseNews';
        var needToDelete = -1;
        var sortArray = [];

        function createTail(pageNumber){
                if(sortArray.length > 0){
                    return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
                }else{
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

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
            sortArray = newValue;
            $http.get(createTail($scope.pagination.current))
                .success(function (result) {
                    $scope.items = result.items;
                    $scope.totalItems = result.count;
                });
        });   

        $scope.items = [];
        $scope.totalItems = 0;
        $scope.itemsPerPage = 20; // this should match however many results your API puts on one page

        $scope.newNews = {};

        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };
        var target = angular.element('#addNews');
        var confDelete = angular.element('#confDelete');
        var picModal = angular.element('#photoModal');



        $scope.send = function (id) {
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;


            //---
            //Files upload

            var promises = [];

            if ($scope.authorPicture/*File model name*/) {
                $scope.loginLoading = false;
                var fd = new FormData();
                fd.append('file', $scope.authorPicture);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newNews.authorPicture = data.name;
                    })
                    .error(function () {
                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            body: 'File upload ERROR!'
                        });
                        $scope.loginLoading = false;
                    });
                promises.push(promise);
            }

            if ($scope.mainPicture/*File model name*/) {
                var fd = new FormData();
                fd.append('file', $scope.mainPicture);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newNews.mainPicture = data.name;
                    })
                    .error(function () {
                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            body: 'File upload ERROR!'
                        });
                        $scope.loginLoading = false;
                    });
                promises.push(promise);
            }

            if ($scope.sponsoredBy) {
                var fd = new FormData();
                fd.append('file', $scope.sponsoredBy);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newNews.sponsoredBy = data.name;
                    })
                    .error(function () {
                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            body: 'File upload ERROR!'
                        });
                        $scope.loginLoading = false;
                    });
                promises.push(promise);
            }

            if ($scope.picture) {
                var fd = new FormData();
                fd.append('file', $scope.picture);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newNews.picture = data.name;
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
               
                if (id != null) {
                    $http.put('/api/ExerciseNews/' + id, $scope.newNews).success(function () {
                        getResultsPage($scope.pagination.current);
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
                } else {
                    
                    $http.post('/api/ExerciseNews', $scope.newNews).success(function () {
                        getResultsPage($scope.pagination.current);
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
            $scope.modalTitle = "Add an Exercise News";
            $scope.newNews = {};
            $scope.myform.form_Submitted = false;
            target.modal('show');
        };
        $scope.delete = function () {
            $http.delete('/api/ExerciseNews/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        };

        $scope.openEdit = function (id) {
            $http.get('/api/ExerciseNews/' + id)
                .success(function (result) {
                    $scope.newNews = result;
                    $scope.myform.form_Submitted = false;
                    $scope.modalTitle = "Update Exercise News";
                    target.modal('show');
                });
        };
        $scope.openPic = function (id) {
            $http.get('/api/ExerciseNews/' + id)
                .success(function (result) {
                    $scope.newNews = result;
                    picModal.modal('show');
                });
        };

    }]);


    module.controller('NutritionNewsController', ['$scope', '$http', 'toaster', '$q', '$rootScope', function ($scope, $http, toaster, $q, $rootScope) {

        var sortArray = [];
        var needToDelete = -1;
        var urlTail = '/api/NutritionNews';

        function createTail(pageNumber){
            if(sortArray.length > 0){
                return urlTail + '/' + $scope.itemsPerPage + '/' + pageNumber + '/' + sortArray[0] + '/' + sortArray[1];
            }else{
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

        $rootScope.$watchGroup(['orderField', 'revers'], function(newValue, oldValue, scope) {
        sortArray = newValue;
        $http.get(createTail($scope.pagination.current))
            .success(function (result) {
                $scope.items = result.items;
                $scope.totalItems = result.count;
            });
    });   

        $scope.items = [];
        $scope.totalItems = 0;
        $scope.itemsPerPage = 20; // this should match however many results your API puts on one page


        $scope.pagination = {
            current: 1
        };
        getResultsPage($scope.pagination.current);
        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
            $scope.pagination.current = newPage;
        };
        var target = angular.element('#addNews');
        var confDelete = angular.element('#confDelete');
        var picModal = angular.element('#photoModal');



        $scope.send = function (id) {
            $scope.loginLoading = true;
            $scope.myform.form_Submitted = !$scope.myform.$valid;


            //---
            //Files upload

            var promises = [];

            if ($scope.authorPicture/*File model name*/) {
                $scope.loginLoading = false;
                var fd = new FormData();
                fd.append('file', $scope.authorPicture);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newNews.authorPicture = data.name;
                    })
                    .error(function () {
                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            body: 'File upload ERROR!'
                        });
                        $scope.loginLoading = false;
                    });
                promises.push(promise);
            }

            if ($scope.mainPicture/*File model name*/) {
                var fd = new FormData();
                fd.append('file', $scope.mainPicture);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newNews.mainPicture = data.name;
                    })
                    .error(function () {
                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            body: 'File upload ERROR!'
                        });
                        $scope.loginLoading = false;
                    });
                promises.push(promise);
            }

            if ($scope.sponsoredBy) {
                var fd = new FormData();
                fd.append('file', $scope.sponsoredBy);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newNews.sponsoredBy = data.name;
                    })
                    .error(function () {
                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            body: 'File upload ERROR!'
                        });
                        $scope.loginLoading = false;
                    });
                promises.push(promise);
            }

            if ($scope.picture) {
                var fd = new FormData();
                fd.append('file', $scope.picture);
                var promise = $http.post('/api/Files', fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                    .success(function (data) {
                        $scope.newNews.picture = data.name;
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

                if (id != null) {
                    $http.put('/api/NutritionNews/' + id, $scope.newNews).success(function () {
                        getResultsPage($scope.pagination.current);
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
                } else {

                    $http.post('/api/NutritionNews', $scope.newNews).success(function () {
                        getResultsPage($scope.pagination.current);
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
            $scope.modalTitle = "Add an Exercise News";
            $scope.newNews = {};
            $scope.myform.form_Submitted = false;
            target.modal('show');
        };
        $scope.delete = function () {
            $http.delete('/api/NutritionNews/' + needToDelete).success(function () {
                getResultsPage($scope.pagination.current);
                needToDelete = -1;
                confDelete.modal('hide');
            });
        };

        $scope.openEdit = function (id) {
            $http.get('/api/NutritionNews/' + id)
                .success(function (result) {
                    $scope.newNews = result;
                    $scope.myform.form_Submitted = false;
                    $scope.modalTitle = "Update Exercise News";
                    target.modal('show');
                });
        };
        $scope.openPic = function (id) {
            $http.get('/api/NutritionNews/' + id)
                .success(function (result) {
                    $scope.newNews = result;
                    picModal.modal('show');
                });
        };

    }]);

   
})();