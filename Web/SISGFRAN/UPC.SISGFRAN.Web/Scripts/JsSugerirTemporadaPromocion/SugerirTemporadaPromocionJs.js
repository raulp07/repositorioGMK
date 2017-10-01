new Vue({
    el: "#app",
    data: {
        combos: {},
        dataarray: [],
    },
    methods: {
        ListaCombo: function (eval) {
            var param = {
                codPlanMkt: "0"
            }
            var json = { Combo: param }
            axios.post("/SugerirTemporadaPromocion/ListaCombo/", json).then(function (response) {
                this.combos = response.data.Combo;
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        GenerarReporte: function () {

            //var param = {
            //    periodoini: $('#periodoini').val(),
            //    periodofin: $('#periodofin').val(),
            //    anioini: $('#anioini').val(),
            //    aniofin: $('#aniofin').val(),
            //}

            var param = {
                periodoini: 2,
                periodofin: 4,
                anioini: 2015,
                aniofin: 2017,
            }
            var json = { DE: param }
            axios.post("/SugerirTemporadaPromocion/CalcularPorcentajexPeriodo/", json).then(function (response) {

                var CountCabezera = 0;
                var _dataarray = [];
                var cabezera = ["Periodo"];
                var cuerpo = [];
                var nombreTemporal = "";
                $.each(response.data._Lista, function (k, v) {
                    if (v.codCombo != nombreTemporal) {
                        cabezera.push(v.NombreCombo);
                        CountCabezera++;
                    }
                    nombreTemporal = v.codCombo;
                });
                _dataarray.push(cabezera);
                $.each(response.data._Lista, function (k, v) {
                    cuerpo = [];
                    cuerpo.push(v.periodo + '(' + v.anioVenta + ')');
                    for (var i = 0; i < CountCabezera; i++) {
                        if (cabezera[i + 1] == v.NombreCombo) {
                            cuerpo.push(v.porVentaxPeridoxAnio);
                        } else {
                            cuerpo.push(0);
                        }
                    }
                    _dataarray.push(cuerpo);
                });
                this.dataarray = _dataarray;
                var _htmltexto = '';
                var _htmltexto = '';
                for (var i = 1; i <= _dataarray.length - 1; i++) {
                    for (var j = 1; j < _dataarray[i].length; j++) {
                        _htmltexto += 'el ' + _dataarray[0][j] + ' genero un promedio de S/.' + _dataarray[i][j] + ', '
                    }
                    _htmltexto = _htmltexto.substring(0, _htmltexto.length - 1);
                    _htmltexto += ' en el periodo ' + _dataarray[i][0] + ' - ';
                }
                $('#textDescripciongrafico').text(_htmltexto);



                google.charts.setOnLoadCallback(drawChart(_dataarray));

            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        ImprimirPantalla: function () {


            var objeto = document.getElementById('columnchart_material');
            var ventana = window.open('', '_blank');
            ventana.document.write(objeto.innerHTML);
            ventana.document.close();
            ventana.print();
            ventana.close();
        },
    },
    computed: {},
    created: function () { },
    mounted: function () {
        google.charts.load('current', { 'packages': ['bar'] });
        this.ListaCombo();
    }
});


function drawChart(_data) {
    var data = google.visualization.arrayToDataTable(_data);

    var options = {
        chart: {
            title: 'Company Performance',
            subtitle: 'Sales, Expenses, and Profit: 2014-2017',
        }
    };
    var chart = new google.charts.Bar(document.getElementById("columnchart_material"));
    chart.draw(data, google.charts.Bar.convertOptions(options));

}