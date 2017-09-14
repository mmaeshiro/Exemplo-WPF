using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreinaWeb.CadastroPessoa.Dominio;
using TreinaWeb.CadastroPessoas.Repositorio;

namespace TreinaWeb.CadastroPessoas
{
    public partial class FrmPesssoas : Form
    {
        private List<Pessoa> _pessoas = new List<Pessoa>();

        private static readonly object locker = new Object();

        public FrmPesssoas()
        {
            InitializeComponent();
        }

        private void FrmPessoas_Load(object sender, EventArgs e)
        {
            PrencherDataGridViewAsync();

            txtPesquisa.Text = string.Empty;

            #region Threads
            ////Threads
            //Thread thread = new Thread(PreencherDataGridView);
            //thread.Start();
            #endregion

            #region TASK Com aWaiter
            ////Task
            //Task minhaTask = Task.Run(() => {
            //    Thread.Sleep(5000);
            //    IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();

            //    #region Populando o grid pelo metodo Invoke
            //    //dataGridView1.Invoke((MethodInvoker)delegate
            //    //{
            //    //    dataGridView1.DataSource = repositorioPessoas.SelecionarTodos();
            //    //    dataGridView1.Refresh();
            //    //});
            //    #endregion

            //    _pessoas = repositorioPessoas.SelecionarTodos();

            //});

            ////metodo que vai ser invocado para carregar o grid apos minhaTask carregar a lista de pessoas
            //var awaiter = minhaTask.GetAwaiter();
            //awaiter.OnCompleted(() =>
            //{
            //    dataGridView1.DataSource = _pessoas;
            //    dataGridView1.Refresh();
            //});
            #endregion

            #region TASK com continueWith
            ////Task
            //Task.Run(() =>
            //{
            //    Thread.Sleep(5000);
            //    IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();

            //    #region Populando o grid pelo metodo Invoke
            //    //dataGridView1.Invoke((MethodInvoker)delegate
            //    //{
            //    //    dataGridView1.DataSource = repositorioPessoas.SelecionarTodos();
            //    //    dataGridView1.Refresh();
            //    //});
            //    #endregion

            //    _pessoas = repositorioPessoas.SelecionarTodos();

            //}).ContinueWith((taskAnterior) =>
            //{
            //    dataGridView1.Invoke((MethodInvoker)delegate
            //    {
            //        dataGridView1.DataSource = _pessoas;
            //        dataGridView1.Refresh();
            //    });
            //});
            #endregion

            #region TASK com retorno
            //try
            //{
            //    //Teste exeção
            //    throw new Exception("Teste exeção");
            //    Task<int>.Run(() =>
            //    {
            //        Thread.Sleep(5000);
            //        IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();

            //        #region Populando o grid pelo metodo Invoke
            //        //dataGridView1.Invoke((MethodInvoker)delegate
            //        //{
            //        //    dataGridView1.DataSource = repositorioPessoas.SelecionarTodos();
            //        //    dataGridView1.Refresh();
            //        //});
            //        #endregion

            //        _pessoas = repositorioPessoas.SelecionarTodos();

            //        return _pessoas.Count;
            //    }).ContinueWith((taskAnterior) =>
            //    {
            //        try
            //        {
            //            dataGridView1.Invoke((MethodInvoker)delegate
            //            {
            //                dataGridView1.DataSource = _pessoas;
            //                dataGridView1.Refresh();
            //            });
            //            //para recuperar o retorno da task que  esta na taskAnterior
            //            MessageBox.Show(string.Format("Há {0} registros.", taskAnterior.Result));

            //        }
            //        catch (AggregateException ex)
            //        {
            //            foreach (Exception excecao in ex.InnerExceptions)
            //            {
            //                MessageBox.Show(excecao.Message);
            //            }
            //        }
            //    });
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            #endregion
        }

        private async void PrencherDataGridViewAsync()
        {                          //await para aguarda carregar
            int quantidadeLinhas = await PreencherDataGridView();

            MessageBox.Show(string.Format("Há {0} resgistros.", quantidadeLinhas));

            dataGridView1.Invoke((MethodInvoker)delegate
            {
                dataGridView1.DataSource = _pessoas;
                dataGridView1.Refresh();
            });
        }

