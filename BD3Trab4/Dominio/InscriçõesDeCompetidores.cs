namespace BD3Trab4.Dominio
{
    public class InscriçõesDeCompetidores
    {
        private static InscriçõesDeCompetidores _instance;

        private InscriçõesDeCompetidores()
        {
        }

        public bool InscriçõesFechadas { get; set; }

        public static InscriçõesDeCompetidores Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance = new InscriçõesDeCompetidores();
                return _instance;
            }
        }
    }
}