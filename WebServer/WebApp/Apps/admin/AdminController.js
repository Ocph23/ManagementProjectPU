angular.module("admin.controllers", [])
    .controller('AdminUserController', AdminUserController)
    .controller('DashboardController', DashboardController)
    .controller('AdminAgentController', AdminAgentController)
    .controller('AdminUserManageController', AdminUserManageController)
    .controller('AdminCityManageController', AdminCityManageController)
    .controller('AdminPriceManageController', AdminPriceManageController)
    .controller('AdminFindController', AdminFindController)
    .controller('AdminPetugasController', AdminPetugasController)
    .controller('AdminInvoiceController', AdminInvoiceController)
    .controller('AdminCreateInvoiceController', AdminCreateInvoiceController)
    .controller('AdminPrintInvoiceController', AdminPrintInvoiceController)
  
    ;


function AdminUserController($scope, AuthServices,$state) {
    //var data = AuthServices.getAgentProfile();
    AuthServices.userRoleIs("Admin");

    $scope.logout = function () {
        AuthServices.logout();
    };

    AuthServices.getUserAdminProfile().then(function (response) {
        $scope.User = response;
    });

}


function DashboardController() {
    AuthServices.userRoleIs("Admin");
}


function AdminAgentController($scope, AdminService, MessageServices, AdminManageUserService, AuthServices) {
    AuthServices.userRoleIs("Admin");

    $scope.Selected = {};
    $scope.model = {};
    AdminService.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.add("warning", error);
        });
    $scope.print = function () { AuthServices.print() };
    $scope.SaveAgent = function (data) {
        if (data.AgentId === undefined) {
            AdminService.post(data).then(function (response) {
                $scope.Datas.push(response);
            });
        } else {
            AdminService.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };
    $scope.SelectedItem = function (item) {
        $scope.SelectedItem = item;
    };
    $scope.EditItem = function (item) {
        $scope.model = item;
    };


    $scope.delete = function () {
        AdminService.delete($scope.SelectedItem).then(function (response) {
            MessageServices.info("info", "Success");
        });
    };




}


function AdminUserManageController($scope, $stateParams, $state, MessageServices,  AdminManageUserService, AuthServices, AdminManageWorkerService) {
    AuthServices.userRoleIs("Admin");
    $scope.LockTitle = "Lock";
    var data = $stateParams.data;
    $scope.Agent = {};
    if (data !== null) {
        $scope.Agent = data;
        AdminManageUserService.get($scope.Agent.AgentId).then(function (response) {
            $scope.Datas = response;
        }, function (error) {
            MessageServices.warning(error);
        });
    } else {
        $state.go('adminagent');
    }

    $scope.Selected = {};
    $scope.model = {};
   

    $scope.SaveAgent = function (data) {
       
        if (data.AgentId === undefined) {
            
            AdminManageUserService.post($scope.Agent.AgentId,data).then(function (response) {
                $scope.Datas.push(response);
            });
        } else {
            AdminManageUserService.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };
    $scope.SelectedItem = function (item) {
        $scope.SelectedItem = item;
    };
    $scope.EditItem = function (item) {
        $scope.model = item;
    };

    $scope.delete = function () {

        if ($scope.LockTitle === 'Lock') {
            AdminManageWorkerService.lock($scope.SelectedItem.Email).then(function (response) {
                MessageServices.warning("User Account Locked", "Warning");
            });
        } else {
            AdminManageWorkerService.unlock($scope.SelectedItem.Email).then(function (response) {
                MessageServices.info("User Active", "Success");
            });
        }

    };

    $scope.swich = function (data) {
        $scope.LockTitle = data;
    };

}


function AdminCityManageController($scope, MessageServices, CityServices, AuthServices) {
    AuthServices.userRoleIs("Admin");

    $scope.Selected = {};
    $scope.model = {};
    CityServices.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.error(error);
    });

    $scope.SaveAgent = function (data) {
        data.AgentId = 0;
        if (data.Id === undefined) {
            CityServices.post(data).then(function (response) {
                MessageServices.info("Success");
            });
        } else {
            CityServices.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };
    $scope.SelectedItem = function (item) {
        $scope.SelectedItem = item;
    };
    $scope.EditItem = function (item) {
        $scope.model = item;
    };


    $scope.delete = function () {
        CityServices.delete($scope.SelectedItem).then(function (response) {
            MessageServices.info("Success");
        });
    };


}


