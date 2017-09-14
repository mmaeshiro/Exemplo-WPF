using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TreinaWeb.CadastroPessoa.Dominio;
using TreinaWeb.CadastroPessoas.Repositorio;

namespace TreinaWeb.CadastroPessoas.WPF
{
    /// <summary>
    /// Interaction logic for WindowCadastraPessoa.xaml
    /// </summary>
    public partial class WindowCadastraPessoa : Window
    {
        public WindowCadastraPessoa()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            IRepositorio<Pessoa> repositorioPessoa = new PessoaRepositorio();

            Pessoa pessoa = new Pessoa
            {
                Nome = textBoxNome1.Text,
                Idade = Convert.ToInt32(textBoxIdade1.Text),
                Endereco = textBoxEndereco1.Text            
            };

            repositorioPessoa.Adicionar(pessoa);
            Close();
        }
    }
}
