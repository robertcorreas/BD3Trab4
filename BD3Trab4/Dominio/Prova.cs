using System;

namespace BD3Trab4.Dominio
{
    public class Prova
    {
        public Prova(int id, string distancia, string modalidade, char sexo, DateTime dataSemifinal, DateTime dataFinal)
        {
            Id = id;
            Distancia = distancia;
            Modalidade = modalidade;
            Sexo = sexo;
            DataSemifinal = dataSemifinal;
            DataFinal = dataFinal;
        }

        public int Id { get; private set; }
        public string Distancia { get; private set; }
        public string Modalidade { get; private set; }
        public char Sexo { get; private set; }
        public DateTime DataSemifinal { get; private set; }
        public DateTime DataFinal { get; private set; }

        public override string ToString()
        {
            return $"{Distancia}m - {Modalidade} - {Sexo}";
        }
    }
}