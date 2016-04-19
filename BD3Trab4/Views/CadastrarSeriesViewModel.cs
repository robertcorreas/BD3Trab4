using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using BD3Trab4.DAOs;
using BD3Trab4.Dominio;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace BD3Trab4.Views
{
    public class CadastrarSeriesViewModel : BindableBase
    {
        private readonly CompetidorDao _competidorDao = new CompetidorDao();
        private IList<Competidor> _competidores;
        private Competidor _competidorSelecionado;
        private string _nomeCompetidor;
        private Prova _prova;
        private Prova _provaSelecionada;
        private int _raia;
        private Serie _serieCorrente;


        public CadastrarSeriesViewModel()
        {
            var provaDao = new ProvaDao();
            Provas = provaDao.GetProvas();


            //Prova = SerieCorrente.Prova;


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
                var serieDao = new SerieDao();
                SerieCorrente = serieDao.GetSerieParaCadastramentoDeCompetidores(_provaSelecionada.Id);
                if (SerieCorrente != null)
                    Prova = SerieCorrente.Prova;
                else
                {
                    Prova = null;
                }
            }
        }

        public Serie SerieCorrente
        {
            get { return _serieCorrente; }
            private set
            {
                _serieCorrente = value;
                OnPropertyChanged(() => SerieCorrente);
            }
        }

        public IList<Competidor> Competidores
        {
            get { return _competidores; }
            private set
            {
                _competidores = value;
                OnPropertyChanged(() => Competidores);
            }
        }


        public string NomeCompetidor
        {
            get { return _nomeCompetidor; }
            set
            {
                _nomeCompetidor = value;
                OnPropertyChanged(() => NomeCompetidor);
            }
        }

        public Prova Prova
        {
            get { return _prova; }
            set
            {
                _prova = value;

                if (_prova != null)
                {
                    Competidores = _competidorDao.GetCompetidoresByProva(_prova.Id);
                }
                else
                {
                    Competidores = new List<Competidor>();
                }


                
                OnPropertyChanged(() => Prova);
            }
        }

        public Competidor CompetidorSelecionado
        {
            get { return _competidorSelecionado; }
            set
            {
                _competidorSelecionado = value;
                OnOk.RaiseCanExecuteChanged();

                if (_competidorSelecionado == null) return;

                NomeCompetidor = _competidorSelecionado.Nome;
            }
        }

        public int Raia
        {
            get { return _raia; }
            set
            {
                _raia = value;
                OnOk.RaiseCanExecuteChanged();
            }
        }

        public DateTime DataHora { get; set; }

        public DelegateCommand OnOk { get; }

        private bool CanOk()
        {
            if (CompetidorSelecionado != null && Raia > 0 && Raia <= 8)
            {
                return true;
            }
            return false;
        }

        private void Ok()
        {
            var serieDao = new SerieDao();
            var result = serieDao.CadastrarCompetidor(SerieCorrente.Id, CompetidorSelecionado.Id, Raia);
            if (result.Item1)
            {
                MessageBox.Show("Cadastro realizado com sucesso");
            }
            else
            {
                MessageBox.Show(result.Item2);
            }
        }
    }
}