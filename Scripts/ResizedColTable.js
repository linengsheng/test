function ResizedColTable(tb) {
    var DEFAULT_COL_WIDTH = 52;
    var dragSpan = null, preWidth = 0, preX = 0, newWidth = 0;

    //改变列宽 (响应 onmousedown)
    function doMouseDown() {
        var evt = arguments.length == 0 ? event : arguments[0];
        dragSpan = evt.srcElement ? evt.srcElement : evt.target;
        preWidth = parseInt(dragSpan.firstChild.style.width);
        preX = evt.x ? evt.x : evt.pageX;
        if (dragSpan.setCapture) {
            dragSpan.setCapture();
            dragSpan.onmousemove = changeColWidth;
            dragSpan.onmouseup = changeColWidthStop;
        } else if (window.captureEvents) {
            window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);
            document.onmousemove = changeColWidth;
            document.onmouseup = changeColWidthStop;
        }
    }

    //改变列宽 (响应 onmousemove)
    function changeColWidth() {
        var evt = arguments.length == 0 ? event : arguments[0];
        var newX = evt.x ? evt.x : evt.pageX;
        newWidth = newX - preX + preWidth;
        if (newWidth < 8) newWidth = 8;

    }
    //改变列宽 (响应 onmouseup)
    function changeColWidthStop() {
        if (dragSpan.releaseCapture) {
            dragSpan.releaseCapture();
            dragSpan.onmousemove = null;
            dragSpan.onmouseup = null;
        } else if (window.captureEvents) {
            window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);
            document.onmousemove = null;
            document.onmouseup = null;
        }
        dragSpan.firstChild.style.width = newWidth;
        var i = dragSpan.parentNode.cellIndex;
        for (var j = 1; j < tb.rows.length; j++) {
            tb.rows[j].cells[i].firstChild.style.width = newWidth;
        }
        saveWidth();

    }
    //鼠标所在行高亮显示
    function doMouseOver() {
        this.className = "dataRowSelected";
    }
    //鼠标移出行
    function doMouseOut() {
        this.className = "dataRow" + (this.rowIndex % 2);
    }
    //url+table.id 保证Cookie的唯一，以区别不同页面的同名控件
    function getCookieName() {
        var i = -1;
        var url = window.location + "";
        i = url.lastIndexOf("?");
        if (i > 0) url = url.substr(0, i);
        i = url.indexOf("///");
        if (i > 0) url = url.substr(i + 3, url.length - i - 3);
        i = url.indexOf("//");
        if (i > 0) url = url.substr(i + 2, url.length - i - 2);
        i = url.indexOf("/");
        if (i > 0) url = url.substr(i, url.length - i);
        return url + "$" + tb.id;
    }
    //保存列宽到Cookie
    function saveWidth() {
        var aWidth = new Array();
        var ths = tb.rows[0].cells;
        for (var i = 0; i < ths.length; i++) {
            var sp = ths[i].firstChild;
            aWidth.push(sp.firstChild.style.width);
        }
        var date = new Date();
        date.setDate(date.getDate() + 7);
        //date.setSeconds(date.getSeconds()+7);
        CookieHelper.setCookie(getCookieName(), aWidth, date);
    }
    //从Cookie初始化列宽
    function initWidth() {
        var sWidth = CookieHelper.getValue(getCookieName());
        if (!sWidth) return;
        var aWidth = sWidth.split(",");
        var ths = tb.rows[0].cells;
        if (aWidth.length != ths.length) return;
        for (var i = 0; i < ths.length; i++) {
            var sp = ths[i].firstChild;
            sp.firstChild.style.width = aWidth[i];
        }
    }

    //初始化
    new function () {
        initWidth();
        var ths = tb.rows[0].cells;
        for (var i = 0; i < ths.length; i++) {
            var sp = ths[i].firstChild;
            if (sp.firstChild.style.width == "") sp.firstChild.style.width = DEFAULT_COL_WIDTH;
            if (sp.fixed != "true") sp.onmousedown = doMouseDown;
            else sp.style.cursor = "default";
            for (var j = 1; j < tb.rows.length; j++) {
                var tr = tb.rows[j];
                if (i == 0) {
                    tr.className = "dataRow" + (j % 2);
                    //tr.onmouseover = doMouseOver;
                    //tr.onmouseout = doMouseOut;
                }
                tr.cells[i].firstChild.style.width = sp.firstChild.style.width;
            }
        }
        saveWidth();
    }

}

//cookie操作
function CookieHelper() { };
//保存cookie
CookieHelper.setCookie = function (name, value, expires, path, domain) {
    if (!name || !value) return;
    var sCookie = name + "=" + escape(value) + ";";
    if (expires) {
        try {
            sCookie += "expires=" + expires.toGMTString() + ";";
        } catch (e) {
        }
    }
    if (path) {
        sCookie += "path=" + path + ";";
    }
    if (domain) {
        sCookie += "domain=" + domain + ";";
    }
    document.cookie = sCookie;
}
//获取cookie值
CookieHelper.getValue = function (sName) {
    var value = null;
    var aCookie = document.cookie.split("; ");
    for (var i = 0; i < aCookie.length; i++) {
        var aCrumb = aCookie[i].split("=");
        if (sName == aCrumb[0]) {
            value = unescape(aCrumb[1]);
            break;
        }
    }
    return value;
}