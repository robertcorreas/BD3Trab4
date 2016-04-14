using System.Windows;
using BD3Trab4.Events;
using PubSub;

namespace BD3Trab4.Views
{
    /// <summary>
    ///     Interaction logic for CriarSeries.xaml
    /// </summary>
    public partial class CriarSeries : Window
    {
        public CriarSeries()
        {
            InitializeComponent();
            this.Subscribe<FecharJanelaEvent>(@event =>
            {
                Close();
                this.Unsubscribe<FecharJanelaEvent>();
            });

            DataContext = new CriarSerieViewModel();
        }
    }
}