function AdminPriceManageController($scope, $stateParams, $state, MessageServices, CityServices, PriceServices, AuthServices) {
    AuthServices.userRoleIs("Admin");
    var data = $stateParams.data;
    if (data === null)
        $state.go('adminagent');
    else
        $scope.Agent = data;

    $scope.SortBy = "Air";
    $scope.Selected = {};
    $scope.PortType;
    $scope.model = {};
    $scope.Cities = [];
    $scope.Ports = ["Sea", "Air","Land"];
    PriceServices.get($scope.Agent.AgentId).then(function (response) {
        $scope.Datas = response;
        CityServices.get().then(function (response) {
            $scope.Cities = response;
        }, function (error) {
            MessageServices.error(error);
        });
    }, function (error) {
        MessageServices.error(error);
    });

    $scope.SaveAgent = function (data) {
        data.CityId = data.City.Id;

        if (data.Id === undefined) {
            data.AgentId = $scope.Agent.AgentId;
            PriceServices.post(data).then(function (response) {
                MessageServices.info("Success");
            });
        } else {
            PriceServices.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };
    $scope.SelectedItem = function (item) {
        $scope.SelectedItem = item;
    };
    $scope.EditItem = function (item) {
        $scope.model = item;
    };


    $scope.delete = function () {
        PriceServices.delete($scope.SelectedItem).then(function (response) {
            MessageServices.info("Success");
        });
    };


    $scope.print = function () {
        AuthServices.print();
    }
}


function AdminFindController($scope,AdminService, AuthServices, ManifestServices, StatusServices, AdminManageWorkerService) {
    AuthServices.userRoleIs("Admin");
    $scope.findBy = "STT";
    $scope.Workers = [];

    $scope.ChangeSearch = function () {
        $scope.Items = undefined;
        $scope.Manifest = undefined;
    };

    AdminService.get().then(function (response) {
        $scope.Agents = response;
        AdminManageWorkerService.get().then(function (response) {
            $scope.Workers = response;
        });
    }, function (error) {
        MessageServices.add("warning", error);
    });

    $scope.Items = [];
    $scope.Search = function (item) {
        item.STT = item.Content;
        if ($scope.findBy === "STT") {
            ManifestServices.find(item.Agent.AgentId, item).then(function (response) {
                $scope.Items = response.data;
            });
        } else {

            ManifestServices.findbyManifestNumber(item.Agent.AgentId, item).then(function (data) {
                $scope.Manifest = data;
            });
        }
    };


    $scope.Detail = function (item) {
        $scope.data = item;
    };


    $scope.ChangeStatus = function (model) {
        model.Status.CourierId = model.Status.Courier.Id;
        if (model.Status.Id === undefined) {
            model.Status.STTId = model.Id;
            StatusServices.post(model.Status).then(function (response) {
                model.Status.Id = response.data.Id;
            });
        } else {
            StatusServices.update(model.Status);
        }
    };


}


function AdminPetugasController($scope, AdminManageWorkerService,MessageServices ) {
    $scope.Roles = ["Admin", "Courier"];
    $scope.LockTitle = "Lock";

    AdminManageWorkerService.get().then(function (response) {
        $scope.Datas = response;
    });


    $scope.AddNewUser = function (model) {
        if (model.Id === undefined) {
            AdminManageWorkerService.post(model).then(function (response) {
                MessageServices.info("Success");
                $scope.Datas.push(response);
            });
        } else {
            AdminManageWorkerService.update(model.Id,model).then(function (response) {
                MessageServices.info("Success");
                $scope.Datas.push(response);
            });
        }
    };


    $scope.EditItem = function (item) {
        $scope.model = item;
    };

    $scope.SelectedItem = function (item) {
        $scope.Selected = item;
    };

    $scope.delete = function () {

        if ($scope.LockTitle === 'Lock') {
            AdminManageWorkerService.lock($scope.Selected.Email).then(function (response) {
                MessageServices.warning("User Account Locked", "Warning");
            });
        } else {
            AdminManageWorkerService.unlock($scope.Selected.Email).then(function (response) {
                MessageServices.info("User Active","Success");
            });
        }
      
    };

    $scope.swich = function (data) {
        $scope.LockTitle = data;
    };

}


function AdminInvoiceController($scope, InvoiceServices) {

    $scope.Statuses = ["Baru", "Tunda", "Panjar", "Pelunasan", "Lunas", "Batal"];

    InvoiceServices.get().then(function (response) {
        $scope.Datas = response;
    });


    $scope.SelectedItem = function (data) {
        data.Payments.every((x) => {
            if (!x.verification) {
                $scope.model = x;
                $scope.model.NumberView = data.NumberView;
                return false;
            }

            return true;
        });

    };

    $scope.SavePayment = function (model) {
        model.verification = true;
        InvoiceServices.verification(model);
    };

}


function AdminCreateInvoiceController($scope, AdminService, AuthServices, InvoiceServices, $stateParams, MessageServices) {

    $scope.Title = "Invoice Baru";
    $scope.model = $stateParams.data;
    $scope.User = {};
    
    if ($scope.model === null) {
        $scope.Saved = false;
        $scope.model = {};
        $scope.model.CreatedDate = new Date();
        $scope.model.SendInvoiceCost = 40000;
    } else {
        $scope.Title = "View/Edit";
        $scope.Saved = true;
        InvoiceServices.getItems($scope.model.Agent.AgentId).then(function (response) {
            $scope.Sources = response;
        });
    }

    AuthServices.getUserAdminProfile().then(function (response) {
        $scope.User = response;
        $scope.model.User = $scope.User;
        AdminService.get().then(function (response) {
            $scope.Agents = response;
        }, function (error) {
            MessageServices.add("warning", error);
        });
    });


    $scope.SaveChange = function (data) {

        data.AgentId = data.Agent.AgentId;
        data.PetugasId = $scope.User.Id;
        if (data.Id === undefined) {
            data.AgentId = data.Agent.AgentId;
            InvoiceServices.post(data).then(function (response) {
                $scope.Saved = true;
                InvoiceServices.getItems(data.AgentId).then(function (response) {
                    $scope.Sources=JSON.parse(response);
                });
            });
        } else {
            InvoiceServices.update(data).then(function (response) {
            });
        }
    };

    $scope.AddItems = function (model, datas) {


        var items = [];
        datas.forEach(x => {
            if (x.Selected) {
                x.InvoiceId = model.Id;
                items.push(x);
            }
        });

        if (items.length <= 0) {
            MessageServices.error("Anda Belum Memilih STT");
        } else {
            InvoiceServices.addNewItem(model.Id, items).then(function (response) {
                response.data.forEach(x => {
                    $scope.model.Items.push(x);
                });
            });
        }
    };

    $scope.SelectedItem = function (item) {
        $scope.Selected = item;
    };

    $scope.delete = function () {
        if ($scope.Selected !== undefined || $scope.Selected !== null) {
            InvoiceServices.delete($scope.Selected).then(function (response) {
                var index = $scope.model.Items.indexOf($scope.Selected);
                $scope.model.Items.splice(index, 1);
                $scope.Selected.Id = 0;
                $scope.Sources.push($scope.Selected);
            });
        } else {
            MessageServices.warning("Anda Belum Memilih  item");
        }
    };
}

function AdminPrintInvoiceController($scope, $state, $stateParams,MessageServices,TerbilangServices) {
    $scope.model = {};
    var data = $stateParams.data;

    if (data !== null) {
        data.Keterangan = "Biaya Handling Barang milik " + data.Agent.Name + " sesuai bukti STT ";
        data.Terbilang = TerbilangServices.capitalize(TerbilangServices.terbilang(data.GrandTotal));
        $scope.model = data;
    } else {
        $state.go($stateParams.state);
    }

    $scope.print = function () {
        window.print();
        $state.go($stateParams.state);
    };
}