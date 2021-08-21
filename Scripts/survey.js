var sheet = document.createElement('style'),
    type = $('#type input'),
    resevoir = $('#resevoir input'),
    host = $('#host input'),
    prefs = ['webkit-slider-runnable-track', 'moz-range-track', 'ms-track'];

let pageNumber = 1;
let pages = ["", "", "/Survey/Transmission", "/Survey/GHSI", "/Survey/DrugVaccines", "/Survey/BioClinical", "/Survey/Psych"];

document.body.appendChild(sheet);

var getTrackStyle = function (el) {
    let selector = $('#' + el.parentNode.id + '-list');
    var curVal = el.value,
        val = (curVal - 1) * 16.666666667,
        style = '';

    // Set active label
    setActiveLabel(selector[0], el.value - 1);

    //curLabel.prevAll().addClass('selected');

    // Change background gradient
    /*for (var i = 0; i < prefs.length; i++) {
      style += '.range {background: linear-gradient(to right, #37adbf 0%, #37adbf ' + val + '%, #fff ' + val + '%, #fff 100%)}';
      style += '.range input::-' + prefs[i] + '{background: linear-gradient(to right, #37adbf 0%, #37adbf ' + val + '%, #b2b2b2 ' + val + '%, #b2b2b2 100%)}';
    }*/

    return style;
}

function setActiveLabel(list, val) {
    for (i = 0; i < list.children.length; i++) {
        list.children[i].removeAttribute('class');
    }
    list.children[val].setAttribute('class', 'active selected');
}

type.on('input', function () {
    sheet.textContent = getTrackStyle(this);
});

resevoir.on('input', function () {
    sheet.textContent = getTrackStyle(this);
});

host.on('input', function () {
    sheet.textContent = getTrackStyle(this);
});

// Change input value on label click
$('#type-list li').on('click', function () {
    var index = $(this).index();
    type.val(index + 1).trigger('input');
});

$('#resevoir-list li').on('click', function () {
    var index = $(this).index();
    resevoir.val(index + 1).trigger('input');
});

$('#host-list li').on('click', function () {
    var index = $(this).index();
    host.val(index + 1).trigger('input');
});

$('#nextBtn').on('click', function () {
    e.preventDefault();
    let typeVal = $('#type input')[0].value;
    let t = $('#type-list li')[typeVal - 1].value;
    let resVal = $('#resevoir input')[0].value;
    let res = $('#resevoir-list li')[resVal - 1].value;
    let hostVal = $('#host input')[0].value;
    let h = $('#host-list li')[hostVal - 1].value;
    alert(t + res + h);
    next();
})

function next() {
    pageNumber++;
    $('.section').load(pages[pageNumber]);
}