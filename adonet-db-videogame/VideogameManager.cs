using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace adonet_db_videogame
{
    internal class VideogameManager
    {
        private static string connectionString = "Data Source=localhost;Initial Catalog=experis-db-videogames-query;Integrated Security=True;Pooling=False";

        public static bool InsertVideogame(Videogame newVideogame)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int insertedRows = 0;

                try
                {
                    connection.Open();

                    string query = "INSERT INTO videogames (name, overview, release_date, software_house_id) VALUES (@Name, @Overview, @ReleaseDate, @SoftwareHouseId);";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.Add(new SqlParameter("@Name", newVideogame.Name));
                    cmd.Parameters.Add(new SqlParameter("@Overview", newVideogame.Overview));
                    cmd.Parameters.Add(new SqlParameter("@ReleaseDate", newVideogame.ReleaseDate));
                    cmd.Parameters.Add(new SqlParameter("@SoftwareHouseId", newVideogame.SoftwareHouseId));

                    insertedRows = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return insertedRows > 0;
            }
        }
        public static bool GetVideogameById(string videogameId, out Videogame? videogame)
        {
            videogame = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT id, name, overview, release_date, software_house_id FROM videogames WHERE id=@Id;";

                    using(SqlCommand cmd = new SqlCommand(query,connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", videogameId));
                        using(SqlDataReader data = cmd.ExecuteReader())
                        {
                            while(data.Read())
                            {
                                long id = data.GetInt64(0);
                                string name = data.GetString(1);
                                string overview = data.GetString(2);
                                DateTime releaseDate = data.GetDateTime(3);
                                long softwareHouseId = data.GetInt64(4);

                                videogame = new Videogame(id, name, overview, releaseDate, softwareHouseId);
                            }

                        }
                    }
                }catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return videogame is not null;
        }
        public static bool DeleteVideogame(long id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int deletedRows = 0;
                try
                {
                    connection.Open();

                    string query = "DELETE FROM videogames WHERE id=@Id";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.Add(new SqlParameter("@Id", id));


                    deletedRows = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return deletedRows > 0;
            }

        }

    }
}
