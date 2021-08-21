google.charts.load('current', { packages: ['corechart', 'bar'] });
google.charts.setOnLoadCallback(drawTransmission);

function drawTransmission() {

    var dataTransmission = google.visualization.arrayToDataTable([
        ['Metric', 'Host', { role: 'style' }, { role: 'annotation' }],
        ['Degree of Contact', parseInt(sessionStorage.contacts), '#160C28', sessionStorage.contacts],
        ['Possible Modes', parseInt(sessionStorage.modes), '#e1efe6', sessionStorage.modes],
        ['Environmental Persistance', parseInt(sessionStorage.persistance), '#aeb7b3', sessionStorage.persistance],
        ['Infectivity Overlap', parseInt(sessionStorage.overlap), '#EFCB68', sessionStorage.overlap]
    ]);

    var optionsTransmission = {
        backgroundColor: 'beige',
        chartArea: { backgroundColor: 'beige' },
        width: '800',
        height: '100%',
        title: 'Transmission',
        hAxis: {
            //title: 'Host Factor',
        },
        vAxis: {
            title: 'Rating (scale of 1-10)'
        }
    };

    var chartTransmission = new google.visualization.ColumnChart(
        document.getElementById('transmission_div'));

    chartTransmission.draw(dataTransmission, optionsTransmission);
}