function fnSeparator(numb) {
    var str = numb.toString().split(".");
    str[0] = str[0].replace(/\B(?=(?:(\d\d)+(\d)(?!\d))+(?!\d))/g, ",")
    return str.join(".");
}