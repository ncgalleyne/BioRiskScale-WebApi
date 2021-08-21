google.charts.load('current', { packages: [ 'bar'] });
google.charts.setOnLoadCallback(drawRhr);

function drawRhr() {

    var data = google.visualization.arrayToDataTable([
        ['Metric', 'Host', { role: 'style' }, { role: 'annotation' }],
        ['Reservoir Type', parseInt(sessionStorage.reservoir_type), '#160C28', sessionStorage.reservoir_type],
        ['Reservoir Endemicity', parseInt(sessionStorage.reservoir_endimicity), '#EFCB68', sessionStorage.reservoir_endimicity]
    ]);

    var options = {
        backgroundColor: 'beige',
        chartArea: {backgroundColor:'beige'},
        width: '800',
        height: '100%',
        title: 'Reservoir Host Region',
        hAxis: {
            title: 'Host Factor',
        },
        vAxis: {
            title: 'Rating (scale of 1-10)'
        }
    };

    var chart = new google.visualization.ColumnChart(
        document.getElementById('rhr_div'));

    chart.draw(data, options);
}