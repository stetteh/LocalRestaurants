(function () {
    'use strict';

    angular
        .module('localeatsapp')
        .factory('Reviewfactory', factory);

    factory.$inject = ['$http'];

    function factory($http) {
        var service = {
            getAllReviews: getAllReviews,
            saveReview: saveReview
        };

        return service;

        function getAllReviews(restaurantId) {
            return $http.get('/Reviews/GetAllReviews');
        }

        function saveReview(newReview) {
            return $http.post('/Reviews/SaveReview', newReview);
        }
    }
})();