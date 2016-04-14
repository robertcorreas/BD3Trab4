using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BD3Trab4.DAOs
{
    public class SerieDao : Dao
    {
        public bool InserirSerie(string nome, DateTime dataHora)
        {
            try
            {
                OpenConection();
                var command = CreateCommand(@"insert into serie (nome,data_hora) values (?,?)");

                command.Parameters.Add("@nome", OdbcType.VarChar).Value = nome;
                command.Parameters.Add("@data_hora", OdbcType.DateTime).Value = dataHora;

                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
