google.charts.load('current', { packages: ['corechart', 'bar'] });
google.charts.setOnLoadCallback(drawGHSI);

function drawGHSI() {

    var dataGHSI = google.visualization.arrayToDataTable([
        ['Metric', 'Host', { role: 'style' }, { role: 'annotation' }],
        ['Prevention', parseInt(sessionStorage.prevention), '#160C28', sessionStorage.prevention],
        ['Detection', parseInt(sessionStorage.detection), '#e1efe6', sessionStorage.detection],
        ['Response', parseInt(sessionStorage.response), '#160C28', sessionStorage.response],
        ['Health System', parseInt(sessionStorage.system), '#e1efe6', sessionStorage.system],
        ['Compliance', parseInt(sessionStorage.compliance), '#aeb7b3', sessionStorage.compliance],
        ['Risk Environment', parseInt(sessionStorage.environment), '#EFCB68', sessionStorage.environment]
    ]);

    var optionsGHSI = {
        backgroundColor: 'beige',
        chartArea: { backgroundColor: 'beige' },
        width: '800',
        height: '100%',
        title: 'GHSI',
        hAxis: {
            //title: 'Host Factor',
        },
        vAxis: {
            title: 'Rating (scale of 1-10)'
        }
    };

    var chartGHSI = new google.visualization.ColumnChart(
        document.getElementById('ghsi_div'));

    chartGHSI.draw(dataGHSI, optionsGHSI);
}