using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AvaliacaoCore.DB.Model;

namespace AvaliacaoCore.RegraDeNegocio.Validacoes.Cadastro
{
    public class ValidadorEmail : IValidacao<DB.Model.Cadastro>
    {
        public ResultadoValidacao Validar(DB.Model.Cadastro model)
        {
            
             String regex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                          + "@"
                           + @"((([\-\w]+\.)+[a-zA-Z]{2,8})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
            if (model.Email != null)
            {

                if (Regex.IsMatch(model.Email, regex))
                {
                    return Valido();
                }
                return Invalido();
            }
            return Invalido();


            //if (string.IsNullOrWhiteSpace(model.Email))
            //    return Invalido();
            //if (!model.Email.Contains("@"))
            //    return Invalido();
            //if (!model.Email.Contains(".com"))
            //    return Invalido();
            //return Valido();

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