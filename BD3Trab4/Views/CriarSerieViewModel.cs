using System;
using System.Collections.Generic;
using System.Windows;
using BD3Trab4.DAOs;
using BD3Trab4.Dominio;
using BD3Trab4.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using PubSub;

namespace BD3Trab4.Views
{
    public class CriarSerieViewModel : BindableBase
    {
        private string _nome;
        private Prova _provaSelecionada;


        public CriarSerieViewModel()
        {
            Provas = new ProvaDao().GetProvas();

            OnOk = new DelegateCommand(Ok, CanOk);
            DataHora = DateTime.Now;
        }

        public IList<Prova> Provas { get; private set; }

        public Prova ProvaSelecionada
        {
            get { return _provaSelecionada; }
            set
            {
                _provaSelecionada = value;
                OnOk.RaiseCanExecuteChanged();
            }
        }

        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                OnOk.RaiseCanExecuteChanged();
            }
        }

        public DateTime DataHora { get; set; }

        public DelegateCommand OnOk { get; }

        private bool CanOk()
        {
            if (string.IsNullOrEmpty(Nome) || ProvaSelecionada == null)
                return false;
            return true;
        }

        private void Ok()
        {
            var dao = new SerieDao();
            if (dao.InserirSerie(Nome, DataHora, ProvaSelecionada))
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