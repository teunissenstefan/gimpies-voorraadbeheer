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
            return 3; //Standaard
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
                            newVoorraad.ItemVerkocht = Int64.Parse(resultRow.Row["verkocht"].ToString());
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
        public bool MysqlAddSale(string userid, string artikelid, string aantal, string euro)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO sales (userid,artikelid,aantal,euro,date) VALUES (@userid,@artikelid,@aantal,@euro,@date)";
                        command.Parameters.AddWithValue("@userid", Int64.Parse(userid));
                        command.Parameters.AddWithValue("@artikelid", Int64.Parse(artikelid));
                        command.Parameters.AddWithValue("@aantal", Int64.Parse(aantal));
                        command.Parameters.AddWithValue("@euro", euro);
                        command.Parameters.AddWithValue("@date",DateTime.Now);
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
        public bool MysqlServerUpdate(long id, string beschrijving, string aantal, string prijs, string maat, string verkocht)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE artikelen SET beschrijving=@beschrijving,aantal=@aantal,maat=@maat,prijs=@prijs,verkocht=@verkocht WHERE id=@id";
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@beschrijving", beschrijving);
                        command.Parameters.AddWithValue("@aantal", Int64.Parse(aantal));
                        command.Parameters.AddWithValue("@prijs", prijs.Replace(',', '.'));
                        command.Parameters.AddWithValue("@maat", Int64.Parse(maat));
                        command.Parameters.AddWithValue("@verkocht",Int64.Parse(verkocht));
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
        public bool LOGIN(string wachtwoord, string username)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM werknemers WHERE username=@uname AND password=@pword";
                        command.Parameters.AddWithValue("@uname", username);
                        command.Parameters.AddWithValue("@pword", wachtwoord);
                        MySqlDataAdapter adap = new MySqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        foreach (DataRowView resultRow in ds.Tables[0].DefaultView)
                        {
                            if (resultRow.Row["username"].ToString() == username)
                            {
                                return true;
                            }
                        }
                    }
                    connection.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan niet verbinden met de database: " + ex.ToString());
                return false;
            }
        }
        public bool OLDLOGIN(string wachtwoord)
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
        private void Log(bool inlog, long medewerkerId)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO log (medewerkerid,inlog,tijd) VALUES (@medewerker,@inlog,@tijd)";
                        command.Parameters.AddWithValue("@medewerker", medewerkerId);
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
        public void CheckIn(long medewerkerId)
        {
            Log(true, medewerkerId);
        }
        public void CheckOut(long medewerkerId)
        {
            Log(false, medewerkerId);
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
        public List<Werknemer> GetWerknemers()
        {
            try
            {
                List<Werknemer> werknemers = new List<Werknemer>();

                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM werknemers";
                        MySqlDataAdapter adap = new MySqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        foreach (DataRowView resultRow in ds.Tables[0].DefaultView)
                        {
                            Werknemer wnemer = new Werknemer((Int64)resultRow.Row["id"], resultRow.Row["username"].ToString(), (Int32)resultRow.Row["rank"]);
                            werknemers.Add(wnemer);
                        }
                    }
                    connection.Close();
                    return werknemers;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan niet verbinden met de database: " + ex.ToString());
                List<Werknemer> werknemers = new List<Werknemer>();
                return werknemers;
            }
        }
        public Werknemer GetWerknemer(string wachtwoord, string username)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM werknemers WHERE username=@uname AND password=@pword";
                        command.Parameters.AddWithValue("@uname", username);
                        command.Parameters.AddWithValue("@pword", wachtwoord);
                        MySqlDataAdapter adap = new MySqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adap.Fill(ds);
                        foreach (DataRowView resultRow in ds.Tables[0].DefaultView)
                        {
                            if (resultRow.Row["username"].ToString() == username)
                            {
                                return new Werknemer((Int64)resultRow.Row["id"], resultRow.Row["username"].ToString(), (Int32)resultRow.Row["rank"]);
                            }
                        }
                    }
                    connection.Close();
                    return new Werknemer(-1,"",0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan niet verbinden met de database: " + ex.ToString());
                return new Werknemer(-1, "", 0);
            }
        }
    }
}