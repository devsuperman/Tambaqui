$(document).ready(function(){

    CarregarFuncaoDeConfirmarSubmit();
    
    //Ativar ou Desativar registros. Elementos HTML estão em _AcoesIndex.cshtml
    $('.InverterAtivo').click(function(){
        
        var $check = $(this);
        var $form = $(this).closest('form');                
        var url = $form.prop('action');
        var data = $form.serialize();
        
        $.post(url, data)        
        .fail(function(err) { 
            $check.prop('checked', !$check.prop('checked'));
            alert('Um Erro foi encontrado. Se o erro persistir, contate o suporte técnico.');
        });       
        
    });      
   
});

function CarregarFuncaoDeConfirmarSubmit() {

    var $elementos = document.querySelectorAll('.ConfirmarSubmit');

    $elementos.forEach(element => {
        
        element.addEventListener('click', (event) => {
            
            event.preventDefault();
            var $form = event.target.closest('form');

            Swal.fire({
                title: "Tem certeza?",
                text: "A operação não poderá ser desfeita!",
                type: "warning",
                showCancelButton: true,
                confirmButtonText: 'Sim, quero continuar!',
                cancelButtonText: 'Cancelar'
            })
            .then((result) => {
                if (result.value) {
                    $form.submit();
                }
            });    
        })
    });   
}