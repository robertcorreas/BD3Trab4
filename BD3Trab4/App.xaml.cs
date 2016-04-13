using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BD3Trab4.DAOs;

namespace BD3Trab4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
           var dao = new ProvaDao();

            var provas = dao.GetProvas();

            foreach (var prova in provas)
            {
                MessageBox.Show(prova.Modalidade);
            }
        }
    }
}
