using AvaliacaoCore.DB.Model;
using Microsoft.EntityFrameworkCore;

namespace AvaliacaoCore.DB
{
    public static class BuscaComFiltroCadastro
    {
        public static IBuscaComFiltro<Cadastro> NomeLike(this IBuscaComFiltro<Cadastro> that, string comparacao)
        {
            return that.Propriedade(x => EF.Functions.Like(x.Nome, "%" + comparacao + "%"));
        }

        public static IBuscaComFiltro<Cadastro> CpfLike(this IBuscaComFiltro<Cadastro> that, string comparacao)
        {
            return that.Propriedade(x => EF.Functions.Like(x.CPF.ToString(), "%" + comparacao + "%"));
        }

        public static IBuscaComFiltro<Cadastro> RGLike(this IBuscaComFiltro<Cadastro> that, string comparacao)
        {
            return that.Propriedade(x => EF.Functions.Like(x.RG.ToString(), "%" + comparacao + "%"));
        } 
    }
}