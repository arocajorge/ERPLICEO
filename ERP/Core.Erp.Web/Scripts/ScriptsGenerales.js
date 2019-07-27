function OpenInNewTabWinBrowser(url) {
        
    var win = window.open(url,'_blank');
    win.focus();
}

function GetPathServer() {
    //var location = window.location.origin + "/FixedERP"; /*produccion*/
    var location = window.location.origin; /*local*/

    return location;
}