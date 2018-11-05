
$(document).ready(function(){

    CarregarDropdownDaSidebar();

    $('select').select2({
        language: "pt-BR"
    });
    
    $('.ConfirmarSubmit').click(function (e) {
        e.preventDefault();

        var $form = $(this).closest('form');

        swal({
            title: "Tem certeza?",
            text: "A operação não poderá ser desfeita!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((confirmado) => {
                if (confirmado) {
                    $form.submit();
                }
            });
    });

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


    //Select2 Assíncrono, bom para listar munícipios!!!
    var url_lista = '/controller/listar';
    $('.carregarListaAsync').select2({
        placeholder: "Selecione",
        language: "pt-BR",
        minimumInputLength: 3,
        ajax: {
            url: url_lista,
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    search: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data
                }
            },
            cache: true
        }
    });

});

function CarregarDropdownDaSidebar() {   

    var dropdown = document.getElementsByClassName("dropdown-btn");
    var i;
    for (i = 0; i < dropdown.length; i++) {
        
        dropdown[i].addEventListener("click", function () {            
            this.classList.toggle("active");
            var dropdownContent = this.nextElementSibling;
            if (dropdownContent.style.display === "block") {
                dropdownContent.style.display = "none";
            }
            else {
                dropdownContent.style.display = "block";
            }
        });
    }
}
