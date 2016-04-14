using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BD3Trab4.DAOs;
using BD3Trab4.Dominio;
using BD3Trab4.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using PubSub;

namespace BD3Trab4.Views
{
    public class ConfigurarConnectionStringViewModel : BindableBase
    {
        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; OnPropertyChanged(() => ConnectionString);}
        }

        public ConfigurarConnectionStringViewModel()
        {
            OnOk = new DelegateCommand(Ok);
            ConnectionString = BD3Trab4.ConnectionString.Value;
        }

        private void Ok()
        {
            var backup = BD3Trab4.ConnectionString.Value;
            BD3Trab4.ConnectionString.Value = ConnectionString;

            if (new ProvaDao().TestarConexão())
            {
                this.Publish(new FecharJanelaEvent());
            }
            else
            {
                BD3Trab4.ConnectionString.Value = backup;
                ConnectionString = backup;
                MessageBox.Show("Falha na conexçao. Connection string deve estar errada");
            }
        }

        public DelegateCommand OnOk { get; private set; }
    }
}
