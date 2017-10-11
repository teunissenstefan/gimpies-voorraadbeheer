using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Gimpies
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static List<Voorraad> voorraad = new List<Voorraad>();
        static string uname;

        static void VOORRAAD_LOAD()
        {
            foreach (var line in File.ReadLines("voorraad.txt"))
            {
                List<String> indexes = line.Split(',').ToList<String>();
                voorraad.Add(new Voorraad(Int32.Parse(indexes[0]), indexes[1], Int32.Parse(indexes[2])));
            }
        }

        static void VOORRAAD_SAVE()
        {
            using (TextWriter tw = new StreamWriter("voorraad.txt"))
            {
                foreach (Voorraad s in voorraad)
                {
                    tw.WriteLine(s.ItemID + "," + s.ItemDesc + "," + s.ItemAmount);
                }
            }
        }

        static void Main(string[] args)
        {
            Medewerkers medewerkersClass = new Medewerkers();

            if (args.Length>0)
            {
                if (args[0] == "gui")
                {
                    //Console verbergen
                    var handle = GetConsoleWindow();
                    ShowWindow(handle, SW_HIDE);
                    //Form starten
                    Application.EnableVisualStyles();
                    Application.Run(new Login());
                }
                else
                {
                    Begin();
                }
            }
            else
            {
                Begin();
            }
        }

        static void Begin()
        {
            Medewerkers medewerkersClass = new Medewerkers();

            Console.Title = "Gimpies voorraadbeheer";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Clear();

            //Console.WriteLine("Gimpies voorraadbeheer");

            Console.Write("Naam: ");
            string username = Console.ReadLine();
            uname = username;
            Wachtwoord(0);
        }
        
        static void Wachtwoord(int tries)
        {
            Medewerkers medewerkersClass = new Medewerkers();
            Global globalClass = new Global();

            if (tries < globalClass.MAX_LOGIN_TRIES())
            {
                Console.Clear();
                Console.Write("Wachtwoord: ");
                string password = null;
                while (true)
                {
                    var key = System.Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    password += key.KeyChar;
                }
                if (medewerkersClass.Login(password))
                {
                    VOORRAAD_LOAD();
                    Menu();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wachtwoord fout");
                    Console.ReadKey();
                    //Als het wachtwoord fout is gebruiker opnieuw laten proberen
                    Wachtwoord(tries+1);
                }
            }
            else
            {
                Begin();
            }
        }

        static void Menu()
        {
            Medewerkers medewerkersClass = new Medewerkers();
            
            Console.Clear();
            string titleText = "Welkom " + uname + "!";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (titleText.Length / 2)) + "}", titleText));


            //Menu
            Console.WriteLine();
            Console.WriteLine("\t1. Voorraad bekijken");
            Console.WriteLine("\t2. Voorraad wijzigen");
            Console.WriteLine();
            Console.WriteLine("\t9. Uitloggen");
            Console.WriteLine("\t0. Afsluiten");
            Console.WriteLine();
            Console.Write("Keuze: ");
            
            string keuze = Console.ReadLine();
            if (keuze == "1")
            {
                VoorraadBekijken();
            }
            else if (keuze == "2")
            {
                VoorraadBewerkenLijst();
            }
            else if (keuze == "9")
            {
                //Uitloggen (log)
                //naar begin
                Begin();
            }
            else if (keuze == "0")
            {
                //Uitloggen (log)
                Application.Exit();
            }
            else
            {
                Menu();
            }
        }

        static void VoorraadBekijken()
        {
            Console.Clear();

            ShowVoorraad();

            Console.ReadKey();
            Menu();
        }

        static void VoorraadBewerkenLijst()
        {
            Console.Clear();
            Console.WriteLine("Typ @ om terug naar het menu te gaan");
            Console.WriteLine("Typ het ID in van het item dat je wilt aanpassen");
            Console.WriteLine();

            ShowVoorraad();
            Console.WriteLine();

            string keuze = Console.ReadLine();
            if (keuze == "@")
            {
                VOORRAAD_SAVE();
                Menu();
            }
            else
            {
                bool itemBestaat = voorraad.Any(r => r.ItemID == Int32.Parse(keuze));
                if (itemBestaat)
                {
                    VoorraadBewerken(Int32.Parse(keuze));
                }
                else
                {
                    Console.WriteLine("Item bestaat niet");
                    Console.ReadLine();
                    VoorraadBewerkenLijst();
                }
            }
        }

        static void VoorraadBewerken(int id)
        {
            Console.Clear();
            Console.WriteLine("Typ @ om terug naar de lijst te gaan");
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                var item = voorraad.Find(r => r.ItemID == id);
                Console.WriteLine(item.ItemDesc);
                Console.WriteLine("Huidig aantal: " + item.ItemAmount);
                Console.WriteLine();
                Console.Write("Verandering: ");
                string input = Console.ReadLine();
                if (input == "@")
                {}
                else
                {
                    if (item.ItemAmount - Int32.Parse(input) < 0)
                    {
                        Console.WriteLine("Er kunnen er niet meer weg zijn dan dat er beschikbaar is. Er zijn er maar " + item.ItemAmount + " beschikbaar.");
                        Console.ReadLine();
                        VoorraadBewerken(id);
                    }
                    else
                    {
                        item.ItemAmount = item.ItemAmount + Int32.Parse(input);
                    }
                }
                VoorraadBewerkenLijst();
            }
            else
            {
                Console.WriteLine("Item bestaat niet");
                Console.ReadLine();
                VoorraadBewerkenLijst();
            }
        }

        static void ShowVoorraad()
        {
            //Voorraad laten zien
            Console.WriteLine("{0,-7}{1,-20}{2,-10}", "ID", "BESCHRIJVING", "AANTAL");
            foreach (var item in voorraad)
            {
                Console.WriteLine("{0,-7}{1,-20}{2,-10}",
                                  item.ItemID,
                                  item.ItemDesc,
                                  item.ItemAmount);
            }
        }
    }
}
