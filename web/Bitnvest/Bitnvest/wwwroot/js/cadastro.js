
$(document).ready(() => {
    $.fn.datepicker.dates['en'] = {
        days: ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"],
        daysShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],
        daysMin: ["Do", "Se", "Te", "Qa", "Qi", "Se", "Sa"],
        months: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        monthsShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
        today: "Hoje",
        clear: "Limpar",
        format: "dd/mm/yyyy",
        titleFormat: "MM yyyy", /* Leverages same syntax as 'format' */
        weekStart: 0
    };

    $('.datepicker').datepicker();
    $('#cpf').mask('000.000.000-00', { reverse: true });
    $('#cel').mask('(00) 00000-0000');

    let form = $("#cadastro");
    let alertContainer = $("#alert-container");

    form.submit((e) => {
        let existeErros = ShowAlertErros();

        if (existeErros) {
            e.preventDefault();
        }
    });

    function fildsValidation() {
        let erros = [];

        let data = form.serializeArray();

        let email = data[1];
        let emailConfirm = data[2];

        let senha = data[6];
        let senhaConfirm = data[7];

        if (email.value != emailConfirm.value) {
            erros.push("O Email e a confirmação de Email estão diferentes!");
        }

        if (senha.value != senhaConfirm.value) {
            erros.push("A Senha e a confirmação de senha estão diferentes!")
        }

        return erros;
    }

    let time;

    function ShowAlertErros() {
        let erros = fildsValidation();

        $("#alert-body").html('');

        if (erros.length != 0) {
            erros.forEach((item, value) => {
                $("#alert-body").append(`<br />${item}<br />`);
            });

            if (alertContainer) {
                alertContainer.fadeIn();

                window.clearTimeout(time);
                time = setTimeout(() => alertContainer.fadeOut(), 6000);
            }

            return true;
        }

        return false;
    }
});

