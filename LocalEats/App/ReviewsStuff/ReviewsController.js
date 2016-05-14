(function () {
    'use strict';

    angular
        .module('localeatsapp')
        .controller('ReviewsController', ReviewsController);

    ReviewsController.$inject = ['$scope', 'Reviewfactory'];

    function ReviewsController($scope, Reviewfactory) {
        /* jshint validthis:true */
  
     

        activate();

        $scope.saveReview = function () {
            $scope.newReview.RestaurantId = $scope.restaurantId;
            console.log($scope.newReview);
            Reviewfactory.saveReview($scope.newReview).then(function(res) {
                $scope.newReview.Text = "";
                $scope.newReview.Score = "";

                console.log(res.data);


                $scope.Reviews.push(res.data);
            });
        }

        function activate() {
            Reviewfactory.getAllReviews($scope.restaurantId).then(function(res) {
                console.log(res.data);
                $scope.Reviews = res.data;
            });
        }
    }
})();
