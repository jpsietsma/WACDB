
function confirm_delete() {
    

    if (confirm("Are you sure you want to delete?") == true)
        return true;
    else
        return false;
}

function confirm_add() {
    if (confirm("Are you sure you want to add?") == true)
        return true;
    else
        return false;
}

function confirm_custom(message) {
    if (confirm(message) == true)
        return true;
    else 
        return false;
}

function modalWinDocumentUpload(target1, target2) {
    if (window.showModalDialog) {
        window.showModalDialog("WACDocumentUpload.aspx?target1=" + target1 + "&target2=" + target2 + "","name","dialogWidth:600px;dialogHeight:300px");
    } else {
        window.open('WACDocumentUpload.aspx?target1=" + target1 + "&target2=" + target2 + "', 'name', 'height=400,width=600,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no ,modal=yes');
    }
}

function nonModalWinDocumentUpload(targetPK1, targetPK2, targetArea, targetAreaSector) {
    window.open('WACDocumentUpload.aspx?pk1=' + targetPK1 + '&pk2=' + targetPK2 + '&area=' + targetArea + '&areaSector=' + targetAreaSector + '', '', 'height=500,width=650,toolbar=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no');
}

function SetScrollEventTop() { window.scrollTo(0, 0); }