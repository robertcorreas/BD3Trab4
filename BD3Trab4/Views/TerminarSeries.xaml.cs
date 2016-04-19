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
using BD3Trab4.Dominio;
using BD3Trab4.Events;
using PubSub;

namespace BD3Trab4.Views
{
    /// <summary>
    /// Interaction logic for TerminarSeries.xaml
    /// </summary>
    public partial class TerminarSeries : Window
    {
        public Prova ProvaCorrente { get; set; }
        public Serie SerieCorente { get; set; }

        public TerminarSeries()
        {
            InitializeComponent();
            DataContext = new TerminarSeriesViewModel();
            this.Subscribe<FecharJanelaEvent>(@event =>
            {
                Close();
                this.Unsubscribe<FecharJanelaEvent>();
            });
        }
    }
}
