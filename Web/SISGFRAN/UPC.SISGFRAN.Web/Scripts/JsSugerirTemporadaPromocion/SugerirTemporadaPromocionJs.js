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

    //var fffff = JSON.parse(_data);
    var dataarray = [];

    dataarray.push(['Year', 'Sales', 'Expenses', 'Profit']);
    dataarray.push(['2014', 1000, 400, 200]);
    dataarray.push(['2015', 1170, 460, 250]);
    dataarray.push(['2016', 660, 1120, 300]);
    dataarray.push(['2017', 1030, 540, 350]);
    var data = google.visualization.arrayToDataTable(dataarray);

    var options = {
        chart: {
            title: 'Company Performance',
            subtitle: 'Sales, Expenses, and Profit: 2014-2017',
        }
    };
    var chart = new google.charts.Bar(document.getElementById(_data));
    chart.draw(data, google.charts.Bar.convertOptions(options));

}