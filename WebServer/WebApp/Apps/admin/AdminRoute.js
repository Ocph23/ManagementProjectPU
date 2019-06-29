'use strict';
angular.module('admin.routes', [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.when('', '/admin');
        $stateProvider
            .state('admin', {
                url: '/admin',
                templateUrl: '/apps/admin/index.html',
                controller: 'AdminUserController'
            })

            .state('dashboard', {
                url: '/dashboard',
                parent:"admin",
                templateUrl: '/apps/admin/templates/dashboard.html',
                controller: 'AdminUserController'
            })

            .state('proyek', {
                url: '/proyek',
                params: {
                    data: null
                },
                parent: "admin",
                templateUrl: '/apps/admin/templates/proyek.html',
                controller: 'ProyekController'
            })

            .state('proyekdetail', {
                url: '/proyekdetail',
                parent: "admin",
                params: {
                    data: null
                },
                templateUrl: '/apps/admin/templates/proyekdetail.html',
                controller: 'ProyekDetailController'
            })

            .state('bidang', {
                url: '/bidang',
                parent: "admin",
                templateUrl: '/apps/admin/templates/bidang.html',
                controller: 'BidangController'
            })
            .state('konsultan', {
                url: '/konsultan',
                parent: "admin",
                templateUrl: '/apps/admin/templates/konsultan.html',
                controller: 'KonsultanController'
            })

            .state('kontraktor', {
                url: '/kontraktor',
                parent: "admin",
                templateUrl: '/apps/admin/templates/kontraktor.html',
                controller: 'KontraktorController'
            })



            
            ;
        


        
    })
   ;