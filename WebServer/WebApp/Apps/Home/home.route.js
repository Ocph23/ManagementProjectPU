'use strict';
angular.module('home.routes', [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.when('', '/login');
        $stateProvider
            .state('login', {
                url: '/login',
                templateUrl: '/apps/home/login.html',
                controller: 'LoginController'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/apps/home/register.html',
                controller: 'RegisterController'
            });
    });