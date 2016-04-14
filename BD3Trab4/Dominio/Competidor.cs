using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD3Trab4.Dominio
{
    public class Competidor
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public char Sexo { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Patrocinio { get; private set; }

        public Competidor(int id,string nome,char sexo, DateTime dataNascimento, string patrocinio)
        {
            Id = id;
            Nome = nome;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            Patrocinio = patrocinio;
        }
    }
}
