(function () {
    'use strict';

    angular
        .module('localeatsapp')
        .factory('Reviewfactory', factory);

    factory.$inject = ['$http'];

    function factory($http) {
        var service = {
            getReview: getReview,
            saveReview: saveReview
        };

        return service;

        function getReview() {
            return $http.get('/Reviews/ShowReviews');
        }

        function saveReview(newReview) {
            console.log('factory save review')
            return $http.post('/Reviews/SaveReview', newReview);
        }
    }
})();