function BuscaCep() {
    $(document).ready(function () {
        function limpa_formulario_cep() {
            // Limpa valores do formul�rio de cep
            $("#Endereco_Logradouro").val("");
            $("#Endereco_Bairro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");
        }

        // Quando o campo cep perde o foco
        $("#Endereco_Cep").blur(function () {
            //Nova vari�vel "cep" somente com digitos.
            var cep = $(this).val().replace(/\D/g, '');

            // Verifica se campo cep possui valor informado.
            if (cep != "") {
                var validaCep = /^[0-9]{8}$/;

                // Valida o formado do CEP
                if (validaCep.test(cep)) {
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Bairro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");

                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {
                            if (!("erro" in dados)) {
                                // Atualiza os campos com os valores da consulta
                                $("#Endereco_Logradouro").val(dados.logradouro);
                                $("#Endereco_Bairro").val(dados.bairro);
                                $("#Endereco_Cidade").val(dados.localidade);
                                $("#Endereco_Estado").val(dados.uf);
                            } // end if
                            else {
                                // CEP pesquisado n�o foi encontrado
                                limpa_formulario_cep();
                                alert("CEP n�o encontrado.")
                            }
                        }
                    );
                }
                else {
                    // cep � inv�lido
                    limpa_formulario_cep();
                    alert("Formato de CEP inv�lido.")
                }
            }
            else {
                // cep sem valor, limpa formul�rio
                limpa_formulario_cep();
            }
        })
    })
}