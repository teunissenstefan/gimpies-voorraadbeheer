using System;
using System.Collections.Generic;
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
        {
            return 3;
        }
        public string FILE_LOCATION()
        {
            return "voorraad.txt";
        }
        public string FIRST_CHAR_UC(string text)
        {
            return text.First().ToString().ToUpper() + String.Join("", text.Skip(1));
        }
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
        public void VOORRAAD_SAVE(List<Voorraad> voorraad)
        {
            using (TextWriter tw = new StreamWriter(FILE_LOCATION()))
            {
                foreach (Voorraad s in voorraad)
                {
                    //MessageBox.Show("ID:"+s.ItemID + "\nDESC:"+s.ItemDesc);
                    tw.WriteLine(s.ItemID + "," + s.ItemDesc + "," + s.ItemAmount + "," + s.ItemMaat + "," + s.ItemPrijs);
                }
            }
        }
        public bool LOGIN(string wachtwoord)
        {
            if (wachtwoord == "1")
            {
                return true;
            }
            return false;
        }
    }
}
