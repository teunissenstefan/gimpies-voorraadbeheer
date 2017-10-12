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
    public class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static List<Voorraad> voorraad = new List<Voorraad>();
        static string uname;

        static Global globalClass = new Global();

        static void Main(string[] args)
        {
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
            try
            {
                Console.Title = "Gimpies voorraadbeheer";
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Clear();

                Console.Write("Naam: ");
                string username = Console.ReadLine();
                if (username == string.Empty)
                {
                    Begin();
                }
                else
                {
                    uname = username;
                    Wachtwoord(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }
        
        static void Wachtwoord(long tries)
        {
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
                if (globalClass.LOGIN(password))
                {
                    voorraad = globalClass.VOORRAAD_LOAD();
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
            Console.Clear();
            string titleText = "Welkom " + globalClass.FIRST_CHAR_UC(uname) + "!";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (titleText.Length / 2)) + "}", titleText)); //Welkom bericht in het midden


            //Menu
            Console.WriteLine("\n\t1. Voorraad bekijken" + 
                "\n\t2. Voorraad wijzigen\n" +
                "\n\t9. Uitloggen" +
                "\n\t0. Afsluiten");
            Console.Write("\nKeuze: ");
            
            string keuze = Console.ReadLine();
            if (keuze == "1")
            {
                VoorraadBekijken();
                //Voorraad bekijken
            }
            else if (keuze == "2")
            {
                VoorraadBewerkenLijst();
                //Naar de lijst om te bewerken
            }
            else if (keuze == "9")
            {
                //Uitloggen (log)
                //Naar begin
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
                //Naar menu
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
                globalClass.VOORRAAD_SAVE(voorraad);
                Menu();
            }
            else if (keuze == string.Empty)
            {
                VoorraadBewerkenLijst();
            }
            else
            {
                bool itemBestaat = voorraad.Any(r => r.ItemID == Int64.Parse(keuze));
                if (itemBestaat)
                {
                    VoorraadBewerken(Int64.Parse(keuze));
                }
                else
                {
                    Console.WriteLine("Item bestaat niet");
                    Console.ReadLine();
                    VoorraadBewerkenLijst();
                }
            }
        }

        static void VoorraadBewerken(long id)
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
                { }
                else if (input == string.Empty)
                {
                    VoorraadBewerkenLijst();
                }
                else
                {
                    if (item.ItemAmount + Int64.Parse(input) < 0)
                    {
                        Console.WriteLine("Er kunnen er niet meer weg zijn dan dat er beschikbaar is. Er zijn er maar " + item.ItemAmount + " beschikbaar.");
                        Console.ReadLine();
                        VoorraadBewerken(id);
                    }
                    else
                    {
                        item.ItemAmount += Int64.Parse(input);
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
            string[] alignment = {
                "-10", //ID
                "-30", //BESCHRIJVING
                "-10", //AANTAL
                "-10", //MAAT
                "-10"  //PRIJS
            };
            Console.WriteLine("{0,"+alignment[0]+"}{1,"+alignment[1]+ "}{2," + alignment[2] + "}{3," + alignment[3] + "}{4," + alignment[4] + "}", "ID", "BESCHRIJVING", "AANTAL", "MAAT", "PRIJS");
            foreach (var item in voorraad)
            {
                Console.WriteLine("{0," + alignment[0] + "}{1," + alignment[1] + "}{2," + alignment[2] + "}{3," + alignment[3] + "}{4," + alignment[4] + "}",
                                  item.ItemID,
                                  item.ItemDesc,
                                  item.ItemAmount,
                                  item.ItemMaat,
                                  item.ItemPrijs);
            }
        }   
    }
}