
angular.module("home.services", [])
    .factory('AuthServices', AuthServices)
    .factory('MessageServices', MessageServices)
    .factory('CityServices', CityServices)
    .factory('PriceServices', PriceServices)
    .factory('StatusServices', StatusServices)
    .factory('TerbilangServices', TerbilangServices)
    ;

function MessageServices($rootScope) {
    var alertService = {};
    $rootScope.alerts = [];

    // will automatidally close
    // types are success, warning, info, danger
    alertService.info = function (msg, title) {
        var titleText = title;
        if (title === undefined) {
            titleText === "Info";
        }

        message(msg, titleText, "info");

        window.setTimeout(function () {
            $("#MessageBox").click();
        }, 3000);
    };

    alertService.warning = function (msg, title) {
        var titleText = title;
        if (title === undefined) {
            titleText === "Warning";
        }

        message(msg, titleText, "warning");

        window.setTimeout(function () {
            $("#MessageBox").click();
        }, 3000);
    };

    alertService.error = function (msg, title) {
        var titleText = title;
        if (title === undefined) {
            titleText === "Error";
        }
        message(msg, titleText, "error");

        window.setTimeout(function () {
            $("#MessageBox").click();
        }, 3000);
    };

    function message(text,title, type) {
        $("body").append($("<div id='MessageBox' class='MessageBox'><div class='content'><h4> <b class='title'></b> </h4><p class='message'></p></div></div>"));
        $('.title').text(title);
        $('.message').text(text);
        $('#MessageBox').css({ 'background': '#dc3545' }).fadeIn(600);

        
        if (type === "error")
            $('#MessageBox').css({ 'background': '#dc3545' }).fadeIn(600);
        if (type === "info")
            $('#MessageBox').css({ 'background': ' #17a2b8' }).fadeIn(600);
        if (type === "warning")
            $('#MessageBox').css({ 'background': ' #f0ad4e' }).fadeIn(600);

        $("#MessageBox").click(function () {
            $(this).remove();
        });
    }

    return alertService;

}



function AuthServices($http, $state, $rootScope, MessageServices, $q) {
    //var def = $q.defer();

    var service = {
        login: login, register: register, getUserAdminProfile: getUserAdminProfile, getUserAgentProfile: getUserAgentProfile,
        logout: logout, userRoleIs: userRoleIs,
        getToken: getToken, getHeaders: getHeaders, getUserProfile: getUserProfile, print: printDoc
    };

    function printDoc() {
        window.print();
    }

    function login(user) {
        var data = "grant_type=password&username=" + user.Email + "&password=" + user.Password;
        NProgress.start();
        $http({
            method: 'POST',
            url: '/Token',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: data
        }).then(function successCallback(response) {
            var result = response.data;
            sessionStorage.setItem("AuthToken", result.access_token);
            sessionStorage.setItem("TokenType", result.token_type);
            sessionStorage.setItem("UserName", user.Email);
            sessionStorage.setItem("Role", result.roles);
            NProgress.done();
            var doc = document.getElementById('loginForm');
            doc.style.animation='hideLogin 3s';
            window.setTimeout(function () {
                $state.go(result.roles.toLowerCase());
            }, 2000);

            }, function errorCallback(response) {
                MessageServices.error(response.data.error_description, response.data.error);
                    
            NProgress.done();
        });
    }
    function logout() {
        sessionStorage.clear();
        $state.go('login');
    }

    function register(model) {
        var def = $q.defer();
        $http({
            method: 'Post',
            url: '/account/register',
            data: model
        }).then(function successCallback(response) {
            MessageServices.success("Register Success, Silahkan Login");
            def.resolve(response);
        }, function errorCallback(response) {
            MessageServices.error(response.data);
            def.reject();
            });
        return def.promise;
    }

    function userRoleIs(role) {
        var sessionRole = sessionStorage.getItem("Role");
        if (role === sessionRole) {
            return true;
        } else {
            $state.go('login');
        }
       
    }

    function getUserAgentProfile() {

        var defer = $q.defer();
        $http({
            headers: getHeaders(),
            method: 'Get',
            url: 'api/UserProfile/agent'
        }).then(function (response) {
            defer.resolve(response.data);
            sessionStorage.setItem("UserProfile", JSON.stringify(response.data));
        }, function (error) {
            MessageServices.error(error.data.Message);
            defer.reject();

        });
        return defer.promise;
    }
    
    function getUserProfile() {

        var profile = sessionStorage.getItem("UserProfile");
        return JSON.parse(profile);
    }


    function getUserAdminProfile() {

        var defer = $q.defer();
        $http({
            headers: getHeaders(),
            method: 'Get',
            url: '/api/UserProfile/admin'
        }).then(function (response) {
            defer.resolve(response.data);
            sessionStorage.setItem("UserProfile", JSON.stringify(response.data));
        }, function (error) {
            MessageServices.error(error.data.Message);
            defer.reject();

        });
        return defer.promise;
    }

    this.token = sessionStorage.getItem("AuthToken");

    function getToken() {
        return sessionStorage.getItem("AuthToken");
    }

    function getHeaders() {
        if (getToken() === null)
            $state.go('login');
        else

            return { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + getToken() };
    }

    return service;
}




