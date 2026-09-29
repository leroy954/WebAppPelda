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
        

    }
}
