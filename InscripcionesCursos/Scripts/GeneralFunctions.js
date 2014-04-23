function clearTextbox(a)
{
    document.getElementById("inputText").value == a && (document.getElementById("inputText").value = "");
    document.getElementById("inputText").setAttribute("class","");
}