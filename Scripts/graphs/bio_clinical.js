google.charts.load('current', { packages: ['corechart', 'bar'] });
google.charts.setOnLoadCallback(drawBio);

function drawBio() {

    var dataBio = google.visualization.arrayToDataTable([
        ['Metric', 'Host', { role: 'style' }, { role: 'annotation' }],
        ['Generation Time', parseInt(sessionStorage.generation), '#EFCB68', sessionStorage.generation],
        ['Immunity Duration', parseInt(sessionStorage.immunity), '#160c28', sessionStorage.immunity],
        ['CFR', parseInt(sessionStorage.cfr), '#e1efe6', sessionStorage.cfr],
        ['IFR', parseInt(sessionStorage.ifr), '#aeb7b3', sessionStorage.ifr]
    ]);

    var optionsBio = {
        backgroundColor: 'beige',
        chartArea: { backgroundColor: 'beige' },
        width: '800',
        height: '100%',
        title: 'Bio Clinical',
        hAxis: {
            //title: 'Host Factor',
        },
        vAxis: {
            title: 'Rating (scale of 1-10)'
        }
    };

    var chartBio = new google.visualization.ColumnChart(
        document.getElementById('bio_div'));

    chartBio.draw(dataBio, optionsBio);
}