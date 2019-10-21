
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
            cadastro.ImpostoRenda = CalcularImposto(cadastro.Salario);

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

        public double? CalcularImposto(double? salario)
        {
            double? faixa1 = 1903.98;
            double? faixa2 = 922.67;
            double? faixa3 = 924.40;
            double? faixa4 = 913.64;

            double? impostoRenda = 0;
            double? calcImpostoRenda = 0;

            // Calcular Imposto de Renda
            
            //Primeira faixa
            if (salario < faixa1)
            {
                impostoRenda = 0;
            }
            else
            {
                salario -= faixa1;
            }

            //Segunda Faixa
            if (salario > faixa2)
            {

                calcImpostoRenda = (faixa2 * 0.075);
                if (calcImpostoRenda < 142.80)
                {
                    impostoRenda += calcImpostoRenda;
                }
                else
                {
                    impostoRenda += 142.80;
                }
                salario -= faixa2;
            }
            else if (salario > 0)
            {
                calcImpostoRenda = (salario * 0.075);
                if (calcImpostoRenda < 142.80)
                {
                    impostoRenda += calcImpostoRenda;
                }
                else
                {
                    impostoRenda += 142.80;
                }
                salario -= faixa2;
            }

            //Terceira Faixa
            if (salario > faixa3)
            {
                calcImpostoRenda = (faixa3 * 0.15);
                if (calcImpostoRenda < 354.80)
                {
                    impostoRenda += calcImpostoRenda;
                }
                else
                {
                    impostoRenda += 354.80;
                }

                salario -= faixa3;
            }
            else if (salario > 0)
            {
                calcImpostoRenda = (salario * 0.15);
                if (calcImpostoRenda < 354.80)
                {
                    impostoRenda += calcImpostoRenda;
                }
                else
                {
                    impostoRenda += 354.80;
                }
                salario -= faixa3;
            }

            //Quarta Faixa
            if (salario > faixa4)
            {
                calcImpostoRenda = (faixa4 * 0.225);
                if (calcImpostoRenda < 636.13)
                {
                    impostoRenda += calcImpostoRenda;
                }
                else
                {
                    impostoRenda += 636.13;
                }
                salario -= faixa4;
            }
            else if (salario > 0)
            {
                calcImpostoRenda = (salario * 0.225);
                if (calcImpostoRenda < 636.13)
                {
                    impostoRenda += calcImpostoRenda;
                }
                else
                {
                    impostoRenda += 636.13;
                }
                salario -= faixa4;

            }

            //Quinta Faixa
            if (salario > 913.62)
            {
                calcImpostoRenda = (salario * 0.275);
                if (calcImpostoRenda < 869.36)
                {
                    impostoRenda += calcImpostoRenda;
                }
                else
                {
                    impostoRenda += 869.36;
                }
            }
            else if (salario > 0)
            {
                calcImpostoRenda = (salario * 0.275);
                if (calcImpostoRenda < 869.36)
                {
                    impostoRenda += calcImpostoRenda;
                }
                else
                {
                    impostoRenda += 869.36;
                }
            }

          
            return impostoRenda;
        }

    }
}