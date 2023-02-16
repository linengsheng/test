

function ShowTip(id, width, me) {
    $("table div[id^='divTip']").hide();
    var idexp = "#" + id.toString();
    div = $(idexp);
    var position = $(me).position(); 
    if (div != null) { 
        div.css('top', position.top + 10);
        div.css('left', position.left-width-10);
        div.css('width', width);
        div.show('slow'); 
    }
}

//-------------------------------------------------------------------------

function HideTip(div,id) {
    if (div != null) {
        var idexp = "#" + id.toString(); 
        div = $(idexp);
        div.hide(); 
    }
}
