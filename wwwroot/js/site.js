
$(document).ready(function(){

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

    //Select2 Assíncrono, bom para listar munícipios!!!
    var url_buscarMunicipio = '/municipios/listar';
    $('.carregarMunicipios').select2({
        placeholder: "Selecione",
        language: "pt-BR",
        minimumInputLength: 3,
        ajax: {
            url: url_buscarMunicipio,
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

