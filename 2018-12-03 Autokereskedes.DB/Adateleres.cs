using _2018_11_05_AutoKereskedes.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace _2018_11_05_AutoKereskedes
{
    public class AdatEleres : IAdatEleres
    {
        private string conStr = "Server=localhost;Database=14KE_Autokereskedes;Uid=root;Pwd=;";
        public List<AutoTipus> ListAutoTipusok()
        {
            List<AutoTipus> lista = new List<AutoTipus>();
            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "Select id, megnevezes from AutoTipus";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new AutoTipus()
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Megnevezes = reader["megnevezes"].ToString()
                            });
                        }
                    }
                }

            }  //con.Dispose();

            return lista;
        }

        public List<Auto> ListAutok()
        {
            List<Auto> lista = new List<Auto>();
            List<AutoTipus> tipusok = ListAutoTipusok();
            using (var con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "SELECT Id, Rendszam, AutoTipusId, Alvazszam, Motorszam, " +
                                     "ElsoForgalombaHelyezes, AutomataValto, KmOraAllas, Uzemanyag " +
                              "FROM Auto";
                using (var cmd = new MySqlCommand(sql, con))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Auto()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Rendszam = reader["Rendszam"].ToString(),
                            Tipus = tipusok.Where(t => t.Id == Convert.ToInt32(reader["AutoTipusId"])).FirstOrDefault(),
                            Alvazszam = reader["Alvazszam"].ToString(),
                            Motorszam = reader["Motorszam"].ToString(),
                            ElsoForgalombaHelyezes = reader["ElsoForgalombaHelyezes"] == DBNull.Value ?
                                                     null : (DateTime?)Convert.ToDateTime(reader["ElsoForgalombaHelyezes"]),
                            AutomataValto = Convert.ToInt32(reader["AutomataValto"]) == 1,
                            KmOraAllas = reader["KmOraAllas"] == DBNull.Value ?
                                         null : (int?)Convert.ToInt32(reader["KmOraAllas"]),
                            Uzemanyag = (UzemanyagEnum)Convert.ToInt32(reader["Uzemanyag"])
                        });
                    }
                }
            }

            return lista;
        }

        public Auto GetAuto(int id) 
        {
            List<AutoTipus> tipusok = ListAutoTipusok();
            using (var con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "SELECT Id, Rendszam, AutoTipusId, Alvazszam, Motorszam, " +
                                     "ElsoForgalombaHelyezes, AutomataValto, KmOraAllas, Uzemanyag " +
                              "FROM Auto " +
                              "Where id = @Id";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Auto()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Rendszam = reader["Rendszam"].ToString(),
                                Tipus = tipusok.Where(t => t.Id == Convert.ToInt32(reader["AutoTipusId"])).FirstOrDefault(),
                                Alvazszam = reader["Alvazszam"].ToString(),
                                Motorszam = reader["Motorszam"].ToString(),
                                ElsoForgalombaHelyezes = reader["ElsoForgalombaHelyezes"] == DBNull.Value ?
                                                     null : (DateTime?)Convert.ToDateTime(reader["ElsoForgalombaHelyezes"]),
                                AutomataValto = Convert.ToInt32(reader["AutomataValto"]) == 1,
                                KmOraAllas = reader["KmOraAllas"] == DBNull.Value ?
                                         null : (int?)Convert.ToInt32(reader["KmOraAllas"]),
                                Uzemanyag = (UzemanyagEnum)Convert.ToInt32(reader["Uzemanyag"])
                            };
                        }
                        else
                            return null;
                    }
                }
            }
        }

        public Auto InsertAuto(Auto a)
        {
            using (var con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "INSERT INTO auto(Rendszam, AutoTipusId, Alvazszam, Motorszam, ElsoForgalombaHelyezes, AutomataValto, KmOraAllas, Uzemanyag) " +
                             "VALUES(@Rendszam, @AutoTipusId, @Alvazszam, @Motorszam, @ElsoForgalombaHelyezes, @AutomataValto, @KmOraAllas, @Uzemanyag)";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    addParameters(cmd, a);
                    cmd.ExecuteNonQuery();
                    return GetAuto((int)cmd.LastInsertedId);
                }
            }
        }

        public Auto UpdateAuto(Auto a)
        {
            using (var con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "Update auto set " +
                                "Rendszam = @Rendszam, " +
                                "AutoTipusId = @AutoTipusId, " +
                                "Alvazszam = @Alvazszam, " +
                                "Motorszam = @Motorszam, " +
                                "ElsoForgalombaHelyezes = @ElsoForgalombaHelyezes, " +
                                "AutomataValto = @AutomataValto, " +
                                "KmOraAllas = @KmOraAllas, " +
                                "Uzemanyag = @Uzemanyag " +
                             "Where id = @ID";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    addParameters(cmd, a);
                    cmd.Parameters.AddWithValue("@id", a.Id);
                    cmd.ExecuteNonQuery();
                    return GetAuto((int)cmd.LastInsertedId);
                }
            }
        }

        private void addParameters(MySqlCommand cmd, Auto a)
        {
            cmd.Parameters.AddWithValue("@Rendszam", a.Rendszam);
            cmd.Parameters.AddWithValue("@AutoTipusId", a.Tipus.Id);
            cmd.Parameters.AddWithValue("@Alvazszam", a.Alvazszam);
            cmd.Parameters.AddWithValue("@Motorszam", a.Motorszam);
            cmd.Parameters.AddWithValue("@ElsoForgalombaHelyezes", a.ElsoForgalombaHelyezes);
            cmd.Parameters.AddWithValue("@AutomataValto", a.AutomataValto);
            cmd.Parameters.AddWithValue("@KmOraAllas", a.KmOraAllas);
            cmd.Parameters.AddWithValue("@Uzemanyag", (int)a.Uzemanyag);
        }

        public void DeleteAuto(Auto a)
        {
            using (var con = new MySqlConnection(conStr))
            {
                con.Open();
                string sql = "delete from auto " +
                             "Where id = @ID";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", a.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
