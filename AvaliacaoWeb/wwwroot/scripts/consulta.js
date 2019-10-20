class Consulta {
    static dataFormat(evt) {
        var numero = evt.target.value.replace(/\D/g, "");
        numero = numero.substr(0, 8);
        if (!numero || (numero.length === 8 && moment(numero, "DDMMYYYY").isValid())) {
            $(evt.target).removeClass("invalid");
        } else {
            $(evt.target).addClass("invalid");
        }
        var builder = [];
        if (numero.length <= 2) {
            builder.push(numero);
        } else {
            builder.push(numero.substr(0, 2));
            builder.push('/');
            if (numero.length <= 4) {
                builder.push(numero.substr(2));
            } else {
                builder.push(numero.substr(2, 2));
                builder.push('/');

                builder.push(numero.substr(4));

            }
        }
        evt.target.value = builder.join('');
    }

    static obterData(id) {
        id = "#" + id;
        if ($(id).hasClass('invalid')) return null;
        return $(id).val() || null;
    }

    static eValidoCPF(strCPF) {
        var soma;
        var resto;
        soma = 0;
        if (strCPF == "00000000000") return false;

        for (var i = 1; i <= 9; i++) soma = soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
        resto = (soma * 10) % 11;

        if ((resto == 10) || (resto == 11)) resto = 0;
        if (resto != parseInt(strCPF.substring(9, 10))) return false;

        soma = 0;
        for (var i = 1; i <= 10; i++) soma = soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
        resto = (soma * 10) % 11;

        if ((resto == 10) || (resto == 11)) resto = 0;
        return (resto == parseInt(strCPF.substring(10, 11)));
    }

    static cpfFormat(evt) {
        var numero = evt.target.value.replace(/\D/g, "");
        numero = numero.substr(0, 11);
        //if ((numero.length != 11 || !Consulta.eValidoCPF(numero)) & numero.length != 0) {
        //    $(evt.target).addClass("invalid");
        //} else {
        //    $(evt.target).removeClass("invalid");
        //}
        var builder = [];
        if (numero.length <= 3) {
            builder.push(numero);
        } else {
            builder.push(numero.substr(0, 3));
            builder.push('.');
            if (numero.length <= 6) {
                builder.push(numero.substr(3));
            } else {
                builder.push(numero.substr(3, 3));
                builder.push('.');
                if (numero.length <= 9) {
                    builder.push(numero.substr(6));
                } else {
                    builder.push(numero.substr(6, 3));
                    builder.push('-');
                    builder.push(numero.substr(9));
                }
            }
        }
        evt.target.value = builder.join('');
    }

    static rgFormat(evt) {
        var numeroRG = evt.target.value.replace(/\D/g, "");
        numeroRG = numeroRG.substr(0, 9);
        var builder = [];
        if (numeroRG.length <= 2) {
            builder.push(numeroRG)
        } else {
            builder.push(numeroRG.substr(0, 2));
            builder.push('.');
            if (numeroRG.length <= 5) {
                builder.push(numeroRG.substr(2));
            } else {
                builder.push(numeroRG.substr(2, 3));
                builder.push('.');
                if (numeroRG.length <= 8) {
                    builder.push(numeroRG.substr(5));
                } else {
                    builder.push(numeroRG.substr(5, 3));
                    if (numeroRG.length === 9) {
                        builder.push('-');
                        builder.push(numeroRG.substr(8));
                    }
                }
            }
        }
        evt.target.value = builder.join('');
    }



    static obterRG() {
        var $campoRG = $("#rgCliente").val();
        debugger;
        var $rg = "";
        if ($campoRG = "undefined") {
            $rg = "";
        } else {

            $rg = $("#rgCliente").val().replace(/\D/g, "");
        }

        return $rg

    };


    static pesquisar(pagina) {
        var pesquisa = {
            Pagina: pagina || 1,
            NascimentoDe: Consulta.obterData("dataNasceDe"),
            NascimentoAte: Consulta.obterData("dataNasceAte"),
            CadastroDe: Consulta.obterData("dataCadDe"),
            CadastroAte: Consulta.obterData("dataCadAte"),
            Nome: $("#nomeCliente").val(),
            Cpf: $("#cpfCliente").val().replace(/\D/g, ""),
            Rg: Consulta.obterRG()
        }
        $('table tbody tr').remove();
        var load = (tb) => {
            var row = $('<tr>');
            var col = $('<td>');
            col.prop("colspan", 10);
            col.append('Carregando...');
            row.append(col);
            tb.append(row);
        };
        load($('table tbody'));

        var carregaLinha = (c, tb, bRG) => {
            var row = $('<tr>');
            const carregaColuna = (v, r) => {
                var col = $('<td>');
                col.append(v);
                r.append(col);
            };
            const criarTelefones = (v, r) => {
                var col = $('<td>');
                if (v.length == 0) {
                    col.append('-');
                } else if (v.length == 1) {
                    col.append(v[0]);
                } else {
                    var span = $('<span>');
                    span.text(v[0]);
                    v.shift();
                    var outros = $('<span>' + v.join('<br />') + "</span>");
                    outros.css('display', 'none');

                    var icon = $('<img>');
                    icon.prop('src', "/icons/list-icon.png");
                    icon.click(() => {
                        if (outros.css('display') === 'none') outros.css('display', 'block');
                        else outros.css('display', 'none');
                    });
                    col.append(span);
                    col.append(outros);
                    col.append(icon);
                }

                r.append(col);
            };


            let cpfFormatado = String(c.cpf);
            cpfFormatado = cpfFormatado.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
            let rgFormatado = String(c.rg);
            rgFormatado = rgFormatado.replace(/(\d{2})(\d{3})(\d{3})(\d{1})/, "$1.$2.$3-$4");


            carregaColuna(c.nome, row);
            carregaColuna(cpfFormatado, row);
            carregaColuna(moment(c.dataNascimento).format("DD/MM/YYYY"), row);
            carregaColuna(moment(c.horaCadastro).format("DD/MM/YYYY HH:mm:ss"), row);
            if (bRG)
                carregaColuna(rgFormatado, row);
            criarTelefones(c.telefones, row);
            tb.append(row);
        }





        $.ajax({
            type: "POST",
            url: "Pesquisar",
            data: JSON.stringify(pesquisa),
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",
            success: function (output) {
                Consulta.buildPaginador(pesquisa.Pagina, output.totalRegistros);
                $('table tbody tr').remove();
                var tb = $('table tbody');
                output.cadastros.forEach(c => {
                    carregaLinha(c, tb, $("#colRG").length === 1);
                });
            },
            error: function () {
                $('table tbody tr').remove();
                alert('Falha ao executar pesquisa no servidor.');
            }
        });
    }

    static buildPaginador(pagina, total) {
        const registrosPorPagina = 25;
        var div = $("#paginador");
        div.children().remove();
        var totalDePaginas = Math.ceil(total / registrosPorPagina);
        if (totalDePaginas === 1) return;
        const criarBotao = (p) => {
            var btn = $("<button>");
            btn.text(p);
            btn.click(Consulta.pesquisar.bind(Consulta, p));
            btn.addClass('btn');
            btn.addClass('btn-pagina');
            return btn;
        }
        var paginas = [1, pagina - 10, pagina - 1, pagina, pagina + 1, pagina + 10, totalDePaginas];
        paginas = paginas.filter((item, pos) => {
            return paginas.indexOf(item) === pos;
        }).filter(item => {
            return item >= 1 && item <= totalDePaginas;
        });
        paginas.forEach(p => {
            var button = criarBotao(p);
            if (p === pagina) button.prop('disabled', true);
            div.append(button);
        });
    }
}

$(document).ready(() => {
    Consulta.pesquisar(1);
})