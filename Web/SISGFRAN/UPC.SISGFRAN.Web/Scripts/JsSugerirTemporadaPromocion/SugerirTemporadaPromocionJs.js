new Vue({
    el: "#app",
    data: {
        combos: {}
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
            google.charts.setOnLoadCallback(drawChart("columnchart_material"));
        },
        ImprimirPantalla: function () {

            google.charts.setOnLoadCallback(drawChart("columnchart_material1"));

            //var objeto = document.getElementById('columnchart_material'); 
            //var ventana = window.open('', '_blank');  
            //ventana.document.write(objeto.innerHTML);
            //ventana.document.close(); 
            //ventana.print();  
            //ventana.close();  
        },
    },
    computed: {},
    created: function () { },
    mounted: function () {
        this.ListaCombo();
    }
});

google.charts.load('current', { 'packages': ['bar'] });
function drawChart(_data) {

    //var fffff = JSON.parse(_data);

    var data = google.visualization.arrayToDataTable([
          ['Year', 'Sales', 'Expenses', 'Profit'],
          ['2014', 1000, 400, 200],
          ['2015', 1170, 460, 250],
          ['2016', 660, 1120, 300],
          ['2017', 1030, 540, 350]
    ]);

    var options = {
        chart: {
            title: 'Company Performance',
            subtitle: 'Sales, Expenses, and Profit: 2014-2017',
        }
    };
    var chart = new google.charts.Bar(document.getElementById(_data));

    chart.draw(data, google.charts.Bar.convertOptions(options));


}