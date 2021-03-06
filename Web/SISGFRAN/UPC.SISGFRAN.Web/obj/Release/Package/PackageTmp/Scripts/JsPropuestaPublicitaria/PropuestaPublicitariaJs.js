﻿

new Vue({
    el: "#app",
    data: {
        locales: [
                    ['Bondi Beach', -33.890542, 151.274856, 1],
                    ['Coogee Beach', -33.923036, 151.259052, 2],
                    ['Cronulla Beach', -34.028249, 151.157507, 3],
                    ['Manly Beach', -33.80010128657071, 151.28747820854187, 4],
                    ['Maroubra Beach', -33.950198, 151.259302, 5]
        ],
        ListaMedios: [],
        ListaLocal: [],
        ListaMediosSeleccionados: [],
        ListadoDetalle: [],
        ContadorLocales: 0,
    },
    methods: {
        NuevasCordenadas: function () {
            this.locales = [['Bondi Beach', -33.890542, 151.274856, 1],
                            ['Coogee Beach', -33.923036, 151.259052, 2]
            ];
            this.CargarMapa();
        },
        VisualizarValor: function (eval) {
            var dato = eval.target.id.split('_')[1];
            var valor = eval.target.value;
            $('#span_' + dato).text(valor + '%');
        },
        CargarMapa: function (eval) {
            axios.post('/PropuestaPublicitaria/ListaMedios_X_Local/').then(function (response) {
                this.ListaLocal = response.data.listLocal;
                var FiltroMedios = [];
                var _ListaMedios = this.ListaMedios;
                var contadormedios = 0;
                $('.ContenedorMedios input').each(function (key, value) {
                    var Campos = [this.valueAsNumber, value.id.split('_')[1]];
                    $.each(_ListaMedios, function (key2, value2) {
                        if (value2.codMedioComunicacion == value.id.split('_')[1]) {
                            _ListaMedios[key2].promedio = value.valueAsNumber;
                        }
                    });
                    contadormedios += value.valueAsNumber;
                    FiltroMedios.push(Campos);
                });
                if (contadormedios == 0) {
                    alert("La búsqueda no se puede realizar por falta de valores en los filtros");
                    return;
                }
                this.ListaMedios = _ListaMedios;
                var cod_ini = 0;
                var datos = [];
                $.each(response.data.listLocal, function (key, value) {
                    if (value.id != cod_ini) {
                        var list = [value.nombre, parseFloat(value.latitud), parseFloat(value.longitud), value.id];
                        datos.push(list);
                    }
                    cod_ini = value.id;
                });

                this.locales = datos;

                var uluru = { lat: -12.04418919671956, lng: -77.0514965057373 };
                var map = new google.maps.Map(document.getElementById('googleMap'), {
                    zoom: 13,
                    center: uluru
                });

                var shape = {
                    coords: [1, 1, 1, 20, 18, 20, 18, 1],
                    type: 'poly'
                };

                var _ContadorLocales = 0;

                for (var i = 0; i < this.locales.length; i++) {
                    var beach = this.locales[i];

                    var tbcuerpo = '';
                    var tbLocal = '';
                    var contador = 0;
                    $.each(response.data.listLocal, function (key, value) {
                        if (value.id == beach[3]) {
                            var sumpuntajeCaracteristicaCombo = 0;
                            $.each(response.data.listLocal, function (key2, value2) {
                                if (value2.id == beach[3]) {
                                    $.each(FiltroMedios, function (k, v) {
                                        if ((v[1] == value2.codMedioComunicacion) && (value2.Porcentaje >= v[0])) {
                                            sumpuntajeCaracteristicaCombo += value2.puntajeCaracteristicaCombo;
                                        }
                                    });                                    
                                }                                
                            });
                            $.each(FiltroMedios, function (key3, value3) {
                                if ((value3[1] == value.codMedioComunicacion) && (value.Porcentaje >= value3[0])) {
                                    tbLocal = value.nombre;
                                    tbcuerpo += '<tr>' +
                                               '<td>' + value.nombreMedioComunicacion + '</td>' +
                                               '<td>' + value.Porcentaje + '%</td>' +
                                               '<td>' + ((value.puntajeCaracteristicaCombo * 100) / sumpuntajeCaracteristicaCombo).toFixed(2) + '%</td>' +
                                               '</tr>';
                                    contador++;
                                }
                            });
                        }
                    });
                    if (contador != 0) {
                        _ContadorLocales++;
                        window["marker" + beach[3]] = new google.maps.Marker({
                            position: { lat: beach[1], lng: beach[2] },
                            map: map,
                            shape: shape,
                            title: beach[0],
                            zIndex: beach[3]
                        });

                        contentString = '<div id="content">' +
                          '<div id="siteNotice">' +
                          '</div>' +
                          '<h2 id="firstHeading" class="firstHeading"> ' + tbLocal + '</h2>' +
                          '<div id="bodyContent">' +
                          '<table class="table table-sm">' +
                          '<thead >' +
                          '<tr>' +
                          '<td>Medio</td>' +
                          '<td>% Global</td>' +
                          '<td>% Local</td>' +
                          '</tr>' +
                          '</thead>' +
                          '<tbody>' +
                          tbcuerpo +
                          '</tbody>' +
                          '</table>' +
                          '</div>' +
                          '</div>';

                        window["infowindow" + beach[3]] = new google.maps.InfoWindow({
                            content: contentString
                        });
                        window["marker" + beach[3]].addListener('click', function () {
                            var indice = this.zIndex;
                            window["infowindow" + indice].open(map, window["marker" + indice]);
                        });
                    }
                }
                this.ContadorLocales = _ContadorLocales;

            }.bind(this)).catch(function (error) {
                console.log(error);
            });



        },
        ListaMediosComunicacion: function () {
            axios.post('/PropuestaPublicitaria/ListaMediosComunicacion/').then(function (response) {
                this.ListaMedios = response.data.listResultadoEncuesta;
                this.CargarMapa();
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        RegistrarPropuesta: function () {


            if (this.ContadorLocales == 0) {
                alert("No hay locales listados en el mapa");
                return;
            }
            var contadormedios = 0;
            $('.ContenedorMedios input').each(function (key, value) {
                contadormedios += value.valueAsNumber;
            });
            if (contadormedios == 0) {
                alert("La búsqueda no se puede realizar por falta de valores en los filtros");
                return;
            }

            $("#myModal").modal("show");
            $('#observacion').val('');
            var ListaDinamica = [];
            var ListaLocal = this.ListaLocal;
            var Presupuesto = 0;
            var _ListadoDetalle = [];
            $('.ContenedorMedios input').each(function (key, value) {
                if (this.valueAsNumber > 0) {
                    var listaTrazabilidad = '';

                    $.each(ListaLocal, function (key2, value2) {
                        if ((value2.codMedioComunicacion == value.id.split('_')[1]) && (value2.Porcentaje >= value.valueAsNumber)) {
                            listaTrazabilidad += value2.nombre + '|';
                            Presupuesto += parseFloat(value.dataset.costo);
                            var parametro = {
                                codMedioComunicacion: value.id.split('_')[1],
                                codLocal: value2.id,
                                porcentaje: value2.Porcentaje,
                                promedio: value.valueAsNumber
                            }
                            _ListadoDetalle.push(parametro);
                        }
                    });
                    listaTrazabilidad = listaTrazabilidad.substring(0, listaTrazabilidad.length - 1);
                    if (listaTrazabilidad.length != 0) {
                        var ListaMed = {
                            codMedio: this.id.split('_')[1],
                            nombreMedio: value.dataset.nombre,
                            porcentaje: value.valueAsNumber,
                            locales: listaTrazabilidad
                        }
                        ListaDinamica.push(ListaMed);
                    }
                }
            });
            this.ListadoDetalle = _ListadoDetalle;
            $('#Presupuesto').val(Presupuesto);
            this.ListaMediosSeleccionados = ListaDinamica;

        },
        RegistroPropuesta: function () {
            if ($('#observacion').val().trim().length == 0) {
                alert("Se debe agregar una observación para la propuesta");
                return;
            }
            if ($('#Presupuesto').val().trim().length == 0 || parseFloat($('#Presupuesto').val()) <= 0) {
                alert("El presupuesto tiene que ser mayor a 0");
                return;
            }
            var parametro = {
                fechaPropuestapublicidad: $("#lblFecha").val(),
                precioPropuestaPublicidad: $('#Presupuesto').val(),
                ObservacionPropuestaPublicidad: $('#observacion').val()
            }
            var _ListadoDetalle = this.ListadoDetalle;
            var jsonData = { PropuestaPublicidad: parametro, DetallePropuestaPublicidad: _ListadoDetalle };
            axios.post('/PropuestaPublicitaria/RegistroPropuesta/', jsonData).then(function (response) {
                alert("Se registro la propuesta correctamente");
                $("#myModal").modal("hide");
            }.bind(this)).catch(function (error) {
                console.log(error);
            });

        }
    },
    computed: {},
    created: function () {

    },
    mounted: function () {
        this.ListaMediosComunicacion();

    },
});

$('.summernote').summernote();



var f = new Date();
var dia = ((parseInt(f.getDate()) + 1) + '').length == 1 ? "0" + (parseInt(f.getDate()) + 1) : (parseInt(f.getDate()) + 1);
var mes = (((f.getMonth() + 1) + '').length == 1 ? "0" + (f.getMonth() + 1) : (f.getMonth() + 1));
var anio = f.getFullYear();
var fechamini = anio + "-" + mes + "-" + dia;
$('#datetimepicker1').datetimepicker({
    format: 'YYYY-MM-DD',
    minDate: fechamini
});


function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    if (key >= 48 && key <= 57) {
        return true;
        //var _ID = $('#' + e.currentTarget.id).val() + e.key;
        //if (parseInt(_ID) <= 10 && parseInt(_ID) >= 0) {
        //    return true
        //} else {
        //    $('#' + e.currentTarget.id).val('');
        //    return true;
        //}
    } else {
        return false;
    }

}