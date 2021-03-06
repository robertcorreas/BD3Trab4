﻿using System;
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
using BD3Trab4.Events;
using PubSub;

namespace BD3Trab4.Views
{
    /// <summary>
    /// Interaction logic for CadastroDeCompetidores.xaml
    /// </summary>
    public partial class CadastroDeCompetidores : Window
    {
        public CadastroDeCompetidores()
        {
            InitializeComponent();
            DataContext = new CadastroDeCompetidoresViewModel();
            this.Subscribe<FecharJanelaEvent>(@event =>
            {
                this.Close();
                this.Unsubscribe<FecharJanelaEvent>();
            });
        }
    }
}
