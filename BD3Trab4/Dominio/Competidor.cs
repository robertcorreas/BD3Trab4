using System;

namespace BD3Trab4.Dominio
{
    public class Competidor
    {
        public Competidor(int id, string nome, char sexo, DateTime dataNascimento, string patrocinio)
        {
            Id = id;
            Nome = nome;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            Patrocinio = patrocinio;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public char Sexo { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Patrocinio { get; private set; }
    }
}