angular.module("admin.controllers", [])
    .controller('AdminUserController', AdminUserController)
    .controller('DashboardController', DashboardController)
    .controller('ProyekController', ProyekController)
    .controller('BidangController', BidangController)
    .controller('KonsultanController', KonsultanController)
    .controller('KontraktorController', KontraktorController)
    .controller('ProyekDetailController', ProyekDetailController)
    ;


function AdminUserController($scope, $state, BidangService, Progress) {
    //var data = AuthServices.getAgentProfile();
    $scope.Title = "Judul;";
    BidangService.get().then(function (response) {
        $scope.Datas = response;
       
    }, function (error) {
        MessageServices.error("warning", error);
    });
    $scope.logout = function () {
        document.getElementById('logoutForm').submit();
       
    };


};


function DashboardController() {
 
};


function ProyekController($scope, $stateParams, $state, ProjectService, MessageServices, BidangService, KonsultanService, KontraktorService) {
    $scope.Search = "";
    $scope.BidangSelected = $stateParams.data;
    $scope.Maximum = "100";
    $scope.Selected = {};
    $scope.model = {};
    ProjectService.get().then(function (response) {
        $scope.Datas = response;
        BidangService.get().then(function (response) {
            $scope.Bidangs = response;
            KonsultanService.get().then(function (response) {
                $scope.Konsultans = response;
                KontraktorService.get().then(function (response) {
                    $scope.Kontraktors = response;
                });
            });
        });

    }, function (error) {
        MessageServices.error("warning", error);
        });


    $scope.print = function () { AuthServices.print() };
    $scope.Save = function (data) {

        if (data.ProjekId === undefined) {
            ProjectService.post(data).then(function (response) {
                $scope.model = {};
                MessageServices.info("Success");
            });
        } else {
            ProjectService.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };

    $scope.SelectedItem = function (item) {
        $scope.Selected = null;
        $scope.model = {};
        $scope.model = item;
        $scope.Selected = item;
       
    };


    $scope.EditItem = function (item) {
        $scope.model = item;
    };


    $scope.delete = function () {
        ProjectService.delete($scope.Selected).then(function (response) {
            MessageServices.info("info", "Success");
        });
    };
    
    $scope.AddAspek = function (data) {
        var item = { ProjekId: data.ProjekId, BobotPenilaian: 0, Aspek: "", KonsultanId: data.KonsultanId, Urutan: data.AspekPenilaian.length+1 };
        data.AspekPenilaian.push(item);
     
        
    };
    $scope.NilaiAkhir = 0;
    $scope.OnChangSlider = function (data, item) {
        var selectedValue = 0;
        var nilai = 0;
        
       
        item.AspekPenilaian.forEach((x) => {
            if (data.Urutan === x.Urutan) {
                selectedValue = parseFloat(x.BobotPenilaian);
            } else {
                nilai += parseFloat(x.BobotPenilaian);
            }
        });

      
        item.AspekPenilaian.forEach((x) => {
          
            if (data.Urutan !== x.Urutan)
            {
                if (item.AspekPenilaian.length === 2) {
                    x.BobotPenilaian = (100 - selectedValue);
                } else {
                  //  x.BobotPenilaian = (parseFloat(x.BobotPenilaian) / 100) * (100 - selectedValue);
                }
               
            }
              
        });
        $scope.NilaiAkhir= 0;
        item.AspekPenilaian.forEach((x) => {
            $scope.NilaiAkhir += parseFloat(x.BobotPenilaian);
        });
    };

    $scope.RemoveItem = function (data, source) {
        var index = source.AspekPenilaian.indexOf(data);
        source.AspekPenilaian.splice(index, 1);
        var urut = 0;
        source.AspekPenilaian.forEach((x) => {
            urut += 1;
            x.Urutan = urut;
        });
    };
    

};

function ProyekDetailController($scope, $sce, $http, $stateParams, $state, ProjectService, MessageServices, Progress) {
    
    $scope.Selected = $stateParams.data;
    if ($scope.Selected === null) {
        $state.go('proyek');
    } else {
        $scope.Selected.MapView = $sce.trustAsResourceUrl($scope.Selected.Map);
        Progress.start();
        $http({
            method: 'Get',
            url: 'ap/fotos/byproject?id=' + $scope.Selected.ProjekId
        }).then(function (response) {

            $scope.Fotos = [];
            $scope.Selected.Periodes.forEach(item => {
                var data = {};
                data.PeriodeId = item.PeriodeId;
                data.Fotos = [];
                response.data.forEach(item1 => {
                    if (item.PeriodeId === item1.ItemPenilaian.Periode)
                        data.Fotos.push(item1);

                });
                $scope.Fotos.push(data);
            });
            $scope.SelectPeriode($scope.Fotos[0]);
            Progress.done();

        }, function (error) {

            MessageServices.warning(error.data.Message);
            Progress.done();
        });

    }


    $scope.Save = function (data) {

        if (data.ProjekId !== undefined) {
            ProjectService.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };

    $scope.AddAspek = function (data) {
        var item = { ProjekId: data.ProjekId, BobotPenilaian: 0, Aspek: "", KonsultanId: data.KonsultanId, Urutan: data.AspekPenilaian.length + 1 };
        data.AspekPenilaian.push(item);


    };
    $scope.NilaiAkhir = 0;
    $scope.OnChangSlider = function (data, item) {
        var selectedValue = 0;
        var nilai = 0;


        item.AspekPenilaian.forEach((x) => {
            if (data.Urutan === x.Urutan) {
                selectedValue = parseFloat(x.BobotPenilaian);
            } else {
                nilai += parseFloat(x.BobotPenilaian);
            }
        });


        item.AspekPenilaian.forEach((x) => {

            if (data.Urutan !== x.Urutan) {
                if (item.AspekPenilaian.length === 2) {
                    x.BobotPenilaian = (100 - selectedValue);
                } else {
                    //  x.BobotPenilaian = (parseFloat(x.BobotPenilaian) / 100) * (100 - selectedValue);
                }

            }

        });
        $scope.NilaiAkhir = 0;
        item.AspekPenilaian.forEach((x) => {
            $scope.NilaiAkhir += parseFloat(x.BobotPenilaian);
        });
    };

    $scope.RemoveItem = function (data, source) {
        var index = source.AspekPenilaian.indexOf(data);
        source.AspekPenilaian.splice(index, 1);
        var urut = 0;
        source.AspekPenilaian.forEach((x) => {
            urut += 1;
            x.Urutan = urut;
        });
    };


    $scope.Fotos = [];
    $scope.groupObj = 'ItemPenilaianId';
    
    $scope.SelectFoto = function (item) {
        $scope.SelectedFoto = item.Foto;
    }

    $scope.SelectPeriode = function (item) {
        $scope.Periode = item;
        $scope.SelectedFoto = item.Fotos[0].Foto;
    };

};


function BidangController($scope, BidangService, MessageServices) {
    $scope.model = {};
    $scope.Save = function (data) {

        if (data.UnitKerjaId === undefined) {
            BidangService.post(data).then(function (response) {
                $scope.model = {};
                MessageServices.info("Success");
            });
        } else {
            BidangService.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };

    BidangService.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.add("warning", error);
    });

    $scope.SelectItem = function (item) {
        $scope.model = item;
      
    };


   

}


function KonsultanController($scope, KonsultanService, MessageServices) {
    $scope.model = {};
    $scope.Save = function (data) {

        if (data.ID === undefined) {
            KonsultanService.post(data).then(function (response) {
                $scope.model = {};
                MessageServices.info("Success");
            });
        } else {
            KonsultanService.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };

    KonsultanService.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.add("warning", error);
        });

    $scope.SelectItem = function (item) {
        $scope.model = item;

    };


}


function KontraktorController($scope, KontraktorService, MessageServices) {
    $scope.model = {};
    $scope.Datas = [];
  
    KontraktorService.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.error(error, "Error");
        });

    $scope.SelectItem = function (item) {
        $scope.model = item;
    };

    $scope.Save = function (data) {

        if (data.PengusahaId === undefined) {
            KontraktorService.post(data).then(function (response) {
                $scope.model = {};
                MessageServices.info("Success");
            });
        } else {
            KontraktorService.update(data).then(function (response) {
                MessageServices.info("Success");
            });
        }
    };
}