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
                var cabezera = '["Periodo",';
                var cuerpo = '';
                var nombreTemporal = "";
                $.each(response.data._Lista, function (k, v) {
                    if (v.codCombo != nombreTemporal) {
                        cabezera += '"' + v.NombreCombo + '",';
                        CountCabezera++;
                    }
                    nombreTemporal = v.codCombo;


                });
                cabezera = cabezera.substring(0, cabezera.length - 1) + ']';
                $.each(response.data._Lista, function (k, v) {
                    cuerpo += '["' + v.periodo + '(' + v.anioVenta + ')"';
                    for (var i = 0; i < CountCabezera; i++) {
                        if (JSON.parse(cabezera)[i + 1] == v.NombreCombo) {
                            cuerpo += ',' + v.porVentaxPeridoxAnio;
                        } else {
                            cuerpo += ',' + 0;
                        }
                    }
                    cuerpo += '],';
                });

                cuerpo = cuerpo.substring(0, cuerpo.length - 1);
                var _DATA = JSON.parse('[' + cabezera + ',' + cuerpo + ']');

                var _htmltexto = '';
                var _htmltexto = '';
                for (var i = 1; i <= _DATA.length-1; i++) {
                    for (var j = 1; j < _DATA[i].length; j++) {
                        _htmltexto += 'el ' + _DATA[0][j] + ' genero un promedio de S/.' + _DATA[i][j] + ', '
                    }
                    _htmltexto = _htmltexto.substring(0, _htmltexto.length - 1);
                    _htmltexto += ' en el periodo ' + _DATA[i][0] +' - ';
                }
                $('#textDescripciongrafico').text(_htmltexto);



                google.charts.setOnLoadCallback(drawChart(_DATA));

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