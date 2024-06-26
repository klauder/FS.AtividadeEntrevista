
$(document).ready(function () {
    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CPF": $(this).find("#CPF").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "Beneficiarios": obj.Beneficiarios,
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset(); //Limpo o formaulario para um novo cadastro.
                    
                    setTimeout(function () {
                            window.location.href = urlRetorno;
                        }, 2000);
                    
                }
        });
    });

    $('#formBeneficiario').submit(function (e) {
        e.preventDefault();
        var NOME = $('#formBeneficiario #Nome').val();
        var CPF = $('#formBeneficiario #CPF').val();
        var tabela = $('#tbBeneficiarios');
        var ID = Math.random().toString().replace('.', '');

        var existe_cpf = obj.Beneficiarios.find(x => x.CPF == CPF);

        if (!existe_cpf) {

            if (ValidarCPF(CPF)) {

                obj.Beneficiarios = obj.Beneficiarios.filter(x => x.CPF != CPF);

                obj.Beneficiarios.push({ ID: ID, NOME: NOME, CPF: CPF });

                var tr_grid = inserirLinha(ID, CPF, NOME);

                tabela.append(tr_grid);

                $('#formBeneficiario')[0].reset();

            } else {
                ModalDialog("Erro: CPF INVÁLIDO", `O CPF ${CPF} não cumpre as condições controle de dígito verificador.`);
            }

        } else {
            ModalDialog("Erro: CPF EXISTE", `O CPF: ${CPF} está associado a outro beneficiário.`);
        }
    });
        
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}


function excluir(e) {
    try {

        var idLinha = e.getAttribute('data-id');
        var CPF = e.getAttribute('data-cpf');

        //atualizar o grid beneficiários modal
        document.getElementById(idLinha).remove();

        //remove o benificiário
        obj.Beneficiarios = obj.Beneficiarios.filter(x => x.CPF != CPF);

    } catch (ex) {
        ModalDialog("Erro: Catch excluir", ex.toString())
    }
}

function alterar(e) {
    try {

        var idLinha = e.getAttribute('data-id');
        var CPF = e.getAttribute('data-cpf');
        var NOME = e.getAttribute('data-nome');

        $('#formBeneficiario #Nome').val(NOME);
        $('#formBeneficiario #CPF').val(CPF);

        //atualizar o grid beneficiários modal
        document.getElementById(idLinha).remove();

        obj.Beneficiarios = obj.Beneficiarios.filter(x => x.CPF != CPF);

    } catch (ex) {
        ModalDialog("Erro: Catch alterar", ex.toString())
    }
}
function inserirLinha(ID, CPF, NOME) {
    var html_incluir = '';

    try {

        html_incluir = `<tr id=${ID}>
                            <td width="30%">${CPF}</td>   
                            <td width="40%" >${NOME}</td>
                            <td width="30%" class="text-left">
                                <button type="button" id="btn-alterar" onclick="alterar(this)" class="btn btn-sm btn-primary" data-id=${ID} data-cpf=${CPF} data-nome="${NOME}">Alterar</button>
                                <button type="button" id="btn-excluir" onclick="excluir(this)" class="btn btn-sm btn-primary" data-id=${ID} data-cpf=${CPF}>Excluir</button>
                            </td>
                        <tr>`;

    } catch (ex) {
        ModalDialog("Erro: Catch inserirlinha", ex.toString());
    }

    return html_incluir;
}
function ValidarCPF(cpf) {
    var result = false;

    try {

        cpf = cpf.replace(/\D/g, '');
        if (cpf.toString().length != 11 || /^(\d)\1{10}$/.test(cpf)) return false;

        result = true;

        [9, 10].forEach(function (j) {
            var soma = 0, r;
            cpf.split(/(?=)/).splice(0, j).forEach(function (e, i) {
                soma += parseInt(e) * ((j + 2) - (i + 1));
            });
            r = soma % 11;
            r = (r < 2) ? 0 : 11 - r;
            if (r != cpf.substring(j, j + 1)) result = false;
        });
    } catch (ex) {
        ModalDialog("Erro: Catch ValidarCPF", ex.toString());
    }

    return result;
}
