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
using BD3Trab4.Dominio;
using BD3Trab4.Events;
using BD3Trab4.Views;
using PubSub;

namespace BD3Trab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnCadastrarSeries.IsEnabled = false;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new CadastroDeCompetidores().ShowDialog();
        }

        private void FecharInscriçõesCompetidores_Click(object sender, RoutedEventArgs e)
        {
            InscriçõesDeCompetidores.Instance.InscriçõesFechadas = true;
            btnCadastrarSeries.IsEnabled = true;
        }

        private void ReabrirInscriçõesCompetidores_Click(object sender, RoutedEventArgs e)
        {
            InscriçõesDeCompetidores.Instance.InscriçõesFechadas = false;
            btnCadastrarSeries.IsEnabled = false;
        }

        private void btnCadastrarSeries_Click(object sender, RoutedEventArgs e)
        {
            new CadastrarSeries().ShowDialog();
        }

        private void btnCriarSerie_Click(object sender, RoutedEventArgs e)
        {
            new CriarSeries().ShowDialog();
        }
    }
}
