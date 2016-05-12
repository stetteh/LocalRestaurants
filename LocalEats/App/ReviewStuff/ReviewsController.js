(function () {
    'use strict';

    angular
        .module('app')
        .controller('ReviewController', ReviewsController);

    ReviewsController.$inject = ['$scope', 'Reviewfactory'];

    function ReviewsController($scope) {
        /* jshint validthis:true */
        //var vm = this;
        $scope.title = 'ReviewController';

        activate();

        function activate() { }
    }
})();
