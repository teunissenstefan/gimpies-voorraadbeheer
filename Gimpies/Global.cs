using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                voorraad.Add(new Voorraad(Int64.Parse(indexes[0]), indexes[1], Int64.Parse(indexes[2]), float.Parse(indexes[4]), Int64.Parse(indexes[3])));
            }
            return voorraad;
        }

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
