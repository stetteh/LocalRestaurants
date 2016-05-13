(function () {
    'use strict';

    angular
        .module('app')
        .controller('ReviewsController', ReviewsController);

    ReviewsController.$inject = ['$scope','$location', 'Reviewfactory'];

    function ReviewsController($scope,$location, Reviewfactory) {
        /* jshint validthis:true */
  
     

        activate();

        $scope.saveReview = function() {
            console.log('called controller saveReview()');
            var newReview = {
                Text: data.data.Text,
                Date: data.data.Date
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
