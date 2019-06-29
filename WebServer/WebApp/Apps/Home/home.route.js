'use strict';
angular.module('home.routes', [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.when('', '/home/index');
        $stateProvider
            .state('home', {
                url: '/home',
                templateUrl: '/apps/home/index.html',
                controller: 'HomeController'
            })
            .state('index', {
                url: '/index',
                templateUrl: '/apps/home/templates/home.html',
                parent: "home",
                controller: 'IndexController'


            })

            .state('homedetailproject', {
                url: '/homedetailproject',
                params: {
                    data: null
                },
                parent: "home",
                templateUrl: '/apps/home/templates/homedetailproject.html',
                controller: 'HomeDetailProjectController'
            })
            .state('homeproject', {
                url: '/project',
                templateUrl: '/apps/home/templates/homeproject.html',
                parent:"home",
                controller: 'HomeProjectController'
            })
            .state('homekonsultan', {
                url: '/konsultan',
                templateUrl: '/apps/home/templates/homekonsultan.html',
                parent: "home",
                controller: 'HomeKonsultanController'
            })
            .state('homekontraktor', {
                url: '/kontraktor',
                templateUrl: '/apps/home/templates/homekontraktor.html',
                parent: "home",
                controller: 'HomeKontraktorController'
            })
            .state('homebidang', {
                url: '/bidang',
                templateUrl: '/apps/home/templates/homebidang.html',
                parent: "home",
                controller: 'HomeBidangController'
            })

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