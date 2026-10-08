
//----------------Can enter only numbers and decimal ---------------------------\\
function checkDec(el) {
    var ex = el.value;
    if (ex.indexOf('.') > -1) {
        el.value = ex.substring(0, ex.indexOf('.') + 8);
    }
}

function onlyNumbers(event) {
    var charCode = (event.which) ? event.which : event.keyCode;

    if (charCode == 9 || charCode == 8) {
        return true;
    }
    if (charCode == 46) {
        return false;
    }
    if (charCode == 31 || charCode < 48 || charCode > 57)
        return false;

    return true;
}
function onlyDotsAndNumbers(txt, event) {

    var charCode = (event.which) ? event.which : event.keyCode;

    if (charCode == 9 || charCode == 8) {
        return true;
    }
    if (charCode == 46) {
        if (txt.value.indexOf(".") < 0)
            return true;
        return false;
    }
    if (charCode == 31 || charCode < 48 || charCode > 57)
        return false;

    return true;
}