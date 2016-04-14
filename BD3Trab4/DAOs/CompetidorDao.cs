using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Odbc;
using System.Windows.Input;
using BD3Trab4.Dominio;

namespace BD3Trab4.DAOs
{
    public class CompetidorDao : Dao
    {
        public Competidor GetCompetidorById(int id)
        {
            try
            {
                OpenConection();
                var command = CreateCommand("select * from competidor where id_competidor = ?");
                command.Parameters.Add("@id_competidor", OdbcType.Int).Value = id;
                var reader = command.ExecuteReader();


                if (reader.Read())
                {
                    char sexo;
                    DateTime dataNascimento;

                    int.TryParse(reader["id_competidor"].ToString(), out id);
                    char.TryParse(reader["sexo"].ToString(), out sexo);
                    var nome = reader["nome"].ToString();
                    var patrocinio = reader["patrocinio"].ToString();
                    DateTime.TryParse(reader["data_nascimento"].ToString(), out dataNascimento);

                    return new Competidor(id, nome, sexo, dataNascimento, patrocinio);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Competidor> GetCompetidores()
        {
            try
            {
                OpenConection();

                var command = CreateCommand("select * from competidor");

                var reader = command.ExecuteReader();

                var competidores = new List<Competidor>();

                while (reader.Read())
                {
                    int id;
                    char sexo;
                    DateTime dataNascimento;

                    int.TryParse(reader["id_competidor"].ToString(), out id);
                    char.TryParse(reader["sexo"].ToString(), out sexo);
                    var nome = reader["nome"].ToString();
                    var patrocinio = reader["patrocinio"].ToString();
                    DateTime.TryParse(reader["data_nascimento"].ToString(), out dataNascimento);

                    competidores.Add(new Competidor(id, nome, sexo, dataNascimento, patrocinio));
                }
                return competidores;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool InserirCompetidor(string nome, DateTime datanascimento, string sexo, string patrocinio, IEnumerable<Prova> provasEscolhidas)
        {
            try
            {
                OpenConection();
                var command = CreateCommand(@"insert into competidor (nome,sexo,data_nascimento,patrocinio) 
                                             values (?,?,?,?)");

                command.Parameters.Add("@nome", OdbcType.VarChar).Value = nome;
                command.Parameters.Add("@sexo", OdbcType.Char).Value = sexo;
                command.Parameters.Add("@data_nascimento", OdbcType.Date).Value = datanascimento;
                command.Parameters.Add("@patrocinio", OdbcType.VarChar).Value = patrocinio;

                command.ExecuteNonQuery();


                command = CreateCommand(@"select SEQ_COMPETIDOR_ID.currval from DUAL");

                var reader = command.ExecuteReader();
                reader.Read();

                var id = int.Parse(reader["currval"].ToString());

                foreach (var prova in provasEscolhidas)
                {
                    command = CreateCommand(@"insert into prova_competidor (id_competidor, id_prova) values (?,?)");

                    command.Parameters.Add("@id_competidor", OdbcType.Int).Value = id;
                    command.Parameters.Add("@id_prova", OdbcType.Int).Value = prova.Id;
                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
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