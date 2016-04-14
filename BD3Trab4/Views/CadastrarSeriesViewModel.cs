using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BD3Trab4.DAOs;
using BD3Trab4.Dominio;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace BD3Trab4.Views
{
    public class CadastrarSeriesViewModel : BindableBase
    {
        private IList<Competidor> _competidores;
        private Prova _provaSelecionada;

        public IList<Competidor> Competidores
        {
            get { return _competidores; }
            private set { _competidores = value; OnPropertyChanged(() => Competidores); }
        }

        public IList<Prova> Provas { get; private set; }

        public string NomeCompetidor
        {
            get { return _nomeCompetidor; }
            set { _nomeCompetidor = value; OnPropertyChanged(() => NomeCompetidor);}
        }

        public Prova ProvaSelecionada
        {
            get { return _provaSelecionada; }
            set
            {
                _provaSelecionada = value;

                Competidores = _competidorDao.GetCompetidoresByProva(_provaSelecionada.Id);
            }
        }

        public Competidor CompetidorSelecionado
        {
            get { return _competidorSelecionado; }
            set
            {
                _competidorSelecionado = value;
                OnOk.RaiseCanExecuteChanged();
                NomeCompetidor = _competidorSelecionado.Nome;
            }
        }

        public int Raia
        {
            get { return _raia; }
            set { _raia = value; OnOk.RaiseCanExecuteChanged();}
        }

        public DateTime DataHora { get; set; }

        public DelegateCommand OnOk { get; private set; }


        private readonly CompetidorDao _competidorDao;
        private string _nomeCompetidor;
        private Competidor _competidorSelecionado;
        private int _raia;

        public CadastrarSeriesViewModel()
        {
            var provaDao = new ProvaDao();
            Provas = provaDao.GetProvas();

            _competidorDao = new CompetidorDao();

            OnOk = new DelegateCommand(Ok, CanOk);
            DataHora = DateTime.Now;
        }

        private bool CanOk()
        {
            if (CompetidorSelecionado != null && (Raia >0 && Raia <= 8)) { return true; }
            return false;

        }

        private void Ok()
        {
            int a = 0;
        }
    }
}
