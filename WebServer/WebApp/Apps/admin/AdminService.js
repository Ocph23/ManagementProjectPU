
angular.module("admin.services", [])
    .factory('AdminService', AdminService)
    .factory('AdminManageUserService', AdminManageUserService)
 
    .factory('AdminManageWorkerService', AdminManageWorkerService)
    .factory('InvoiceServices', InvoiceServices)
;



function AdminService($http, $state, $rootScope, $q, MessageServices) {
    var service = {};
    var instance = false;
    var datas = [];

    return {
        get: get, post:post,delete:deleteItem,update:update
    };

    function get() {
        var deffer = $q.defer();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/Agent'
            }).then(function (response) {
                instance = true;
                response.data.forEach(item => {
                    datas.push(item);
                });
                deffer.resolve(datas);
            }, function (error) {
                deffer.reject();
                MessageServices.warning(error.data.Message);
            });
        } else {
            deffer.resolve(datas);
        }

        return deffer.promise;
    }

    function post(model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'api/Agent',
            data:model
        }).then(function (response) {
            datas.push(response.data);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }


    function deleteItem(model) {
        var deffer = $q.defer();
        $http({
            method: 'Delete',
            url: 'api/Agent/'+model.AgentId
        }).then(function (response) {
            var index = datas.indexOf(model);
            datas.splice(index, 1);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }



    function update(model) {
        var deffer = $q.defer();
        $http({
            method: 'put',
            url: 'api/Agent/' + model.AgentId,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }

}

function AdminManageWorkerService($http,  $q,MessageServices) {
    var service = {};
    var datas = [];
    var instance = false;
    return {
        get: get, post: post, update: update,lock:lock,unlock:unlock

    };
    
    function get() {
        var deffer = $q.defer();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/userprofile'
            }).then(function (response) {
                instance = true;
                response.data.forEach(item => {
                    datas.push(item);
                });
                deffer.resolve(datas);
            }, function (error) {
                deffer.reject();
                MessageServices.warning(error.data.Message);
            });
        } else {
            deffer.resolve(datas);
        }

        return deffer.promise;
    }

    function post(model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'api/UserProfile/Admin',
            data: model
        }).then(function (response) {
            datas.push(response.data);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }



    function update(id,model) {
        var deffer = $q.defer();
        $http({
            method: 'put',
            url: 'api/UserProfile/' +id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }

    function lock(email) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/UserProfile/Lock?email=' + email
        }).then(function (response) {
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }


    function unlock(email) {
        var deffer = $q.defer();
        $http({
            method: 'get',
            url: ' api/UserProfile/UnLock?email=' + email
        }).then(function (response) {
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }

}

function AdminManageUserService($http,MessageServices, $q) {
    var service = {};
    var datas = [];

    return {
        get: get, post: post, delete: deleteItem, update: update
    };

    function get(agentId) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'agent/AgentUsers?agentId=' + agentId
        }).then(function (response) {
            instance = true;
            response.data.forEach(item => {
                datas.push(item);
            });
            deffer.resolve(datas);
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
        });

        return deffer.promise;
    }

    function post(agentId,model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'agent/AddNewUser?agentId=' + agentId,
            data: model
        }).then(function (response) {
            datas.push(response.data);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
        });

        return deffer.promise;
    }


    function deleteItem(id,model) {
        var deffer = $q.defer();
        $http({
            method: 'Put',
            url: 'agent/ChangeActive?agentId=' + id,
            data:model
        }).then(function (response) {
            var index = datas.indexOf(model);
            datas.splice(index, 1);
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }




    function update(model) {
        var deffer = $q.defer();
        $http({
            method: 'put',
            url: 'agent/UpdateProfile?agentId='+model.Id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }

}

function InvoiceServices($http, $q, MessageServices) {
    var instance = false;
    var datas = [];

    return {
        get: get, getItems:getItems, getInvoiceByNumber: getInvoiceByNumber, post: post, delete: deleteItem, update: update,
        addNewItem: addNewItem, updateItem: updateItem,verification:verification
    };


    function verification(model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'api/Invoice/addpembayaran',
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success");
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }


    function getInvoiceByNumber(id) {
        var deffer = $q.defer();
        $http({
            method: 'Get',
            url: 'api/invoice/' + id
        }).then(function (response) {
            deffer.resolve(response.data);
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
        });

        return deffer.promise;
    }

    function get() {
        var deffer = $q.defer();
        NProgress.start();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/Invoice'
            }).then(function (response) {
                instance = true;
                response.data.forEach(item => {
                    datas.push(item);
                });
                deffer.resolve(datas);
                
                NProgress.done();

            }, function (error) {
                deffer.reject();
                MessageServices.warning(error.data.Message);
                NProgress.done();
            });
        } else {
            deffer.resolve(datas);
            NProgress.done();
        }

        return deffer.promise;
    }

    function post(model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'api/invoice',
            data: model
        }).then(function (response) {
            datas.push(response.data);
            deffer.resolve(response.data);
            MessageServices.success(error.data.Message);
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;

    }

    function update(model) {
        var deffer = $q.defer();
        $http({
            method: 'put',
            url: 'api/invoice/' + model.Id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success", "Update Payment");
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }

    function getItems(id) {
        var deffer = $q.defer();
        NProgress.start();
        $http({
            method: 'Get',
            url: 'api/invoice/getitems?id=' + id
        }).then(function (response) {
            deffer.resolve(response.data);
            NProgress.done();
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
            NProgress.done();
        });
        return deffer.promise;
    }
    
    function addNewItem(id, model) {
        var deffer = $q.defer();
        $http({
            method: 'Post',
            url: 'api/invoice/additem?id=' + id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success");
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }

    function updateItem(id, model) {
        var deffer = $q.defer();
        $http({
            method: 'Put',
            url: 'api/invoice/updateitem?id=' + id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success");
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }


    function deleteItem(model) {
        var deffer = $q.defer();
        $http({
            method: 'Delete',
            url: 'api/invoice/deleteitem?id=' + model.Id
        }).then(function (response) {
          
            deffer.resolve(response);
          
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
        });

        return deffer.promise;
    }




  

}



