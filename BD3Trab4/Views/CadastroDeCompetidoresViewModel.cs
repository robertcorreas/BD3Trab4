using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BD3Trab4.DAOs;
using BD3Trab4.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using PubSub;

namespace BD3Trab4.Views
{
    public class CadastroDeCompetidoresViewModel : BindableBase
    {
        private string _nome;
        private string _patrocinio;
        private string _sexo;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnOk.RaiseCanExecuteChanged(); }
        }

        public string Patrocinio
        {
            get { return _patrocinio; }
            set { _patrocinio = value; OnOk.RaiseCanExecuteChanged(); }
        }

        public DateTime DataNascimento { get; set; }

        public string Sexo
        {
            get { return _sexo; }
            set { _sexo = value; OnOk.RaiseCanExecuteChanged(); }
        }

        public DelegateCommand OnOk { get; set; }

        public List<string> Sexos => new List<string>() {"M","F"};

        public CadastroDeCompetidoresViewModel()
        {
            OnOk = new DelegateCommand(Ok, CanOk);
        }

        private bool CanOk()
        {
            if (string.IsNullOrEmpty(Nome) || string.IsNullOrEmpty(Patrocinio) || string.IsNullOrWhiteSpace(Sexo))
            {
                return false;
            }
            return true;
        }

        private void Ok()
        {
           var dao = new CompetidorDao();
            if (dao.InserirCompetidor(Nome, DataNascimento, Sexo, Patrocinio))
            {
                MessageBox.Show("Competidor Cadastrado com sucesso");
                this.Publish(new FecharJanelaEvent());
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao tentar cadastrar este competidor");
            }
        }
    }
}
