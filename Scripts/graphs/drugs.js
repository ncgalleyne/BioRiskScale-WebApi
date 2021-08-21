google.charts.load('current', { packages: ['corechart', 'bar'] });
google.charts.setOnLoadCallback(drawDrug);

function drawDrug() {

    var dataDrug = google.visualization.arrayToDataTable([
        ['Metric', 'Host', { role: 'style' }, { role: 'annotation' }],
        ['Drug Treatment', parseInt(sessionStorage.drug_treatment), '#EFCB68', sessionStorage.drug_treatment]
    ]);

    var optionsDrug = {
        backgroundColor: 'beige',
        chartArea: { backgroundColor: 'beige' },
        width: '800',
        height: '100%',
        title: 'Drug / Vaccines',
        hAxis: {
            //title: 'Host Factor',
        },
        vAxis: {
            title: 'Rating (scale of 1-10)'
        }
    };

    var chartDrug = new google.visualization.ColumnChart(
        document.getElementById('drug_div'));

    chartDrug.draw(dataDrug, optionsDrug);
}