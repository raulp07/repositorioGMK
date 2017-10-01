new Vue({
    el: "#app",
    data: {
        listaplanmarketing: [],
        ListaObjetivos: [],
        ListaEstrategia: [],
        ListaAccion:[],
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
            $('#tbodyid input').each(function (i, v) {
                if ($(this).is(":checked")) {
                    titulo = $(this).parents('td').siblings('td').text();
                    param = { "idPlanMkt": $(this).attr("data-pmkt") };
                }
            });
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
        Retr: function () {
            $('#Pag1').show();
            $('#Pag2').hide(500);
        },
        SaveSegui: function () {
        },
        CargaEstrategia: function (val) {            
            var param = {
                codObjetivo:val,
            };
            var Json = { DE: param };
            axios.post("/SeguimientoPlanMarketing/ListaEstrategia/", Json).then(function (response) {
                this.ListaEstrategia = response.data.ListEstrategia;
                $('.nav-tabs a[href="#Estrategia"]').tab('show');
            }.bind(this)).catch(function (error) {
                console.log(error);
            });

        },
        CargaAccion: function (val) {
            var param = {
                codEstrategia: val,
            };
            var Json = { DE: param };
            axios.post("/SeguimientoPlanMarketing/ListaAccion/", Json).then(function (response) {
                this.ListaAccion = response.data.ListAccion;
                $('.nav-tabs a[href="#Acciones"]').tab('show');
            }.bind(this)).catch(function (error) {
                console.log(error);
            });
        },
    },
    computed: {},
    created: function () { },
    mounted: function () {
        this.ListaPlanMarketing();
    }
});


