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
    public class CriarSerieViewModel : BindableBase
    {
        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnOk.RaiseCanExecuteChanged();}
        }

        public DateTime DataHora { get; set; }

        public DelegateCommand OnOk { get; private set; }

        public CriarSerieViewModel()
        {
            OnOk = new DelegateCommand(Ok, CanOk);
            DataHora = DateTime.Now;
        }

        private bool CanOk()
        {
            if (string.IsNullOrEmpty(Nome))
                return false;
            return true;
        }

        private void Ok()
        {
            var dao = new SerieDao();
            if (dao.InserirSerie(Nome, DataHora))
            {
                MessageBox.Show("Serie criada com sucesso");
                this.Publish(new FecharJanelaEvent());
            }
            else
            {
                MessageBox.Show("Ocorreu um problema ao criar a serie");
            }
        }
    }
}
