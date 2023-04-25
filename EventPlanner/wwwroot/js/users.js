function Validate() {
    if ($("#CompanyNameInput").val().trim().length > 0) {
        document.getElementById("CompanyNameValidation").innerHTML = "";
        
    }
    else {
        document.getElementById("CompanyNameValidation").innerHTML = "El nombre de la empresa es requerido";
    }
}

//oculto por defecto
element = document.getElementById("CompanyName");
element.style.display = 'none';    

function valueChanged() {
    if ($("#IsCompany").is(":checked"))
        $("#CompanyName").show();
    else
        $("#CompanyName").hide();
}