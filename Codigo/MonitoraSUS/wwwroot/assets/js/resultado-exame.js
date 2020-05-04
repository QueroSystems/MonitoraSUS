// Mostra modal com mensagem de erro
$(document).ready(function () {
    document.getElementById("mensagem-retorno").click();
});

//quando o usuario der submit no exame
$('#btn-submit').on('click', function () {

    try { $("#input-cpf").unmask(); } catch (e) { submitForm(); }

    $('#modal-confirmar').modal('hide');

    var nome = $('#input-nome').val();
    var dataNasc = $('#input-data-nascimento').val();
    var cep = $('#postal_code').val();
    var rua = $('#route').val();
    var bairro = $('#sublocality_level_1').val();
    var cidade = $('#administrative_area_level_2').val();
    var estado = $('#administrative_area_level_1').val();
    var foneCelular = $('#input-celular').val();
    var dataExame = $('#input-data-exame').val();
    var dataSintomas = $('#input-data-sintomas').val();
    document.getElementById('PesquisarCpf').value = 0;

    if (!(nome === "" || dataNasc === "" || cep === "" || rua === "" || bairro === "" ||
        cidade === "" || estado === "" || foneCelular === "" || dataExame === "" || dataSintomas === "")) {
        $('#modal-espera').modal('show');
    }

});

// submete o formulário completo
function submitForm() {
    $('#modal-espera').modal('show');
    document.forms["form-exame"].submit();
}
// mostra modal de confirmaocao
function mensagemResultado() {
    var cpf = document.getElementById('input-cpf').value;
    var nome = document.getElementById('input-nome').value;
    var idVirus = document.getElementById('input-virus-bacteria').value;
    var virus = document.getElementById('input-virus-bacteria')[idVirus-1].text;

    var mensagem = "";

    $('#ok-model-form').hide();
    $('#acoes-model-form').hide();

    if (nome === "") {
        mensagem = "Informe o Nome do Paciente!.";
        $('#texto-erro').text(mensagem);
        $('#ok-model-form').show();
    } else {

        $('#cpf-paciente').text(cpf);
        $('#nome-paciente').text(nome);
        $('#resultado-paciente').text(resultadoExame());
        $('#virus-paciente').text(virus);

        $('#acoes-model-form').show();
    }
    $('#modal-confirmar').modal('show');
}

// Calcula o resultado do exame
function resultadoExame() {
    var igg = $("input[name='IgG']:checked").val();
    var igm = $("input[name='IgM']:checked").val();
    var pcr = $("input[name='Pcr']:checked").val();
    var resultado = "Indetermiando";
    if (pcr === "S" || igm === "S") {
        resultado = "Positivo";
    } else if (pcr === "I" || igm === "I") {
        resultado = "Indeterminado";
    } else if (igg === "S") {
        resultado = "Curado";
    } else if (pcr === "N" || igm === "N") {
        resultado = "Negativo";
    }

    return resultado;
}
