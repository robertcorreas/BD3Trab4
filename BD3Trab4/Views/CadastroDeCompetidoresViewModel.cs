using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using BD3Trab4.DAOs;
using BD3Trab4.Dominio;
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
        private ObservableCollection<Prova> _provasDisponíveis;
        private string _sexo;


        public CadastroDeCompetidoresViewModel()
        {
            ProvasEscolhidas = new ObservableCollection<Prova>();

            OnOk = new DelegateCommand(Ok, CanOk);
            OnAdd = new DelegateCommand(Add);
            OnRemove = new DelegateCommand(Remove);
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

        public string Patrocinio
        {
            get { return _patrocinio; }
            set
            {
                _patrocinio = value;
                OnOk.RaiseCanExecuteChanged();
            }
        }

        public DateTime DataNascimento { get; set; }

        public string Sexo
        {
            get { return _sexo; }
            set
            {
                _sexo = value;
                OnOk.RaiseCanExecuteChanged();
                ProvasDisponíveis = new ObservableCollection<Prova>(new ProvaDao().GetProvaBySexo(_sexo));
            }
        }

        public DelegateCommand OnOk { get; set; }
        public DelegateCommand OnAdd { get; set; }
        public DelegateCommand OnRemove { get; set; }

        public List<string> Sexos => new List<string> {"M", "F"};

        public ObservableCollection<Prova> ProvasDisponíveis
        {
            get { return _provasDisponíveis; }
            private set
            {
                _provasDisponíveis = value;
                OnPropertyChanged(() => ProvasDisponíveis);
            }
        }

        public ObservableCollection<Prova> ProvasEscolhidas { get; }

        public Prova ProvaDisponivelSelecionada { get; set; }
        public Prova ProvaEscolhidaSelecionada { get; set; }

        private void Remove()
        {
            ProvasDisponíveis.Add(ProvaEscolhidaSelecionada);
            ProvasEscolhidas.Remove(ProvaEscolhidaSelecionada);
        }

        private void Add()
        {
            ProvasEscolhidas.Add(ProvaDisponivelSelecionada);
            ProvasDisponíveis.Remove(ProvaDisponivelSelecionada);
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
            if (dao.InserirCompetidor(Nome, DataNascimento, Sexo, Patrocinio, ProvasEscolhidas))
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