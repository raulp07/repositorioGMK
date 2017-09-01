new Vue({
    el: "#app",
    data: {
        locales: [],
        listaplanmarketing: [],
        listacomboproducto: [],
        listacombo: [],
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
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        CargarMapa: function (eval) {
            var arreglomapas = [];
            var _d1 = {codLocal:11, codCombo:1, cantProyeccionVenta:30, nombreCaractComboVenta:'prueba 1', impProyeccionCosto:40};
            var _d2 = { codLocal: 12, codCombo: 2, cantProyeccionVenta: 40, nombreCaractComboVenta: 'prueba 2', impProyeccionCosto: 30 };
            var _d3 = { codLocal: 13, codCombo: 1, cantProyeccionVenta: 50, nombreCaractComboVenta: 'prueba 3', impProyeccionCosto: 20 };
            arreglomapas.push(_d1);
            arreglomapas.push(_d2);
            arreglomapas.push(_d3);

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
            var _index = 0;
            $.each(_locales, function (key, value) {
                $.each(arreglomapas, function (key2, value2) {
                    if (value.id == value2.codLocal) {

                        window["marker" + _index] = new google.maps.Marker({
                            position: { lat: parseFloat(value.latitud), lng: parseFloat(value.longitud) },
                            map: map,
                            shape: shape,
                            title: value2.nombreCaractComboVenta,
                            zIndex: _index
                        });
                        _index++;
                    }
                });
            });
            return;
            for (var i = 0; i < this.locales.length; i++) {
                var beach = this.locales[i];
                window["marker" + i] = new google.maps.Marker({
                    position: { lat: beach[1], lng: beach[2] },
                    map: map,
                    shape: shape,
                    title: beach[0],
                    zIndex: beach[3]
                });

                //window["infowindow" + i] = new google.maps.InfoWindow({
                //    content: contentString
                //});
            }
           

            axios.post('/PropuestaIndicador/ListaLocales/').then(function (response) {
                var cod_ini = 0;
                var datos = [];
                $.each(response.data.Local, function (key, value) {
                    var list = [value.nombre, parseFloat(value.latitud), parseFloat(value.longitud), value.id];
                    datos.push(list);
                });

                this.locales = datos;

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

                for (var i = 0; i < this.locales.length; i++) {
                    var beach = this.locales[i];
                    window["marker" + i] = new google.maps.Marker({
                        position: { lat: beach[1], lng: beach[2] },
                        map: map,
                        shape: shape,
                        title: beach[0],
                        zIndex: beach[3]
                    });

                    //window["infowindow" + i] = new google.maps.InfoWindow({
                    //    content: contentString
                    //});
                }

            }.bind(this)).catch(function (error) {
                console.log(error);
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


            var arreglomapas = [];
            var _d1 = { codLocal: 11, codCombo: 1, cantProyeccionVenta: 30, nombreCaractComboVenta: 'prueba 1', impProyeccionCosto: 40 };
            var _d2 = { codLocal: 12, codCombo: 2, cantProyeccionVenta: 40, nombreCaractComboVenta: 'prueba 2', impProyeccionCosto: 30 };
            var _d3 = { codLocal: 13, codCombo: 1, cantProyeccionVenta: 50, nombreCaractComboVenta: 'prueba 3', impProyeccionCosto: 20 };
            arreglomapas.push(_d1);
            arreglomapas.push(_d2);
            arreglomapas.push(_d3);

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
            var _index = 0;
            var _html = '';
            debugger;
            $.each(_locales, function (key, value) {
                $.each(arreglomapas, function (key2, value2) {
                    debugger;
                    if (value[3] == value2.codLocal) {

                        window["marker" + _index] = new google.maps.Marker({
                            position: { lat: parseFloat(value[1]), lng: parseFloat(value[2]) },
                            map: map,
                            shape: shape,
                            title: value2.nombreCaractComboVenta,
                            zIndex: _index
                        });
                        _html = '<h3>Proyección de venta ' + value2.cantProyeccionVenta + '</h3>' +
                                '<br />' +
                                '<h3>Proyección de costa ' + value2.cantProyeccionVenta + '</h3>';
                        window["infowindow" + _index] = new google.maps.InfoWindow({
                            content: _html
                        });
                        window["marker" + _index].addListener('click', function () {
                            var indice = this.zIndex ;
                            window["infowindow" + indice].open(map, window["marker" + indice]);
                        });
                        
                        _index++;
                    }
                });
            });

            return;


            var locals = '';
            $.each(this.locales, function (key, value) {
                locals += value.id + ',';
            });
            locals = locals.substring(0, locals.length - 1);
            var param = {
                listLocal: local,
                codCombo: $('#cboProducto').val(),
                indConsumo: $('#rangeConsumo_1').val(),
                indSabor: $('#rangeSabor_2').val(),
                indCosto: $('#rangeCosto_3').val(),
                cantPuntuacionMax: ''
            }

            var json = { listCalcularPropuestaxIndicador: param };
            axios.post("/PropuestaIndicador/CalcularPropuestaxIndicadores/", json).then(function (response) {

            }.bind(this)).catch(function (error) {
                console.log(error);
            });

        },
        VisualizarValor: function (eval) {
            var dato = eval.target.id.split('_')[1];
            var valor = eval.target.value;
            $('#span_' + dato).text(valor);
        },
    },
    computed: {},
    created: function () {
        this.ListaPlanMarketing();
        
    },
    mounted: function () {
    }
})