using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinaWeb.CadastroPessoa.Dominio;

namespace CadastroPessoas.Persistencia.NHibernete.Maps
{
    public class PessoaMap : ClassMapping<Pessoa>
    {
        //Mapeamento relacional
        public PessoaMap()
        {
            //passando a tabela pq ja existe se não existisse não precisava passar
            Table("Pessoas");
            Id(pk => pk.Id, (map) => { map.Generator(Generators.Identity);});
            Property(p => p.Nome);
            Property(p => p.Idade);
            Property(p => p.Endereco);
        }
    }
}
