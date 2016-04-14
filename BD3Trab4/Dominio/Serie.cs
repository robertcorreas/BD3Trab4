using System;

namespace BD3Trab4.Dominio
{
    public class Serie
    {
        public Serie(int id, string nome, DateTime dataHora, int totalParticipantes, int totalEfetivo, Prova prova)
        {
            Id = id;
            Nome = nome;
            DataHora = dataHora;
            TotalParticipantes = totalParticipantes;
            TotalEfetivo = totalEfetivo;
            Prova = prova;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataHora { get; private set; }
        public int TotalParticipantes { get; private set; }
        public int TotalEfetivo { get; private set; }
        public Prova Prova { get; set; }
    }
}