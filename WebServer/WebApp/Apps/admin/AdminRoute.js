'use strict';
angular.module('admin.routes', [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('admin', {
                url: '/admin',
                templateUrl: '/apps/admin/index.html',
                controller: 'AdminUserController'
            })

            .state('dashboard', {
                url: '/dashboard',
                parent:'admin',
                templateUrl: '/apps/admin/templates/dashboard.html',
                controller: 'AdminUserController'
            })

            .state('adminagent', {
                url: '/adminagent',
                parent: 'admin',
                templateUrl: '/apps/admin/templates/agent.html',
                controller: 'AdminAgentController'
            })

            .state('usermanage', {
                url: '/usermanage',
                parent: 'admin',
                params: {
                    data: null
                },
                templateUrl: '/apps/admin/templates/manageuser.html',
                controller: 'AdminUserManageController'
            })

            .state('cities', {
                url: '/cities',
                parent: 'admin',
                params: {
                    data: null
                },
                templateUrl: '/apps/admin/templates/city.html',
                controller: 'AdminCityManageController'
            })

            .state('prices', {
                url: '/prices',
                parent: 'admin',
                params: {
                    data: null
                },
                templateUrl: '/apps/admin/templates/prices.html',
                controller: 'AdminPriceManageController'
            })

            .state('adminfind', {
                url: '/adminfind',
                parent: 'admin',
                templateUrl: '/apps/admin/templates/find.html',
                controller: 'AdminFindController'
            })


            .state('petugas', {
                url: '/petugas',
                parent: 'admin',
                templateUrl: '/apps/admin/templates/petugas.html',
                controller: 'AdminPetugasController'
            })

            .state('invoices', {
                url: '/invoices',
                parent: 'admin',
                templateUrl: '/apps/admin/templates/invoice.html',
                controller: 'AdminInvoiceController'
            })

            .state('createinvoice', {
                url: '/createinvoice',
                parent: 'admin',
                params: {
                    data: null
                },
                templateUrl: '/apps/admin/templates/createinvoice.html',
                controller: 'AdminCreateInvoiceController'
            })
            .state('printinvoice', {
                url: '/printinvoice',
                params: {
                    data: null, state: null
                },
                templateUrl: '/apps/admin/templates/printinvoice.html',
                controller: 'AdminPrintInvoiceController'
            })


            ;
        


        
    })
   ;