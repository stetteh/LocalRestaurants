(function () {
    'use strict';

    angular
        .module('localeatsapp')
        .controller('ReviewsController', ReviewsController);

    ReviewsController.$inject = ['$scope', 'Reviewfactory'];

    function ReviewsController($scope, Reviewfactory) {
        /* jshint validthis:true */
  
     

        activate();

        $scope.saveReview = function() {
            console.log('called controller saveReview()');
            var newreview = {
                Text: $scope.newReview.Text,
                Date: $scope.newreview.Date
            };
            Reviewfactory.saveReview(newreview).then(function(res) {
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
