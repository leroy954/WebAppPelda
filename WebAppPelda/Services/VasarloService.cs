using MySql.Data.MySqlClient;
using WebAppPelda.Models;
using WebAppPelda.DataManager;

namespace WebAppPelda.Services
{
    public class VasarloService
    {
        public List<Vasarlo> GetAllVasarlok()
        {
            using (MySqlConnection conn = new DBManager().CreateConnection())
            {
                try
                {
                    conn.Open();
                    List<Vasarlo> result = new List<Vasarlo>();
                    string sql = "SELECT * FROM vasarlo;";
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        Vasarlo ujVasarlo = new Vasarlo();
                        ujVasarlo.Id = reader.GetInt32("Id");
                        ujVasarlo.Nev = reader.GetString("Nev");
                        ujVasarlo.Cim = reader.GetString("Cim");
                        ujVasarlo.Email = reader.GetString("Email");
                        ujVasarlo.Telefon = reader.GetString("Telefon");
                        ujVasarlo.Pontszam = reader.GetInt32("Pontszam");
                        result.Add(ujVasarlo);
                    }
                    conn.Close();
                    return result;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    return new List<Vasarlo>() { new Vasarlo() { Nev = ex.Message } };
                }
                
            }
        
        }

        public Vasarlo GetById(int id)
        {
            try
            {
                Vasarlo result = new Vasarlo();
                using (MySqlConnection conn = new DBManager().CreateConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM vasarlo WHERE Id=@Id;";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    result.Id = reader.GetInt32("Id");
                    result.Nev = reader.GetString("Nev");
                    result.Cim = reader.GetString("Cim");
                    result.Email = reader.GetString("Email");
                    result.Telefon = reader.GetString("Telefon");
                    result.Pontszam = reader.GetInt32("Pontszam");

                }   
                
                    return result;
            }
            catch (Exception ex)
            {

                Vasarlo hiba = new Vasarlo();
                hiba.Nev = ex.Message;
                return hiba;
            }



        }


        public string PostVasarlo(Vasarlo uj)
        {
            try
            {
                string result = "";
                using (MySqlConnection conn = new DBManager().CreateConnection())
                {
                    conn.Open();
                    string sql = "INSERT INTO vasarlo (Nev, Cim, Email, Telefon, Pontszam) VALUES (@Nev, @Cim, @Email, @Telefon, @Pontszam);";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Nev", uj.Nev);
                    cmd.Parameters.AddWithValue("@Cim", uj.Cim);
                    cmd.Parameters.AddWithValue("@Email", uj.Email);
                    cmd.Parameters.AddWithValue("@Telefon", uj.Telefon);
                    cmd.Parameters.AddWithValue("@Pontszam", uj.Pontszam);
                   int sorokSzama = cmd.ExecuteNonQuery();
                    if (sorokSzama > 0)
                    {
                        result = "Sikeres mentés!";
                    }
                    else
                    {
                        result = "Sikertelen mentés!";
                    }
                    conn.Close();
                }
                return result;

            }
            catch (Exception ex)
            {

                return ex.Message;
            }


        }



        public string PutVasarlo(Vasarlo uj)
        {
            try
            {
                string result = "";
                using (MySqlConnection conn = new DBManager().CreateConnection())
                {
                    conn.Open();
                    string sql = "UPDATE vasarlo SET nev = @nev, cim = @cim, email = @cim, telefon = @telefon, pontszam = @pontszam WHERE id  = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Nev", uj.Nev);
                    cmd.Parameters.AddWithValue("@Cim", uj.Cim);
                    cmd.Parameters.AddWithValue("@Email", uj.Email);
                    cmd.Parameters.AddWithValue("@Telefon", uj.Telefon);
                    cmd.Parameters.AddWithValue("@Pontszam", uj.Pontszam);
                    cmd.Parameters.AddWithValue("@Id", uj.Id);
                    int sorokSzama = cmd.ExecuteNonQuery();
                    if (sorokSzama > 0)
                    {
                        result = "Sikeres módosítás!";
                    }
                    else
                    {
                        result = "Sikertelen módosítás!";
                    }
                    conn.Close();
                }
                return result;

            }
            catch (Exception ex)
            {

                return ex.Message;
            }


        }




        public string DeleteVasarlo(int id)
        {
            try
            {
                string result = "";
                using (MySqlConnection conn = new DBManager().CreateConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM vasarlo WHERE Id = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    int sorokSzama = cmd.ExecuteNonQuery();
                    if (sorokSzama > 0)
                    {
                        result = "Sikeres törlés!";
                    }
                    else
                    {
                        result = "Sikertelen törlés!";
                    }
                    conn.Close();
                }
                return result;

            }
            catch (Exception ex)
            {

                return ex.Message;
            }


        }


    }
}
