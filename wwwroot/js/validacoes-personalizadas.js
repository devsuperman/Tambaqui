//Adapter de CPF do Jquery Validation com o Unobtrusive 
$.validator.unobtrusive.adapters.add('cpfBR', {}, function (options) {
    options.rules['cpfBR'] = true;
    options.messages['cpfBR'] = options.message;
});

//Impede que os usuários submitem um form mais de um vez em sequência
$('form').submit(function () {
    var formValido = $(this).valid();

    if (formValido) {
        var $botoes = $(this).find('[type=submit]');

        $botoes.each(function () {
            $(this).prop('disabled', true);
            $(this).html('<i class="fa fa-circle-notch fa-spin"></i> Carregando');
        });
    }
});