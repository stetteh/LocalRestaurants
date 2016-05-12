(function () {
    'use strict';

    angular
        .module('app')
        .controller('ReviewController', ReviewsController);

    ReviewsController.$inject = ['$scope', 'Reviewfactory'];

    function ReviewsController($scope, Reviewfactory) {
        /* jshint validthis:true */
        //var vm = this;
        $scope.title = 'ReviewController';

        activate();

        $scope.saveReview = function() {
            console.log('called controller saveReview()');
            var newReview = {
                Text: $scope.newReview.Text,
                Date: $scope.newReview.Date
            };
            Reviewfactory.saveReview(newReview).then(function(res) {
                $scope.newReview.Text = "";
                $scope.newReview.Date = "";

                $scope.Reviews.push(res.data);
            });
        }

        function activate() {
            Reviewfactory.getReview().then(function(res) {
                console.log(res.data);
                $scope.Reviews = res.data;
            });
        }
    }
})();
