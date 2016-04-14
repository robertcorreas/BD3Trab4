using System;
using System.Collections.Generic;
using System.Data.Odbc;
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

                    CloseConnection();

                    return new Competidor(id, nome, sexo, dataNascimento, patrocinio);
                }
                else
                {
                    CloseConnection();
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
                CloseConnection();
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
    }
}