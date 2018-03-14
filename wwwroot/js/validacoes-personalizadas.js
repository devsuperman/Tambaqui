/*
 * Localized default methods for the jQuery validation plugin.
 * Locale: PT_BR
 * http://cleytonferrari.com/validacao-de-data-e-moeda-asp-net-mvc-jquery-validation-em-portugues/
 */
jQuery.extend(jQuery.validator.methods, {
    date: function (value, element) {
        return this.optional(element) || /^\d\d?\/\d\d?\/\d\d\d?\d?$/.test(value);
    },
    number: function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
    }
});


 /*
* Translated default messages for the jQuery validation plugin.
* Locale: PT_BR
* https://gist.github.com/diegoprates/5047663
*/
jQuery.extend(jQuery.validator.messages, {
    required: "Este campo &eacute; requerido.",
    remote: "Por favor, corrija este campo.",
    email: "Por favor, forne&ccedil;a um endere&ccedil;o eletr&ocirc;nico v&aacute;lido.",
    url: "Por favor, forne&ccedil;a uma URL v&aacute;lida.",
    date: "Por favor, forne&ccedil;a uma data v&aacute;lida.",
    dateISO: "Por favor, forne&ccedil;a uma data v&aacute;lida (ISO).",
    number: "Por favor, forne&ccedil;a um n&uacute;mero v&aacute;lido.",
    digits: "Por favor, forne&ccedil;a somente d&iacute;gitos.",
    creditcard: "Por favor, forne&ccedil;a um cart&atilde;o de cr&eacute;dito v&aacute;lido.",
    equalTo: "Por favor, forne&ccedil;a o mesmo valor novamente.",
    accept: "Por favor, forne&ccedil;a um valor com uma extens&atilde;o v&aacute;lida.",
    maxlength: jQuery.validator.format("Por favor, forne&ccedil;a n&atilde;o mais que {0} caracteres."),
    minlength: jQuery.validator.format("Por favor, forne&ccedil;a ao menos {0} caracteres."),
    rangelength: jQuery.validator.format("Por favor, forne&ccedil;a um valor entre {0} e {1} caracteres de comprimento."),
    range: jQuery.validator.format("Por favor, forne&ccedil;a um valor entre {0} e {1}."),
    max: jQuery.validator.format("Por favor, forne&ccedil;a um valor menor ou igual a {0}."),
    min: jQuery.validator.format("Por favor, forne&ccedil;a um valor maior ou igual a {0}.")
});

//Validar CPF no front
// var ValidarCPF = {};

// ValidarCPF.Init = function () {
//     $.validator.addMethod('cpf', function (value, element, params) {
//         var cpf = value.replace(/[^0-9]/gi, ''); //Remove tudo que não for número
//         if (value.length == 0)
//             return true; //vazio
//         if (cpf.length != 11 || cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999")
//             return false;
//         add = 0;
//         for (i = 0; i < 9; i++)
//             add += parseInt(cpf.charAt(i)) * (10 - i);
//         rev = 11 - (add % 11);
//         if (rev == 10 || rev == 11)
//             rev = 0;
//         if (rev != parseInt(cpf.charAt(9)))
//             return false;
//         add = 0;
//         for (i = 0; i < 10; i++)
//             add += parseInt(cpf.charAt(i)) * (11 - i);
//         rev = 11 - (add % 11);
//         if (rev == 10 || rev == 11)
//             rev = 0;
//         if (rev != parseInt(cpf.charAt(10)))
//             return false;
//         return true; //cpf válido
//     }, '');
//     $.validator.unobtrusive.adapters.add('cpf', {}, function (options) {
//     	options.rules['cpf'] = true;
//     	options.messages['cpf'] = options.message;
//     });
// }

// //executa -- importante que isso seja feito antes do document.ready
// ValidarCPF.Init();