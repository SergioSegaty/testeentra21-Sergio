
using AvaliacaoCore.DB.Map;
using AvaliacaoCore.DB.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace AvaliacaoCore.DB
{
    public class ManipuladorSqlite : DbContext, IManipuladorBancoDeDados
    {
        private DbSet<Cadastro> Cadastros { get; set; }

        public void CriarNovo()
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@"Data Source=" + Constantes.NomeArquivoBanco);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TelefoneMap().Map(modelBuilder);
            new CadastroMap().Map(modelBuilder);

        }

        public void AdicionarCadastro(Cadastro cadastro)
        {
            cadastro.HoraCadastro = DateTime.Now;
            //cadastro.UF = 
            double? calcImpostoRenda = 0.0;
            // Calcular Imposto de Renda
            var salario = cadastro.Salario;
            if (salario < 1903.98)
            {
                cadastro.ImpostoRenda = 0;
            }
            if(salario > 1903.98)
            {
                calcImpostoRenda += (salario * 0.075);
                if (calcImpostoRenda < 142.80)
                {
                    cadastro.ImpostoRenda += calcImpostoRenda;
                }
                else
                {
                    cadastro.ImpostoRenda += 142.80;
                }
            }
            else if( salario > 2826.66)
            {
                calcImpostoRenda += (salario * 0.15);
                if (calcImpostoRenda < 354.80)
                {
                    cadastro.ImpostoRenda += calcImpostoRenda;
                }
                else
                {
                    cadastro.ImpostoRenda += 354.80;
                }
            }
            if( salario > 3751.06)
            {
                calcImpostoRenda += (salario * 0.225);
                if (calcImpostoRenda < 636.13)
                {
                    cadastro.ImpostoRenda += calcImpostoRenda;
                }
                else
                {
                    cadastro.ImpostoRenda += 636.13;
                }
            }
            if(salario > 4664.68)
            {
                calcImpostoRenda += (salario * 0.275);
                if (calcImpostoRenda < 869.36)
                {
                    cadastro.ImpostoRenda += calcImpostoRenda;
                }
                else
                {
                    cadastro.ImpostoRenda += 636.13;
                }
            }

            Cadastros.Add(cadastro);
            SaveChanges();
        }

        public IBuscaComFiltro<Cadastro> BuscarCadastrosCompletoOnde()
        {
            return new BuscaComFiltro<Cadastro>(Cadastros.Include(x => x.Telefones));
        }

        public IBuscaComFiltro<Cadastro> BuscarCadastrosOnde()
        {
            return new BuscaComFiltro<Cadastro>(Cadastros);
        }

    }
}