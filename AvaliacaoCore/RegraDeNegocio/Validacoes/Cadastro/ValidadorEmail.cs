using System;
using System.Collections.Generic;
using System.Text;
using AvaliacaoCore.DB.Model;

namespace AvaliacaoCore.RegraDeNegocio.Validacoes.Cadastro
{
    public class ValidadorEmail : IValidacao<DB.Model.Cadastro>
    {
        public ResultadoValidacao Validar(DB.Model.Cadastro model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                return Invalido();
            if (!model.Email.Contains("@"))
                return Invalido();
            if (!model.Email.Contains(".com"))
                return Invalido();
            return Valido();
            
        }

        public ResultadoValidacao Valido()
        {
            return new ResultadoValidacao
            {
                Valido = true
            };

        }


        public ResultadoValidacao Invalido()
        {
            return new ResultadoValidacao
            {
                Valido = false,
                Mensagem = "Esperado Endereço de Email"
            };
        }
    }
}