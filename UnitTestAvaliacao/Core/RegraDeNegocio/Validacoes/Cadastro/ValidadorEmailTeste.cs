using AvaliacaoCore.RegraDeNegocio.Validacoes.Cadastro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestAvaliacao.Core.RegraDeNegocio.Validacoes.Cadastro
{
    [TestClass]
    public class ValidadorEmailTeste
    {
        [TestMethod]
        public void dado_um_cadastro_sem_email_deve_ser_invalido()
        {

            var cadastro = new AvaliacaoCore.DB.Model.Cadastro();
            var validador = new ValidadorEmail();
            var resultado = validador.Validar(cadastro);

            Assert.IsFalse(resultado.Valido);
            StringAssert.Contains(resultado.Mensagem, "Esperado Endereço de Email");
        }

        [TestMethod]
        public void dado_um_cadastro_sem_arroba_email_deve_ser_invalido()
        {
            var cadastro = new AvaliacaoCore.DB.Model.Cadastro();
            cadastro.Email = "alguumacoisa.coisa";
            var validador = new ValidadorEmail();
            var resultado = validador.Validar(cadastro);

            Assert.IsFalse(resultado.Valido);
            StringAssert.Contains(resultado.Mensagem, "Esperado Endereço de Email");

        }


        [TestMethod]
        public void dado_um_cadastro_sem_ponto_com_email_deve_ser_invalido()
        {
            var cadastro = new AvaliacaoCore.DB.Model.Cadastro();
            cadastro.Email = "alguumacoisa@coisa";
            var validador = new ValidadorEmail();
            var resultado = validador.Validar(cadastro);

            Assert.IsFalse(resultado.Valido);
            StringAssert.Contains(resultado.Mensagem, "Esperado Endereço de Email");

        }



        [TestMethod]
        public void dado_um_cadastro_com_email_deve_ser_valido()
        {
            var cadastro = new AvaliacaoCore.DB.Model.Cadastro();
            cadastro.Email = "algumacoisa@alguma.com.br";
            var validador = new ValidadorEmail();
            var resultado = validador.Validar(cadastro);

            Assert.IsTrue(resultado.Valido);
        }



    }
}
