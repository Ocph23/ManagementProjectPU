
angular.module("admin.services", [])
    .factory('ProjectService', ProjectService)
    .factory('BidangService', BidangServices)
    .factory('KonsultanService', KonsultanService)
    .factory('KontraktorService', KontraktorService)
    ;


function ProjectService($http, $state, $rootScope, $q, MessageServices) {
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
                url: 'api/projects'
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
            url: 'api/projects',
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
            url: 'api/projects/'+model.AgentId
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
            url: 'api/projects/' + model.ProjekId,
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

function BidangServices($http, $q, MessageServices, Progress) {
    var instance = false;
    var datas = [];
 
    return {
        get: get, post: post, delete: deleteItem, update: update
    };

    function get() {
        var deffer = $q.defer();
        Progress.start();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/unitkerja'
            }).then(function (response) {
                instance = true;
                datas = [];
                response.data.forEach(item => {
                    datas.push(item);
                });
                deffer.resolve(datas);
                Progress.done();

            }, function (error) {
                deffer.reject();
                MessageServices.warning(error.data.Message);
                Progress.done();
            });
        } else {
            deffer.resolve(datas);
            Progress.done();
        }

        return deffer.promise;
    }

    function post(model) {

        var deffer = $q.defer();
        Progress.start();
        $http({
            method: 'Post',
            url: 'api/unitkerja',
            data: model
        }).then(function (response) {
            datas.push(response.data);
            deffer.resolve(response.data);
            Progress.done();
           
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
            Progress.done();
        });

        return deffer.promise;

    }

    function update(model) {
        var deffer = $q.defer();
        Progress.start();
        $http({
            method: 'put',
            url: 'api/unitkerja/' + model.UnitKerjaId,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success", "Update Payment");
            Progress.done();
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
            Progress.done();
        });

        return deffer.promise;
    }

   
    function deleteItem(model) {
        var deffer = $q.defer();
        Progress.start();
        $http({
            method: 'Delete',
            url: 'api/unitkerja/' + model.UnitKerjaId
        }).then(function (response) {
          
            deffer.resolve(response);
            Progress.done();
          
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
            Progress.done();
        });

        return deffer.promise;
    }




  

}



function KonsultanService($http, $q, MessageServices, Progress) {
    var instance = false;
    var datas = [];

    return {
        get: get, post: post, delete: deleteItem, update: update
    };

    function get() {
        var deffer = $q.defer();
        Progress.start();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/konsultans'
            }).then(function (response) {
                instance = true;
                response.data.forEach(item => {
                    datas.push(item);
                });
                deffer.resolve(datas);

                Progress.done();

            }, function (error) {
                deffer.reject();
                MessageServices.warning(error.data.Message);
                Progress.done();
            });
        } else {
            deffer.resolve(datas);
            Progress.done();
        }

        return deffer.promise;
    }

    function post(model) {
        var deffer = $q.defer();
        Progress.start();
        $http({
            method: 'Post',
            url: 'api/konsultans',
            data: model
        }).then(function (response) {
            datas.push(response.data);
            deffer.resolve(response.data);
            Progress.done();
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
            Progress.done();
        });

        return deffer.promise;

    }

    function update(model) {
        var deffer = $q.defer();
        Progress.start();
        $http({
            method: 'put',
            url: 'api/konsultans/' + model.ID,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            Progress.done();
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
            Progress.done();
        });

        return deffer.promise;
    }


    function deleteItem(model) {
        var deffer = $q.defer();
        Progress.start();
        $http({
            method: 'Delete',
            url: 'api/konsultans/' + model.ID
        }).then(function (response) {

            deffer.resolve(response);
            Progress.done();
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
            Progress.done();
        });

        return deffer.promise;
    }






}




function KontraktorService($http, $q, MessageServices, Progress) {
    var instance = false;
    var datas = [];

   
    return {
        get: get, post: post, delete: deleteItem, update: update
    };

    function get() {
        var deffer = $q.defer();
        Progress.start();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/Company'
            }).then(function (response) {
                instance = true;
                response.data.forEach(item => {
                    datas.push(item);
                });
                deffer.resolve(datas);

                Progress.done();

            }, function (error) {
                deffer.reject();
                MessageServices.warning(error.data.Message);
                Progress.done();
            });
        } else {
            deffer.resolve(datas);
            Progress.done();
        }

        return deffer.promise;
    }

    function post(model) {
        var deffer = $q.defer();
        Progress.start();
        $http({
            method: 'Post',
            url: 'api/Company',
            data: model
        }).then(function (response) {
            datas.push(response.data);
            deffer.resolve(response.data);
            Progress.done();
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
            Progress.done();

        });

        return deffer.promise;

    }

    function update(model) {
        var deffer = $q.defer();
        Progress.start();
        $http({
            method: 'put',
            url: 'api/Company/' + model.PengusahaId,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Success", "Update Payment");
            Progress.done();
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
            Progress.done();
        });

        return deffer.promise;
    }


    function deleteItem(model) {
        var deffer = $q.defer();
        Progress.start();
        $http({
            method: 'Delete',
            url: 'api/Company/' + model.PengusahaId
        }).then(function (response) {

            deffer.resolve(response);
            Progress.done();
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
            Progress.done();
        });

        return deffer.promise;
    }






}




