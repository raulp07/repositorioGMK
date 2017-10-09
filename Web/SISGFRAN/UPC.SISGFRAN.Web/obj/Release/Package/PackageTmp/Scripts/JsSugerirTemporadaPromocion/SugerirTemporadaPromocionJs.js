new Vue({
    el: "#app",
    data: {
        combos: {},
        dataarray: [],
        periodo: 0,
        anio: 0,
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
            if ($('#ddlcombos').val() == 0) {
                alert("Debe selecionar un combo");
                return;
            }
            if ($('#periodoini').val() >= $('#periodofin').val()) {
                alert("El periodo fin debe ser mayor al periodo inicial");
                return;
            }
            if ($('#anioini').val() >= $('#aniofin').val()) {
                alert("El año fin debe ser mayor al año inicial");
                return;
            }


            var param = {
                codCombo: $('#ddlcombos').val(),
                periodoini: $('#periodoini').val(),
                periodofin: $('#periodofin').val(),
                anioini: $('#anioini').val(),
                aniofin: $('#aniofin').val(),
            }

            //var param = {
            //    codCombo: 2,
            //    periodoini: 2,
            //    periodofin: 4,
            //    anioini: 2015,
            //    aniofin: 2017,
            //}
            var json = { DE: param }
            axios.post("/SugerirTemporadaPromocion/CalcularPorcentajexPeriodo/", json).then(function (response) {

                var CountCabezera = 0;
                var _dataarray = [];
                var cabezera = ["Periodo"];
                var cuerpo = [];
                var _codCombo = "";
                var _codLocal = "";
                $.each(response.data._Lista, function (k, v) {
                    if (v.codCombo != _codCombo || v.codLocal != _codLocal) {
                        cabezera.push(v.NombreCombo + ' - ' + v.NombreLocal);
                        CountCabezera++;
                    }
                    _codCombo = v.codCombo;
                    _codLocal = v.codLocal;
                });
                _dataarray.push(cabezera);

                var _periodo = 0;
                var _anio = 0;
                $.each(response.data._Lista, function (k, v) {
                    if (v.periodo != _periodo || v.anioVenta != _anio) {
                        var anioperiodo = v.periodo + '(' + v.anioVenta + ')';
                        cuerpo = [];
                        cuerpo.push(v.periodo + '(' + v.anioVenta + ')');
                        for (var i = 0; i < CountCabezera; i++) {
                            var _valor = 0;
                            $.each(response.data._Lista, function (k1, v1) {
                                if (anioperiodo == v1.periodo + '(' + v1.anioVenta + ')') {
                                    if (cabezera[i + 1] == (v1.NombreCombo + ' - ' + v1.NombreLocal)) {
                                        _valor = v1.porVentaxPeridoxAnio;
                                    }
                                }
                            });
                            cuerpo.push(_valor);
                        }
                        _dataarray.push(cuerpo);
                    }
                    _periodo = v.periodo;
                    _anio = v.anioVenta;
                });
                this.dataarray = _dataarray;
                var _htmltexto = '';
                var _htmltexto = '';

                var _mayor = 0;
                var _indiceX = 0;
                var _indiceY = 0;
                for (var i = 1; i <= _dataarray.length - 1; i++) {
                    for (var j = 1; j < _dataarray[i].length; j++) {
                        _htmltexto += 'El ' + _dataarray[0][j] + ' genero un promedio de S/.' + _dataarray[i][j] + ', '

                        if (_dataarray[i][j] > _mayor) {
                            _mayor = _dataarray[i][j];
                            _indiceX = i;
                            _indiceY = j;
                        }
                    }
                    _htmltexto = _htmltexto.substring(0, _htmltexto.length - 1);
                    _htmltexto += ' en el periodo ' + _dataarray[i][0] + ' \n ';
                }
                $('#textDescripciongrafico').val(_htmltexto);
                var _periodo = _dataarray[_indiceX][0];
                var _anio = 0;
                _htmltexto = 'Se recomienda realizar la promoción del ' + _dataarray[0][_indiceY] + ' en el periodo ' + _periodo + ' , en el cual tuvo mayor cantidad de ventas'
                _anio = _periodo.split('(')[1];
                _anio = _anio.substring(0, _anio.length - 1);
                _periodo = _periodo.split('(')[0];
                this.anio = _anio;
                this.periodo = _periodo;
                $('#txtsugerencia').val(_htmltexto);

                google.charts.setOnLoadCallback(drawChart(_dataarray));

            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        ImprimirPantalla: function () {
            //if ($('#textDescripciongrafico').val() == 0) {
            //    alert("Debe generarse una grafico");
            //    return;
            //}
            var objeto = document.getElementById('columnchart_material');
            var ventana = window.open('', '_blank');
            ventana.document.write(objeto.innerHTML);
            ventana.document.close();
            ventana.print();
            ventana.close();
        },
        GuardarPropuesta: function () {

            if ($('#txtobservacion').val().trim() == "") {
                alert("Debe ingresar una observación");
                return;
            }

            if ($('#textDescripciongrafico').val() == 0) {
                alert("Debe generarse una grafico");
                return;
            } 
            

            var param = {
                NombreLocal: $('#txtobservacion').val(),
                codCombo: $('#ddlcombos').val(),
                periodo: this.periodo,
                anioini: this.anio,
            }
            var json = { DE: param }
            axios.post("/SugerirTemporadaPromocion/INS_SugerirTemporadaPromocion/", json).then(function (response) {
                if (response.data.rest > 0) {
                    alert('Se gabro exitosamente');
                    debugger;
                    this.ImprimirPantalla();

                    $('#textDescripciongrafico').val('');
                    $('#txtsugerencia').val('');
                    $('#txtobservacion').val('');
                    $('#ddlcombos').val(0);
                    $('#periodoini').val(0);
                    $('#periodofin').val(0);
                    $('#anioini').val(0);
                    $('#aniofin').val(0);
                    $('#columnchart_material').html('');
                }
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
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
            title: 'Reporte de combo locales por periodo año',
            subtitle: '',
        }
    };
    var chart = new google.charts.Bar(document.getElementById("columnchart_material"));
    chart.draw(data, google.charts.Bar.convertOptions(options));

}