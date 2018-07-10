$(document).ready(function(){
    var $cep = $('#Endereco_CEP');
    var $rua = $("#Endereco_Rua");
    var $bairro = $("#Endereco_Bairro");
    var $cidade = $("#Endereco_Cidade");
    var $estado = $("#Endereco_Estado");

    $cep.blur(function () {
        ConsultarCEP($cep, $rua, $bairro, $cidade, $estado);
    });
});