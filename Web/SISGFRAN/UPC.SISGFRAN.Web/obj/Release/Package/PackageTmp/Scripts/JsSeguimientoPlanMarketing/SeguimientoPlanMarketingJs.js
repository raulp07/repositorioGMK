new Vue({
    el: "#app",
    data: {
        listaplanmarketing: [],
        ListaObjetivos: [],
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
    },
    computed: {},
    created: function () { },
    mounted: function () {
        this.ListaPlanMarketing();
    }
});