function CityServices($http, $state, $rootScope, MessageServices, $q, AuthServices) {
    var service = {};
    var instance = false;
    var datas = [];

    return {
        get: get, post: post, delete: deleteItem, update: update
    };

    function get() {
        var deffer = $q.defer();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/City'
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
            url: 'api/City',
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


    function deleteItem(model) {
        var deffer = $q.defer();
        $http({
            method: 'Delete',
            url: 'api/City/' + model.Id
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
            url: 'api/City',
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

function PriceServices($http, $state, $rootScope, MessageServices, $q, AuthServices) {
    var service = {};
    var instance = false;
    var datas = [];

    return {
        get: get, post: post, delete: deleteItem, update: update
    };

    function get(agentId) {
        var deffer = $q.defer();
        if (!instance) {
            $http({
                method: 'Get',
                url: 'api/Price/' + agentId
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
            url: 'api/Price',
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


    function deleteItem(model) {
        var deffer = $q.defer();
        $http({
            method: 'Delete',
            url: 'api/Price/' + model.Id
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
            url: 'api/Price/'+model.Id,
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

function StatusServices($http,MessageServices, $q, AuthServices) {
  
    return {
        post: post, update: update
    };

    function post(model) {
        var deffer = $q.defer();
        NProgress.start();
        $http({
            method: 'Post',
            url: 'api/status',
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Succses","Add New Status");
            NProgress.done();
        }, function (error) {
            deffer.reject();
            MessageServices.warning(error.data.Message);
            NProgress.done();
        });

        return deffer.promise;
    }

    function update(model) {
        var deffer = $q.defer();
        NProgress.start();
        $http({
            method: 'put',
            url: 'api/status?Id=' + model.Id,
            data: model
        }).then(function (response) {
            deffer.resolve(response);
            MessageServices.info("Succses", "Update Status");
            NProgress.done();
        }, function (error) {
            deffer.reject();
            MessageServices.error(error.data.Message);
            NProgress.done();
        });

        return deffer.promise;
    }

}


function TerbilangServices() {

    var result;
    return { terbilang: terbilang, capitalize: capitalize };

    function capitalize(str) {
        str = str.trim().split(" ");

        for (var i = 0, x = str.length; i < x; i++) {
            str[i] = str[i][0].toUpperCase() + str[i].substr(1);
        }

        return str.join(" ");
    }

    function terbilang(txtsrc) {

       // var txtsrc;
        var options = {
            lang: 'id',
            output: result
        };
        var setting = $.extend({
            lang: 'id',
            output: result
        }, options);

        var IDreadThousand = function (n, dg, snum, thousandDesc) {
            var s = '';
            var d1 = Math.floor(n / 100);
            var d2 = Math.floor((n - (d1 * 100)) / 10);
            var d3 = n - (d1 * 100) - (d2 * 10);

            if (d1 > 0) {
                if (d1 == 1) {
                    s = s + 'seratus ';
                } else {
                    s = s + snum[d1] + ' ratus ';
                }
            }

            if (d2 > 0) {
                if (d2 == 1) {
                    switch (d3) {
                        case 0: s = s + 'sepuluh ';
                            break;
                        case 1: s = s + 'sebelas ';
                            break;
                        default: s = s + snum[d3] + ' belas ';
                    }
                } else {
                    s = s + snum[d2] + ' puluh ';
                }
            }

            if (d3 > 0) {
                if ((d2 > 1) || (d2 == 0)) {
                    if ((dg == 1) & (d3 == 1)) {
                        s = s + 'se';
                    } else {
                        s = s + snum[d3] + ' ';
                    }
                }
            }

            return s;
        }

        var ENreadThousand = function (n, snum, thousandDesc, steens, stens) {
            var s = '';
            var d1 = Math.floor(n / 100);
            var d2 = Math.floor((n - (d1 * 100)) / 10);
            var d3 = n - (d1 * 100) - (d2 * 10);

            if (d1 > 0) {
                s = s + snum[d1] + ' hundred ';
            }

            if (d2 > 0) {
                if (d2 == 1) {
                    s = s + steens[d3] + ' ';
                } else {
                    s = s + stens[d2] + ' ';
                }
            }

            if (d3 > 0) {
                if ((d2 > 1) || (d2 == 0)) {
                    s = s + snum[d3] + ' ';
                }
            }

            return s;
        }

        var readAll = function (x, lang) {
            var s = '';
            var i = 0;
            var isfailed = false;

            switch (lang) {
                case 'id': var snum = new Array('nol', 'satu', 'dua', 'tiga', 'empat', 'lima', 'enam', 'tujuh', 'delapan', 'sembilan');
                    var thousandDesc = new Array('', 'ribu', 'juta', 'miliar', 'triliun');
                    break;
                case 'en': var snum = new Array('zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine');
                    var thousandDesc = new Array('', 'thousand', 'million', 'billion', 'trillion');
                    var steens = new Array('ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen');
                    var stens = new Array('', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety');
                    break;
                default: alert('Unknown language');
                    isfailed = true;
            }

            if (isNaN(x)) {
                isfailed = true;
                alert('Not a number!');
            } else {
                x = parseFloat(x);
            }

            if (isfailed == false) {
                do {
                    var groupNumbers = Math.round(((Math.floor(x) / 1000) - Math.floor((Math.floor(x) / 1000))) * 1000);
                    if (lang == 'id') {
                        s = IDreadThousand(groupNumbers, i, snum, thousandDesc) + thousandDesc[i] + ' ' + s;
                        if (x == 0) { s = 'nol' };
                    }
                    if (lang == 'en') {
                        s = ENreadThousand(groupNumbers, snum, thousandDesc, steens, stens) + thousandDesc[i] + ' ' + s;
                        if (x == 0) { s = 'zero' };
                    }
                    x = Math.floor(Math.floor(x) / 1000);
                    i++;
                } while (x > 0);
                return s.replace(/^\s*|\s(?=\s)|\s*$/g, '');
            } else {
                return 'NaN';
            }
        }

        var readNumbers = function () {
            var txtout = setting.output;
            switch (setting.lang) {
                case 'id': var lang = setting.lang;
                    var cur = 'rupiah';
                    var cen = 'sen';
                    break;
                case 'en': var lang = setting.lang;
                    var cur = 'dollar';
                    var cen = 'cent';
                    break;
            }

            var ssrc = txtsrc;
            // window.alert(ssrc);
            var ssrc = txtsrc.toString().split(/[.]|[,]/);
            var sout = readAll(ssrc, lang) + ' ' + cur;
            var sout1 = '';
            if (ssrc[1] != undefined) {
                sout1 = readAll(ssrc[1].substr(0, 2), lang) + ' ' + cen;
            }
            if ((sout.search('NaN') != -1) || (sout1.search('NaN') != -1)) {
                txtsrc.val('');
                txtout.val('');
            } else {
                result=sout + ' ' + sout1;
            }
        }

        readNumbers();

        return result;
      
    }
}