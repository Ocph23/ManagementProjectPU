'use strict';

angular
    .module('app')

    .run(
        function ($rootScope) {
            var app = {
                'name': 'Trireksa Incoming',
                'version': '1.0.0',

            };
            $rootScope.app = app;
        }
    );
