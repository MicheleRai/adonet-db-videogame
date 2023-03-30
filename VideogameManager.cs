using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace adonet_db_videogame

{
    internal class VideogameManager
    {
        string connStr;

        public VideogameManager(string connStr)
        {
            this.connStr = connStr;
        }
        public List<Videogame> GetVideogameByNameLike(string likeString)
        {
            using var conn = new SqlConnection(connStr);
            var videogame = new List<Videogame>();

            try
            {
                conn.Open();

                var query = "SELECT id, name, overview, relase_date, software_house_id"
                    + " FROM videogames"
                    + $" WHERE name LIKE @NameLike"
                    + " ORDER BY name";

                using var command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@NameLike", $"%{likeString}%");

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var idIdx = reader.GetOrdinal("id");
                    var id = reader.GetInt64(idIdx);

                    var nameIdx = reader.GetOrdinal("name");
                    var name = reader.GetString(nameIdx);

                    var overviewIdx = reader.GetOrdinal("overview");
                    var overview = reader.GetString(overviewIdx);

                    var relase_dateIdx = reader.GetOrdinal("relase_date");
                    var relase_date = reader.GetDateTime(relase_dateIdx);

                    var software_house_idIdx = reader.GetOrdinal("software_house_id");
                    var software_house_id = reader.GetInt32(software_house_idIdx);

                    var p = new Videogame(id, name, overview, relase_date, software_house_id);
                    videogame.Add(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return videogame;
        }

        public void AddVideogame(Videogame videogame)
        {
            using var conn = new SqlConnection(connStr);

            try
            {
                conn.Open();
                using var tran = conn.BeginTransaction();

                try
                {
                    var query = "INSERT INTO videogames (name, overview, release_date, software_house_id)"
                        + " VALUES ( @Name, @Overview, @Release_date, @Software_house_id );";

                    var cmd = new SqlCommand(query, conn, tran);
                    cmd.Parameters.AddWithValue("@Name", videogame.Name);
                    cmd.Parameters.AddWithValue("@Overview", videogame.Overview);
                    cmd.Parameters.AddWithValue("@Release_date", videogame.Release_date);
                    cmd.Parameters.AddWithValue("@Software_house_id", videogame.Softweare_house_id);


                    cmd.ExecuteNonQuery();
                    //cmd.ExecuteNonQuery();

                    Console.WriteLine("Commit");
                    tran.Commit();
                }
                catch
                {
                    Console.WriteLine("Rollback");
                    tran.Rollback();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public List<Videogame> GetVideogameById(string likeInt)
        {
            using var conn = new SqlConnection(connStr);
            var videogame = new List<Videogame>();

            try
            {
                conn.Open();

                var query = "SELECT id, name, overview, relase_date, software_house_id"
                    + " FROM videogames"
                    + $" WHERE id LIKE @likeInt"
                    + " ORDER BY id";

                using var command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@likeInt", likeInt);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var idIdx = reader.GetOrdinal("id");
                    var id = reader.GetInt64(idIdx);

                    var nameIdx = reader.GetOrdinal("name");
                    var name = reader.GetString(nameIdx);

                    var overviewIdx = reader.GetOrdinal("overview");
                    var overview = reader.GetString(overviewIdx);

                    var relase_dateIdx = reader.GetOrdinal("relase_date");
                    var relase_date = reader.GetDateTime(relase_dateIdx);

                    var software_house_idIdx = reader.GetOrdinal("software_house_id");
                    var software_house_id = reader.GetInt32(software_house_idIdx);

                    var p = new Videogame(id, name, overview, relase_date, software_house_id);
                    videogame.Add(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return videogame;
        }
        public void DeleteGame(long id)
        {
            using var conn = new SqlConnection(connStr);

            conn.Open();

            string query = "delete from videogames " +
                "where id=@Id;";

            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@Id", id));

            int lineaObbiettivo = cmd.ExecuteNonQuery();
            Console.WriteLine($"Record eliminati: {lineaObbiettivo}.");
        }

    }
}
