using System;
using System.Windows.Forms;
using TreinaWeb.CadastroPessoa.Dominio;
using TreinaWeb.CadastroPessoas.Repositorio;

namespace TreinaWeb.CadastroPessoas
{
    public partial class FmrAdicionarPessoa : Form
    {
        public FmrAdicionarPessoa()
        {
            InitializeComponent();
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            Pessoa pessoa = new Pessoa
            {
                Nome = textBoxNome.Text,
                Idade = Convert.ToInt32(textBoxIdade.Text),
                Endereco = textBoxEndereco.Text
            };

            #region entity Normal
            ////Chamada do metodo salva normal com entity
            //IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();

            //repositorioPessoas.Adicionar(pessoa);

            //Close();
            #endregion

            //Chamada do metodo salvar do entityAsync
            IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();
            repositorioPessoas.AdicionarAsync(pessoa, (linhasAfetadas) =>
             {
                 MessageBox.Show(string.Format("Foram inseridos {0} registros", linhasAfetadas));
             });

            Close();

        }
    }
}
