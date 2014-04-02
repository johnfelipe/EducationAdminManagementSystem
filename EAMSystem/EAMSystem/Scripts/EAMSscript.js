function fun(id, holder) {
    var text = document.getElementById(id).value;
    if (text == "") {
        document.getElementById(holder).style.display = "";
    }
    else {
        document.getElementById(holder).style.display = "none";
    }
}