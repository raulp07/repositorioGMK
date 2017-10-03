new Vue({
    el: "#app",
    data: {
        listaplanmarketing: [],
        ListaObjetivos: [],
        ListaEstrategia: [],
        ListaAccion: [],
        descipcionaccion: 'sd',
        observacionaccion: 'dsa',
        codAccion: 0,
        codEstrategia: 0,
        codObjetivo: 0,
        idPlanMkt:0,
    },
    methods: {
        ListaPlanMarketing: function () {
            axios.post("/PropuestaIndicador/ListaPlanMarketing/").then(function (response) {
                this.listaplanmarketing = response.data.PlanMarketing;
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        DarSeguimiento: function () {

            var param = {};
            var titulo = '';
            var contador = 0;
            var _idPlanMkt = 0;
            $('#tbodyid input').each(function (i, v) {
                if ($(this).is(":checked")) {
                    titulo = $(this).parents('td').siblings('td').text();
                    param = { "idPlanMkt": $(this).attr("data-pmkt") };
                    _idPlanMkt = $(this).attr("data-pmkt");
                    contador++;
                }
            });
            if (contador==0) {
                alert("Debe seleccionar un plan de Marketing");
                return;
            }
            this.idPlanMkt = _idPlanMkt;
            var Json = { DE: param };
            $('#titulo').text(titulo);
            axios.post("/SeguimientoPlanMarketing/ListaObjetivo/", Json).then(function (response) {
                this.ListaObjetivos = response.data.ListObjetivo;
                $('#Pag1').hide(500);
                $('#Pag2').show();
                $('.nav-tabs a[href="#Objetivos"]').tab('show');
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        Lista_Objetivos: function (val) {
            var param = {
                idPlanMkt: val,
            };
            var Json = { DE: param };
            axios.post("/SeguimientoPlanMarketing/ListaObjetivo/", Json).then(function (response) {
                this.ListaObjetivos = response.data.ListObjetivo;
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        Retr: function () {

            this.ListaPlanMarketing();
            this.idPlanMkt = [];
            this.ListaObjetivos = [];
            this.ListaEstrategia = [];
            this.ListaAccion = [];
            $('#Pag1').show();
            $('#Pag2').hide(500);
        },
        SaveSegui: function () {
        },
        CargaEstrategia: function (val) {
            var param = {
                codObjetivo: val,
            };
            this.codObjetivo = val;
            var Json = { DE: param };
            //this.codEstrategia = 0;
            axios.post("/SeguimientoPlanMarketing/ListaEstrategia/", Json).then(function (response) {
                this.ListaEstrategia = response.data.ListEstrategia;
                $('.nav-tabs a[href="#Estrategia"]').tab('show');
            }.bind(this)).catch(function (error) {
                console.log(error);
            });

        },
        Lista_Estrategia: function(val){
            var param = {
                codObjetivo: val,
            };
            var Json = { DE: param };
            //this.codEstrategia = 0;
            axios.post("/SeguimientoPlanMarketing/ListaEstrategia/", Json).then(function (response) {
                this.ListaEstrategia = response.data.ListEstrategia;
                this.Lista_Objetivos(this.idPlanMkt);
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        CargaAccion: function (val) {
            var param = {
                codEstrategia: val,
            };
            this.codAccion = 0;
            this.codEstrategia = val;
            var Json = { DE: param };
            axios.post("/SeguimientoPlanMarketing/ListaAccion/", Json).then(function (response) {
                this.ListaAccion = response.data.ListAccion;
                $('.nav-tabs a[href="#Acciones"]').tab('show');
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
        VerDetallerAccion: function (codAccion) {
            this.codAccion = codAccion;
            var _ListaAccion = this.ListaAccion;

            $.each(_ListaAccion, function (k, v) {
                if (v.codAccion == codAccion) {
                    $('#txtdescipcion').val(v.nombreAccion);
                    $('#ddlestado').val(v.estadoAccion);
                    $('#txtobservacion').val(v.descripcionAccion);
                    $('#txtcostoAccion').val(v.costoAccion);

                }
            });
        },
        ActualizarAccion: function () {
            if (this.codAccion == 0) {
                alert("Seleccione una Acción");
                return;
            }
            if (this.codAccion != 0) {

                if ($('#ddlestado').val() == 0) {
                    alert("Seleccione un estado para la Acción");
                    return;
                }
                if ($('#txtobservacion').val().trim() == "") {
                    alert("Ingrese una observación para la Acción");
                    return;
                }
                if ($('#txtcostoAccion').val().trim() == "") {
                    alert("Ingrese un costo para la Acción");
                    return;
                }
                if ($('#txtcostoAccion').val()<1) {
                    alert("El costo tiene que ser mayor a 0");
                    return;
                }

                var param = {
                    codAccion: this.codAccion,
                    nombreAccion: $('#txtdescipcion').val(),
                    codEstrategia: this.codEstrategia,
                    descripcionAccion: $('#txtobservacion').val(),
                    costoAccion: $('#txtcostoAccion').val(),
                    estadoAccion: $('#ddlestado').val(),
                }
                var Json = { DE: param };
                axios.post("/SeguimientoPlanMarketing/ActualizarAccion/", Json).then(function (response) {
                    if (response.data.ListAccion.length > 0) {
                        this.ListaAccion = response.data.ListAccion;
                        alert("Se Actualizo correctamente la Acción");
                        this.Lista_Estrategia(this.codObjetivo);
                        this.codAccion = 0;
                        $('#ddlestado').val(0);
                        $('#txtobservacion').val('');
                        $('#txtcostoAccion').val('');
                        $('#txtdescipcion').val('');
                    }
                }.bind(this)).catch(function (error) {
                    console.log(error);
                });
            }
        }
    },
    computed: {},
    created: function () { },
    mounted: function () {
        this.ListaPlanMarketing();
    }
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