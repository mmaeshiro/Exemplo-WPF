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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreinaWeb.CadastroPessoa.Dominio;
using TreinaWeb.CadastroPessoas.Repositorio;

namespace TreinaWeb.CadastroPessoas.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PreencherDataGrid()
        {
            IRepositorio<Pessoa> repositorioPessoa = new PessoaRepositorio();

            List<Pessoa> Pessoas = repositorioPessoa.SelecionarTodos();

            dgrPessoas.ItemsSource = Pessoas;
        }

        private void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            PreencherDataGrid();
        }

        private void btnCadastraPessoa_Click(object sender, RoutedEventArgs e)
        {
            WindowCadastraPessoa wdwCadastrarPessoa = new WindowCadastraPessoa();
            wdwCadastrarPessoa.ShowDialog();
            PreencherDataGrid();
        }
    }
}
