(function () {
    'use strict';

    angular
        .module('localeatsapp')
        .factory('Reviewfactory', factory);

    factory.$inject = ['$http'];

    function factory($http) {
        var service = {
            getData: getData
        };

        return service;

        function getData() { }
    }
})();