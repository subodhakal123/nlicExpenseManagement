function fnSeparator(numb) {
    var str = numb.toString().split(".");
    str[0] = str[0].replace(/\B(?=(?:(\d\d)+(\d)(?!\d))+(?!\d))/g, ",")
    return str.join(".");
}

//validate textbox
function validateTextBoxesCM(txtNames, needDiv) {
    var retVal = true;
    var substr = txtNames.split(",");
    substr.forEach(function (item) {
        var txtName = item.trim();
        var Name = txtName;
        //if (Name.slice(0, 3) == "div") {
        //    Name = txtName.slice(3);
        //}
        var txtLen = $("#" + Name).val().length;
        //if (needDiv) {
        //    txtName = "div" + Name;
        //}
        $("#" + txtName).removeClass("clsRedBox");
        $("#" + txtName).parent().find('label').removeClass("clsRedTxt");
        if (txtLen == 0) {
            $("#" + txtName).addClass("clsRedBox");
            $("#" + txtName).parent().find('label').addClass("clsRedTxt");
            retVal = false;
        }
    });
    return retVal;
}

