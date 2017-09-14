using System;
using System.Collections.Generic;
using TreinaWeb.CadastroPessoa.Dominio;
using TrenaWeb.Persistencia.EF;
using System.Threading;
using System.Linq.Expressions;
using System.Linq;

namespace TreinaWeb.CadastroPessoas.Repositorio
{
    public class PessoaRepositorio : IRepositorio<Pessoa>
    {
        #region NHebernite
        //private ISessionFactory _sessionFactory;

        //public PessoaRepositorio()
        //{
        //    //configuração do nhibernate
        //    Configuration config = new Configuration();

        //    config.Configure();

        //    //para mapear xml de pessoa
        //    config.AddAssembly(typeof(Pessoa).Assembly);

        //    //referencia as classes de pessoa
        //    HbmMapping mapping = CreateMappings();

        //    config.AddDeserializedMapping(mapping, null);

        //    //aqui que abre a conexão com o banco de dados
        //    _sessionFactory = config.BuildSessionFactory();
        //}

        //private HbmMapping CreateMappings()
        //{
        //    //mapeamento de modelo
        //    ModelMapper mapper = new ModelMapper();

        //    mapper.AddMapping(typeof(PessoaMap));

        //    return mapper.CompileMappingForAllExplicitlyAddedEntities();
        //}
        #endregion

        public int Adicionar(Pessoa objeto)
        {
            ////NHibernate=====================
            //using (ISession sessao = _sessionFactory.OpenSession())
            //{
            //    using (var Transacao = sessao.BeginTransaction())
            //    {
            //        sessao.Save(objeto);

            //        Transacao.Commit();

            //        return 0;
            //    }
            //}

            //ENTITY=========================
            CadastroPessoaDbContext _db = new CadastroPessoaDbContext();

            _db.Pessoas.Add(objeto);

            //salva no banco
            return _db.SaveChanges();

        }

        //Metodo Async
        public async void AdicionarAsync(Pessoa objeto, Action<int> callback)
        {
         
            //ENTITY async=========================
            CadastroPessoaDbContext _db = new CadastroPessoaDbContext();

            _db.Pessoas.Add(objeto);

            //simula a demora
            Thread.Sleep(3000);

            //salva no banco
            await _db.SaveChangesAsync().ContinueWith((taskAnterior) =>
            {
                int linhasAfetadas = taskAnterior.Result;
                callback(linhasAfetadas);
            });

        }

        //PLINQ
        public List<Pessoa> Selecionar(Func<Pessoa, bool> whereClause)
        {
            CadastroPessoaDbContext _db = new CadastroPessoaDbContext();

            List<Pessoa> Pessoas = _db.Pessoas.AsParallel().Where(whereClause).ToList();

            //fecha a coneção
            _db.Dispose();

            return Pessoas;
        }

        public List<Pessoa> SelecionarTodos()
        {
            ////NHibernate============================
            //using (ISession sessao = _sessionFactory.OpenSession())
            //{
            //    IQuery consulta = sessao.CreateQuery("FROM Pessoa");

            //    return consulta.List<Pessoa>().ToList();
            //}

            //ENTITY ==========================================
            CadastroPessoaDbContext _db = new CadastroPessoaDbContext();

            List<Pessoa> Pessoas = _db.Pessoas.OrderBy(x => x.Nome).ToList();

            //fecha a coneção
            _db.Dispose();

            return Pessoas;
        }
    }
}
