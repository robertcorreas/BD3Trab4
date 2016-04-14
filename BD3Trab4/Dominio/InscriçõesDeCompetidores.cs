using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD3Trab4.Dominio
{
    public  class InscriçõesDeCompetidores
    {
        public bool InscriçõesFechadas { get; set; }

        private static InscriçõesDeCompetidores _instance;
        public static InscriçõesDeCompetidores Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                else
                {
                    _instance = new InscriçõesDeCompetidores();
                    return _instance;
                }
            }
        }

        private InscriçõesDeCompetidores()
        {
            
        }
    }
}
