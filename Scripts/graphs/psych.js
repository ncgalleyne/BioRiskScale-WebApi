google.charts.load('current', { packages: ['corechart', 'bar'] });
google.charts.setOnLoadCallback(drawPsych);

function drawPsych() {

    var dataPsych = google.visualization.arrayToDataTable([
        ['Metric', 'Host', { role: 'style' }, { role: 'annotation' }],
        ['History', parseInt(sessionStorage.history), '#EFCB68', sessionStorage.history],
        ['Public Awareness', parseInt(sessionStorage.awareness), '#160c28', sessionStorage.awareness],
        ['Symptom Induced Fear', parseInt(sessionStorage.fear), '#e1efe6', sessionStorage.fear],
        ['Agent Relatives', parseInt(sessionStorage.agent_relatives), '#166088', sessionStorage.agent_relatives],
        ['Agent Species', parseInt(sessionStorage.agent_species), '#aeb7b3', sessionStorage.agent_species]
    ]);

    var optionsPsych = {
        backgroundColor: 'beige',
        chartArea: { backgroundColor: 'beige' },
        width: '800',
        height: '100%',
        title: 'Psych',
        hAxis: {
            //title: 'Host Factor',
        },
        vAxis: {
            title: 'Rating (scale of 1-10)'
        }
    };

    var chartPsych = new google.visualization.ColumnChart(
        document.getElementById('psych_div'));

    chartPsych.draw(dataPsych, optionsPsych);
}