(function () {
    var module = angular.module('MainApp', ['ngRoute']);

    require('./controllers/DashBord').inject(module);
    require('./controllers/Clubs').inject(module);
    require('./controllers/Clients').inject(module);
})();