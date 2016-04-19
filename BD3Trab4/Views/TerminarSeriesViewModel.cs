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
    public class TerminarSeriesViewModel : BindableBase
    {
        private Competidor _competidorSelecionado;
        private Serie _serieCorrente;

        public Serie SerieCorrente
        {
            get { return _serieCorrente; }
            set { _serieCorrente = value; OnRegistrarCommand.RaiseCanExecuteChanged(); }
        }

        public Prova ProvaCorrente { get; set; }

        public List<Competidor> Competidores { get; set; }

        public double TempoDeProva { get; set; }


        public Competidor CompetidorSelecionado
        {
            get { return _competidorSelecionado; }
            set { _competidorSelecionado = value; OnPropertyChanged(() => CompetidorSelecionado); OnRegistrarCommand.RaiseCanExecuteChanged();}
        }

        public DelegateCommand OnFecharSerieCommand { get; set; }

        public TerminarSeriesViewModel()
        {
            OnRegistrarCommand = new DelegateCommand(Registrar,CanRegistrar);
            OnFecharSerieCommand = new DelegateCommand(FecharSerie);
            var serieDao = new SerieDao();

            SerieCorrente = serieDao.GetSerieMaisCedo();

            if (SerieCorrente != null)
            {
                ProvaCorrente = SerieCorrente.Prova;
                Competidores = new CompetidorDao().GetCompetidoresBySerie(SerieCorrente.Id).ToList();
            }
        }

        private void FecharSerie()
        {
            var serieDao = new SerieDao();
            serieDao.FecharSerie(SerieCorrente.Id);
            this.Publish(new FecharJanelaEvent());

        }

        private bool CanRegistrar()
        {
            return SerieCorrente != null && CompetidorSelecionado != null;

        }

        private void Registrar()
        {
            var serieDao = new SerieDao();

            var result = serieDao.RegistrarTempo(SerieCorrente.Id, CompetidorSelecionado.Id, TempoDeProva);

            if (result)
            {
                
                MessageBox.Show("Tempo registrado com sucesso");
                this.Publish(new FecharJanelaEvent());
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao registrar o tempo");
            }
        }

        public DelegateCommand OnRegistrarCommand { get; private set; }

    }
}
