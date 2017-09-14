
using System.Data.Entity;
using TreinaWeb.CadastroPessoa.Dominio;

namespace TrenaWeb.Persistencia.EF
{
    //aqui vai gerar as tabelas do banco de dados
    public class CadastroPessoaDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
