(function () {
    var module = angular.module('MainApp', ['tc.chartjs', 'angularUtils.directives.dirPagination']);

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
            data.forEach(function(val) {
                monthArray.push(monthNames[val.month-1]);
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
    module.controller('PlayersBoxController', ['$scope', '$http', function($scope, $http) {
        $http.get('api/dashboard/logged/players').success(function(data) {
            $scope.amountPlayers = data.amount;
            $scope.progressPlayer = data.progress;
            $scope.percentage = data.percentage;
        });
    }]);
    module.controller('CoachsBoxController', ['$scope', '$http', function($scope, $http) {
        $http.get('api/dashboard/logged/coaches').success(function(data) {
            $scope.amountCoaches = data.amount;
            $scope.progressCoach = data.progress;
            $scope.percentage = data.percentage;
        });
    }]);
    module.controller('ClubsBoxController', ['$scope', '$http', function($scope, $http) {
        $http.get('api/dashboard/logged/clubs').success(function(data) {
            $scope.amountClubs = data.amount;
            $scope.progressClub = data.progress;
            $scope.percentage = data.percentage;
        });
    }]);
    module.controller('UsersBoxController', ['$scope', '$http', function($scope, $http) {
        $http.get('api/dashboard/logged/users').success(function(data) {
            $scope.amountUsers = data.amount;
            $scope.progressUser = data.progress;
            $scope.percentage = data.percentage;
        });
    }]);
    module.controller('PlayerLoginHistoryController', ['$scope', '$http', function($scope, $http) {
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
    module.controller('AllPlayerController', ['$scope', '$http', function($scope, $http) {
        $http.get('api/dashboard/active/players/all').success(function (data) {
            $scope.playerCount = data;
        });
    }]);
    module.controller('FaCoursesController', ['$scope', '$http', function ($scope, $http) {

        $scope.users = [];
        $scope.totalUsers = 0;
        $scope.usersPerPage = 10; // this should match however many results your API puts on one page
        getResultsPage(1);

        $scope.pagination = {
            current: 1
        };

        $scope.pageChanged = function (newPage) {
            getResultsPage(newPage);
        };

        function getResultsPage(pageNumber) {
            $http.get('/api/FaCourses/' + $scope.usersPerPage + '/' + pageNumber)
                .success(function (result) {
                    $scope.users = result.items;
                    $scope.totalUsers = result.count;
            });
        }
    }]);


})();