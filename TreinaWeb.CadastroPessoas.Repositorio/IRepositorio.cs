using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TreinaWeb.CadastroPessoas.Repositorio
{
    //Repositorio Generico
    public interface IRepositorio<TTipo>
    {
        List<TTipo> SelecionarTodos();

        int Adicionar(TTipo objeto);

        //metodo async
        void AdicionarAsync(TTipo objeto, Action<int> callback);

        //Metodo PLINQ
        List<TTipo> Selecionar(Func<TTipo, bool> whereClause);
    }
}
