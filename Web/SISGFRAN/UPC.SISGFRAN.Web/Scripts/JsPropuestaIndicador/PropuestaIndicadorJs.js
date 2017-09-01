new Vue({
    el: "#app",
    data: {
        locales: [],
        listaplanmarketing: [],
        listacomboproducto: [],
        listacombo: [],
        listaregistro:[],
    },
    methods: {
        ListaPlanMarketing: function () {
            axios.post("/PropuestaIndicador/ListaPlanMarketing/").then(function (response) {
                this.listaplanmarketing = response.data.PlanMarketing;
                this.ListarLocales();
            }.bind(this)).catch(function (error) {

            });
        },
        ListaCombo: function (eval) {
            if ($('#sboPlanMKT').val() != "0") {
                var param={
                    codPlanMkt: $('#sboPlanMKT').val()
                }
                var json = { Combo: param }
                axios.post("/PropuestaIndicador/ListaCombo/", json).then(function (response) {
                    this.listacombo = response.data.Combo;
                    //this.CargarMapa();
                }.bind(this)).catch(function (error) {
                    console.log(error);
                });
            }
        },
        ListarLocales : function (){
            axios.post('/PropuestaIndicador/ListaLocales/').then(function (response) {
                var datos = [];
                $.each(response.data.Local, function (key, value) {
                    var list = [value.nombre, parseFloat(value.latitud), parseFloat(value.longitud), value.id];
                    datos.push(list);
                });
                this.locales = datos;
                this.CargarMapa();
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        CargarMapa: function (eval) {

            var uluru = { lat: -9.563234298135303, lng: -71.63956062343755 };
            var map = new google.maps.Map(document.getElementById('googleMap'), {
                zoom: 6,
                center: uluru
            });

           
        },
        Proyectar: function () {
            if ($('#sboPlanMKT').val() == "0") {
                alert("Debe seleccionar un Plan de Marketing");
                return;
            }
            if ($('#cboProducto').val() == "0") {
                alert("Debe seleccionar un Combo");
                return;
            }


            //var arreglomapas = [];
            //var _d1 = { codLocal: 11, codCombo: 1, cantProyeccionVenta: 30, nombreCaractComboVenta: 'prueba 1', impProyeccionCosto: 40 };
            //var _d2 = { codLocal: 12, codCombo: 2, cantProyeccionVenta: 40, nombreCaractComboVenta: 'prueba 2', impProyeccionCosto: 30 };
            //var _d3 = { codLocal: 13, codCombo: 1, cantProyeccionVenta: 50, nombreCaractComboVenta: 'prueba 3', impProyeccionCosto: 20 };
            //arreglomapas.push(_d1);
            //arreglomapas.push(_d2);
            //arreglomapas.push(_d3);

            //var uluru = { lat: -9.563234298135303, lng: -71.63956062343755 };
            //var map = new google.maps.Map(document.getElementById('googleMap'), {
            //    zoom: 6,
            //    center: uluru
            //});
            //debugger;
            var locals = '';
            $.each(this.locales, function (key, value) {

                locals += value.id + ',';
            });
            locals = locals.substring(0, locals.length - 1);
            var param = {
                listLocal: '',
                codCombo: $('#cboProducto').val(),
                indConsumo: $('#rangeConsumo_1').val(),
                indSabor: $('#rangeSabor_2').val(),
                indCosto: $('#rangeCosto_3').val(),
                cantPuntuacionMax: ''
            }

            var json = { CalcularPropuestaxIndicador: param };
            axios.post("/PropuestaIndicador/CalcularPropuestaxIndicadores/", json).then(function (response) {

                var uluru = { lat: -9.563234298135303, lng: -71.63956062343755 };
                var map = new google.maps.Map(document.getElementById('googleMap'), {
                    zoom: 6,
                    center: uluru
                });

                var shape = {
                    coords: [1, 1, 1, 20, 18, 20, 18, 1],
                    type: 'poly'
                };
                var _ContadorLocales = 0;
                var _locales = this.locales;
                var _index = 1;
                var _html = '';
                var _listaregistro = [];

                var promconsumo = 0;
                var promsabor = 0;
                var promcosto = 0;

                $.each(_locales, function (key, value) {
                    $.each(response.data.listCalcularPropuestaxIndicador, function (key2, value2) {

                        if (value[3] == value2.codLocal) {

                            window["marker" + value2.codLocal] = new google.maps.Marker({
                                position: { lat: parseFloat(value[1]), lng: parseFloat(value[2]) },
                                map: map,
                                shape: shape,
                                title: value2.nombreCaractComboVenta,
                                zIndex: value2.codLocal
                            });
                            _html = '<br /><label>- Para el ' + value[0] + ' la cantidad de proyección de venta de ' + $('#cboProducto option:selected').text() + ' es ' + value2.cantProyeccionVenta + '</label>' +
                                    '<br />' +
                                    '<label>- Para el ' + value[0] + ' el importe de proyección en el costo para el ' + $('#cboProducto option:selected').text() + ' es de ' + value2.impProyeccionCosto + '</label>';
                            window["infowindow" + value2.codLocal] = new google.maps.InfoWindow({
                                content: _html
                            });
                            window["marker" + value2.codLocal].addListener('click', function () {
                                var indice = this.zIndex;
                                $.each(_listaregistro, function (key, value) {
                                    if (value.codLocal == indice) {
                                        debugger;
                                        $('#rangeConsumo_1').val(value.indicadorConsumo);
                                        $('#rangeSabor_2').val(value.indicadorSabor);
                                        $('#rangeCosto_3').val(value.indicadorCosto);


                                        $('#span_1').text(value.indicadorConsumo > 10 ? 10 : value.indicadorConsumo);
                                        $('#span_2').text(value.indicadorSabor > 10 ? 10 : value.indicadorSabor);
                                        $('#span_3').text(value.indicadorCosto > 10 ? 10 : value.indicadorCosto);
                                    }
                                });
                                window["infowindow" + indice].open(map, window["marker" + indice]);
                            });

                            promconsumo += value2.indConsumo;
                            promsabor += value2.indSabor;
                            promcosto += value2.indCosto;

                            var param = {
                                indicadorConsumo: value2.indConsumo,
                                indicadorSabor: value2.indSabor,
                                indicadorCosto:value2.indCosto,
                                codCombo: $('#cboProducto').val(),
                                codLocal: value2.codLocal,
                            }
                            _listaregistro.push(param);
                            _index++;
                        }
                    });
                });
                
                $('#txtConsumo').val(promconsumo/_index);
                $('#txtSabor').val(promsabor / _index);
                $('#txtCosto').val(promcosto / _index);
                this.listaregistro = _listaregistro;


            }.bind(this)).catch(function (error) {
                console.log(error);
            });

        },
        VisualizarValor: function (eval) {
            var dato = eval.target.id.split('_')[1];
            var valor = eval.target.value;
            $('#span_' + dato).text(valor);
        },
        GuardarProyeccion: function () {
            if ($('#sboPlanMKT').val() == "0") {
                alert("Debe seleccionar un Plan de Marketing");
                return;
            }
            if ($('#cboProducto').val() == "0") {
                alert("Debe seleccionar un Combo");
                return;
            }
            if (this.listaregistro.length==0) {
                alert("No hay locales en el mapa para el registro");
                return;
            }



            if (!confirm("¿Esta seguro de continuar con el registro ?")) {
                return;
            }   
            var json = this.listaregistro;
            axios.post('/PropuestaIndicador/proyectarPropuestaxIndicadores/', json).then(function (response) {
                alert("Propuesta Guardada exitosamente");
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
            
        }
    },
    computed: {},
    created: function () {
        this.ListaPlanMarketing();
        
    },
    mounted: function () {
    }
})