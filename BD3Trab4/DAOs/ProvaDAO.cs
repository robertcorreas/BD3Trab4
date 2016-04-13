using System;
using System.Collections.Generic;
using BD3Trab4.Dominio;

namespace BD3Trab4.DAOs
{
    public class ProvaDao : Dao
    {
        public List<Prova> GetProvas()
        {
            try
            {
                OpenConection();
                var command = CreateCommand("select * from prova");
                var reader = command.ExecuteReader();

                var provas = new List<Prova>();

                while (reader.Read())
                {
                    int id;
                    char sexo;
                    DateTime dataSemifinal, dataFinal;

                    int.TryParse(reader["id_prova"].ToString(), out id);
                    var distancia = reader["distancia"].ToString();
                    var modalidade = reader["modalidade"].ToString();
                    char.TryParse(reader["sexo"].ToString(), out sexo);
                    DateTime.TryParse(reader["data_semifinal"].ToString(), out dataSemifinal);
                    DateTime.TryParse(reader["data_final"].ToString(), out dataFinal);

                    provas.Add(new Prova(id, distancia, modalidade, sexo, dataSemifinal, dataFinal));
                }
                CloseConnection();
                return provas;
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