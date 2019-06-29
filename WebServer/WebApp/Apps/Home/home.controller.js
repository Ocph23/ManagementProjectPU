angular.module("home.controllers", [])
    .controller('LoginController', LoginController)
    .controller('RegisterController', RegisterController)
    .controller('HomeController', HomeController)
    .controller("HomeProjectController", HomeProjectController)
    .controller("HomeDetailProjectController", HomeDetailProjectController)
    .controller("HomeKontraktorController", HomeKontraktorController)
    .controller("HomeKonsultanController", HomeKonsultanController)
    .controller("HomeBidangController", HomeBidangController)
    .controller("IndexController", IndexController)
    ;


function IndexController($scope) {

}
function HomeController($scope, ProjectService, BidangService, KonsultanService, KontraktorService) {
    //ProjectService.get().then(function (response) {
    //    $scope.Datas = response;
    //    BidangService.get().then(function (response) {
    //        $scope.Bidangs = response;
    //        KonsultanService.get().then(function (response) {
    //            $scope.Konsultans = response;
    //            KontraktorService.get().then(function (response) {
    //                $scope.Kontraktors = response;
    //            });
    //        });
    //    });

    //}, function (error) {
    //    MessageServices.add("warning", error);
    //});
}

function HomeDetailProjectController($scope, $http, $sce, $stateParams, $state, ProjectService, MessageServices, Progress) {

    $scope.Selected = $stateParams.data;
    if ($scope.Selected === null) {
        $state.go('homeproject');
    }

    $scope.Selected.MapView = $sce.trustAsResourceUrl($scope.Selected.Map);

    var pieChartCanvas = $('#pieChart').get(0).getContext('2d');
    var pieChart = new Chart(pieChartCanvas);
    var PieData = [
        {
            value: $scope.Selected.Progress,
            color: '#f56954',
            highlight: '#f56954',
            label: 'Selesai'
        },
        {
            value: 100 - $scope.Selected.Progress,
            color: '#d2d6de',
            highlight: '#d2d6de',
            label: 'Belum'
        }
    ];
    var pieOptions = {
        //Boolean - Whether we should show a stroke on each segment
        segmentShowStroke: true,
        //String - The colour of each segment stroke
        segmentStrokeColor: '#fff',
        //Number - The width of each segment stroke
        segmentStrokeWidth: 2,
        //Number - The percentage of the chart that we cut out of the middle
        percentageInnerCutout: 50, // This is 0 for Pie charts
        //Number - Amount of animation steps
        animationSteps: 100,
        //String - Animation easing effect
        animationEasing: 'easeOutBounce',
        //Boolean - Whether we animate the rotation of the Doughnut
        animateRotate: true,
        //Boolean - Whether we animate scaling the Doughnut from the centre
        animateScale: false,
        //Boolean - whether to make the chart responsive to window resizing
        responsive: true,
        // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
        maintainAspectRatio: true,
        //String - A legend template
        legendTemplate: '<ul class="<%=name.toLowerCase()%>-legend"><% for (var i=0; i<segments.length; i++){%><li><span style="background-color:<%=segments[i].fillColor%>"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>'
    }
    //Create pie or douhnut chart
    // You can switch between pie and douhnut using the method below.
    pieChart.Doughnut(PieData, pieOptions);

    $scope.Fotos = [];
    $scope.groupObj = 'ItemPenilaianId';
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

    $scope.SelectFoto = function (item) {
        $scope.SelectedFoto = item.Foto;
    }

    $scope.SelectPeriode = function (item) {
        $scope.Periode = item;
        $scope.SelectedFoto = item.Fotos[0].Foto;
    };

}

function HomeProjectController($scope, ProjectService, MessageServices) {
    ProjectService.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.add("warning", error);
    });
}


function HomeBidangController($scope, BidangService, MessageServices) {
    BidangService.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.add("warning", error);
    });
}


function HomeKontraktorController($scope, KontraktorService, MessageServices) {
    KontraktorService.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.add("warning", error);
    });
}


function HomeKonsultanController($scope, KonsultanService, MessageServices) {
    KonsultanService.get().then(function (response) {
        $scope.Datas = response;
    }, function (error) {
        MessageServices.add("warning", error);
    });
}


function LoginController($scope, AuthServices) {
 //   sessionStorage.clear();
    $scope.Login = function (model) {
        AuthServices.login(model);
    };
}

function RegisterController($scope, AuthServices) {
    $scope.model = {};
    $scope.register = function (model) {
        AuthServices.register(model).then(function (response) {
            $scope.model = {};
        });
    };
}
