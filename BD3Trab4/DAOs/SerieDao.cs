using System;
using System.Data.Odbc;
using BD3Trab4.Dominio;

namespace BD3Trab4.DAOs
{
    public class SerieDao : Dao
    {
        public bool InserirSerie(string nome, DateTime dataHora, Prova prova)
        {
            try
            {
                OpenConection();
                var command = CreateCommand(@"insert into serie (nome,data_hora,fk_id_prova) values (?,?,?)");

                command.Parameters.Add("@nome", OdbcType.VarChar).Value = nome;
                command.Parameters.Add("@data_hora", OdbcType.DateTime).Value = dataHora;
                command.Parameters.Add("@fk_id_prova", OdbcType.Int).Value = prova.Id;

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

        public Serie GetSerieParaCadastramentoDeCompetidores(int provaId)
        {
            try
            {
                OpenConection();

                var command = CreateCommand(@"select * from serie where (total_participantes < 8 or total_participantes is null) and 
                                             fk_id_prova = ? order by id_serie");

                command.Parameters.Add("@fk_id_prova", OdbcType.Int).Value = provaId;

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DateTime dataHora;
                    int totalParticipantes, totalEfetivos, id, idProva;
                    var nome = reader["nome"].ToString();
                    DateTime.TryParse(reader["data_hora"].ToString(), out dataHora);
                    int.TryParse(reader["id_serie"].ToString(), out id);
                    int.TryParse(reader["total_participantes"].ToString(), out totalParticipantes);
                    int.TryParse(reader["total_efetivos"].ToString(), out totalEfetivos);
                    int.TryParse(reader["fk_id_prova"].ToString(), out idProva);

                    var prova = new ProvaDao().GetProvaById(idProva);
                    return new Serie(id, nome, dataHora, totalParticipantes, totalParticipantes, prova);
                }
                return null;
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

        public bool PodeCriarNovasSeries()
        {
            try
            {
                OpenConection();

                var command = CreateCommand(@"select count(*) from serie where total_participantes < 8");
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var total = int.Parse(reader[0].ToString());
                    if (total == 0)
                        return true;
                    return false;
                }
                return false;
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

        public Tuple<bool,string> CadastrarCompetidor(int idSerie, int idCompetidor, int raia)
        {
            try
            {
                OpenConection();
                var command = CreateCommand(@"select count(raia) from serie_competidor where id_serie = ? and raia = ?");

                command.Parameters.Add("@id_serie", OdbcType.Int).Value = idSerie;
                command.Parameters.Add("@raia", OdbcType.Int).Value = raia;

                var reader =command.ExecuteReader();

                if (reader.Read())
                {
                    var total = int.Parse(reader[0].ToString());

                    if (total > 0)
                    {
                        return new Tuple<bool, string>(false,"Essa raia já está ocupada");
                    }
                }


                command = CreateCommand(@"insert into serie_competidor (id_serie,id_competidor, raia) values(?,?,?);");
                command.Parameters.Add("@id_serie", OdbcType.Int).Value = idSerie;
                command.Parameters.Add("@id_competidor", OdbcType.Int).Value = idCompetidor;
                command.Parameters.Add("@raia", OdbcType.Int).Value = raia;
                command.ExecuteNonQuery();

                return new Tuple<bool, string>(true,"");
            }
            catch (Exception e)
            {
                return new Tuple<bool, string>(false,e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}