        private Task<int> PreencherDataGridView()
        {
            #region Sem Threads
            //IRepositorio<Pessoa> repositorioPessoa = new PessoaRepositorio();

            //List<Pessoa> pessoas = repositorioPessoa.SelecionarTodos();

            //dataGridView1.DataSource = pessoas;

            ////para atualizar o grid
            //dataGridView1.Refresh();
            #endregion

            #region Preenchendo grid com Threads
            ////Threads
            ////tempo de para thread começar
            //Thread.Sleep(5000);

            //Thread thread = new Thread(PreencherListaPessoas);
            //Thread thread2 = new Thread(PreencherListaPessoas2);
            //thread.Start();
            //thread2.Start();
            //thread.Join();
            //thread2.Join();
            //dataGridView1.Invoke((MethodInvoker)delegate { dataGridView1.DataSource = _pessoas; dataGridView1.Refresh(); });
            #endregion

            #region TASK com retorno PLINQ                     
            return Task<int>.Run(() =>
            {
                Thread.Sleep(5000);
                IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();

                _pessoas = repositorioPessoas.SelecionarTodos();

                List<Pessoa> temp = repositorioPessoas.SelecionarTodos();

                //plinq forEach
                Parallel.ForEach(temp, (pessoa) =>
                 {
                     pessoa.Nome += " - Paralelizado";

                     _pessoas.Add(pessoa);
                 });

                return _pessoas.Count;
            });
            #endregion
        }

        #region Preencher pessoa com TASK E NORMAL
        //private Task<int> PreencherDataGridView()
        //{
        //    #region Sem Threads
        //    //IRepositorio<Pessoa> repositorioPessoa = new PessoaRepositorio();

        //    //List<Pessoa> pessoas = repositorioPessoa.SelecionarTodos();

        //    //dataGridView1.DataSource = pessoas;

        //    ////para atualizar o grid
        //    //dataGridView1.Refresh();
        //    #endregion

        //    #region Preenchendo grid com Threads
        //    ////Threads
        //    ////tempo de para thread começar
        //    //Thread.Sleep(5000);

        //    //Thread thread = new Thread(PreencherListaPessoas);
        //    //Thread thread2 = new Thread(PreencherListaPessoas2);
        //    //thread.Start();
        //    //thread2.Start();
        //    //thread.Join();
        //    //thread2.Join();
        //    //dataGridView1.Invoke((MethodInvoker)delegate { dataGridView1.DataSource = _pessoas; dataGridView1.Refresh(); });
        //    #endregion

        //    #region TASK com retorno                      
        //    return Task<int>.Run(() =>
        //    {
        //        Thread.Sleep(5000);
        //        IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();                

        //        _pessoas = repositorioPessoas.SelecionarTodos();

        //        return _pessoas.Count;
        //    });
        //    #endregion
        //}
        #endregion

        private void PreencherListaPessoas()
        {
            try
            {
                throw new Exception("teste exeção threads");
                IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();
                List<Pessoa> pessoas = repositorioPessoas.SelecionarTodos();

                lock (locker)
                {
                    foreach (var p in pessoas)
                    {
                        p.Nome += "Thread 1";
                        _pessoas.Add(p);
                        Thread.Sleep(300);
                    }
                }

            }
            catch (Exception ex)
            {
                ExibirErro(ex);
            }

        }

        private void PreencherListaPessoas2()
        {
            IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();
            List<Pessoa> pessoas = repositorioPessoas.SelecionarTodos();

            lock (locker)
            {
                foreach (var p in pessoas)
                {
                    p.Nome += "Thread 2";
                    _pessoas.Add(p);
                    Thread.Sleep(100);
                }
            }

        }

        //em threads so captura exceçoes assim
        private void ExibirErro(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        private void buttonAdicionar_Click(object sender, EventArgs e)
        {
            FmrAdicionarPessoa frmAdicionarPessoa = new FmrAdicionarPessoa();

            //para ficar igual modal
            frmAdicionarPessoa.ShowDialog();

            PrencherDataGridViewAsync();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            IRepositorio<Pessoa> RepositoriosPessoas = new PessoaRepositorio();
            //PLINQ
            dataGridView1.DataSource = RepositoriosPessoas.Selecionar(p => p.Nome.Contains(txtPesquisa.Text));

            dataGridView1.Refresh();
        }
    }
}
