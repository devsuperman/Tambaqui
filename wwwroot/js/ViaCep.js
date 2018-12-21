
function ConsultarCEP($cep, $rua, $bairro, $cidade, $estado) {
    var cep = $cep.val().replace(/\D/g, '');
    
    var validacep = /^[0-9]{8}$/;

    if (!validacep.test(cep)) {                 
        return;
    } 
    
    var url = `https://viacep.com.br/ws/${cep}/json`;
    
    fetch(url)
        .then(a => a.json())
        .then(a => {
            $rua.val(a.logradouro);
            $bairro.val(a.bairro);
            $cidade.val(a.localidade);                
            $estado.val(a.uf);
        });
    
}