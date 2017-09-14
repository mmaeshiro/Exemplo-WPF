using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TreinaWeb.CadastroPessoa.Dominio
{
    public class Pessoa
    {
        //NHiberneta
        public virtual int Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual int Idade { get; set; }

        public virtual string Endereco { get; set; }

        //public int Id { get; set; }

        //public string Nome { get; set; }

        //public int Idade { get; set; }

        //public string Endereco { get; set; }
    }
}
