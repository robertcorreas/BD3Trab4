using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD3Trab4.Dominio
{
    public class Competidor
    {
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Patrocinio { get; private set; }

        public Competidor(string nome, DateTime dataNascimento, string patrocinio)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Patrocinio = patrocinio;
        }
    }
}
