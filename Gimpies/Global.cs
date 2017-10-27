using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gimpies
{
    class Global
    {
        public int MAX_LOGIN_TRIES()
        {//3
            foreach (var gvar in GetGlobalVars())
            {
                if (gvar.var.ToString() == "MAX_LOGIN_TRIES")
                {
                    return Int32.Parse(gvar.value);
                }
                else
                {
                    return 0;
                }
            }
            return 30;
        }
        public string ConnectionString()
        {
            return File.ReadAllText(CONNECTION_STRING_LOCATION());
            //return "server=localhost;user id=root;password=;database=gimpies;";
        }
        [System.Obsolete("Gebruik de Mysql server in plaats van het bestand.",true)]
        public string FILE_LOCATION()
        {
            return "data/voorraad.txt";
        }
        public string CONNECTION_STRING_LOCATION()
        {
            return "data/conn.txt";
        }
        public string FIRST_CHAR_UC(string text)
        {
            if(text == string.Empty)
            {
                return text;
            }
            return text.First().ToString().ToUpper() + String.Join("", text.Skip(1));
        }
        public List<Voorraad> MysqlServerLoadArtikelen()
        {
            try
            {
                List<Voorraad> voorraad = new List<Voorraad>();

                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM artikelen";
                        MySqlDataAdapter adap = new MySqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        foreach (DataRowView resultRow in ds.Tables[0].DefaultView)
                        {
                            Voorraad newVoorraad = new Voorraad();
                            newVoorraad.ItemID = Int64.Parse(resultRow.Row["id"].ToString());
                            newVoorraad.ItemDesc = (resultRow.Row["beschrijving"].ToString());
                            newVoorraad.ItemAmount = Int64.Parse(resultRow.Row["aantal"].ToString());
                            newVoorraad.ItemPrijs = (resultRow.Row["prijs"].ToString());
                            newVoorraad.ItemMaat = Int64.Parse(resultRow.Row["maat"].ToString());
                            voorraad.Add(newVoorraad);
                        }
                    }
                    voorraad = voorraad.OrderByDescending(p => p.ItemID).ToList();
                    connection.Close();
                    return voorraad;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan niet verbinden met de database: "+ex.ToString());
                return new List<Voorraad>();
            }
        }
        [System.Obsolete("VOORRAAD_LOAD is deprecated, gebruik MysqlServerLoadArtikelen.",true)]
        public List<Voorraad> VOORRAAD_LOAD()
        {
            List<Voorraad> voorraad = new List<Voorraad>();
            foreach (var line in File.ReadLines(FILE_LOCATION()))
            {
                List<String> indexes = line.Split(',').ToList<String>();
                Voorraad newVoorraad = new Voorraad();
                newVoorraad.ItemID = Int64.Parse(indexes[0]);
                newVoorraad.ItemDesc = (indexes[1]);
                newVoorraad.ItemAmount = Int64.Parse(indexes[2]);
                newVoorraad.ItemPrijs = (indexes[4]);
                newVoorraad.ItemMaat = Int64.Parse(indexes[3]);
                voorraad.Add(newVoorraad);
            }
            voorraad = voorraad.OrderBy(p => p.ItemID).ToList();
            return voorraad;
        }
        [System.Obsolete("VOORRAAD_SAVE is deprecated, gebruik de MysqlServerUpdate, MysqlServerInsert en MysqlServerDelete functies.",true)]
        public void VOORRAAD_SAVE(List<Voorraad> voorraad)
        {
            using (TextWriter tw = new StreamWriter(FILE_LOCATION()))
            {
                foreach (Voorraad s in voorraad)
                {
                    tw.WriteLine(s.ItemID + "," + s.ItemDesc + "," + s.ItemAmount + "," + s.ItemMaat + "," + s.ItemPrijs);
                }
            }
        }
        public bool MysqlServerInsert(string beschrijving, string aantal, string prijs, string maat)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO artikelen (beschrijving,aantal,maat,prijs) VALUES (@beschrijving,@aantal,@maat,@prijs)";
                        command.Parameters.AddWithValue("@beschrijving", beschrijving);
                        command.Parameters.AddWithValue("@aantal", Int64.Parse(aantal));
                        command.Parameters.AddWithValue("@prijs", prijs.Replace(',', '.'));
                        command.Parameters.AddWithValue("@maat", Int64.Parse(maat));
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan de rij niet toevoegen: " + ex.ToString());
                return false;
            }
        }
        public bool MysqlServerUpdate(long id, string beschrijving, string aantal, string prijs, string maat)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE artikelen SET beschrijving=@beschrijving,aantal=@aantal,maat=@maat,prijs=@prijs WHERE id=@id";
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@beschrijving", beschrijving);
                        command.Parameters.AddWithValue("@aantal", Int64.Parse(aantal));
                        command.Parameters.AddWithValue("@prijs", prijs.Replace(',', '.'));
                        command.Parameters.AddWithValue("@maat", Int64.Parse(maat));
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan de rij niet updaten: " + ex.ToString());
                return false;
            }
        }
        public bool MysqlServerDelete(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        if (id == -1)
                        {
                            command.CommandText = "DELETE FROM artikelen";
                        }
                        else
                        {
                            command.CommandText = "DELETE FROM artikelen WHERE id=@id";
                            command.Parameters.AddWithValue("@id", id);
                        }
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan de rij niet verwijderen: " + ex.ToString());
                return false;
            }
        }
        public bool LOGIN(string wachtwoord)
        {
            string ww = string.Empty;
            foreach (var gvar in GetGlobalVars())
            {
                if (gvar.var.ToString() == "WACHTWOORD")
                {
                    ww = gvar.value;
                }
                else
                {
                    ww = string.Empty;
                }
            }
            if (ww != string.Empty && wachtwoord == ww)
            {
                return true;
            }
            return false;
        }
        private void Log(bool inlog,string medewerker)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO log (medewerker,inlog,tijd) VALUES (@medewerker,@inlog,@tijd)";
                        command.Parameters.AddWithValue("@medewerker", medewerker);
                        command.Parameters.AddWithValue("@inlog", inlog);
                        command.Parameters.AddWithValue("@tijd", DateTime.Now);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan niet loggen: " + ex.ToString());
            }
        }
        public void CheckIn(string medewerker)
        {
            Log(true,medewerker);
        }
        public void CheckOut(string medewerker)
        {
            Log(false,medewerker);
        }
        private List<GlobalVars> GetGlobalVars()
        {
            try
            {
                List<GlobalVars> globalVars = new List<GlobalVars>();

                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM globalvars";
                        MySqlDataAdapter adap = new MySqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        foreach (DataRowView resultRow in ds.Tables[0].DefaultView)
                        {
                            GlobalVars gvar = new GlobalVars();
                            gvar.var = resultRow.Row["var"].ToString();
                            gvar.value = resultRow.Row["value"].ToString();
                            globalVars.Add(gvar);
                        }
                    }
                    connection.Close();
                    return globalVars;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan niet verbinden met de database: " + ex.ToString());
                List<GlobalVars> globalVars = new List<GlobalVars>();
                return globalVars;
            }
        }
    }
}