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
        static string _username = "";

        const ConsoleKey menuKey = ConsoleKey.F8;
        const ConsoleKey backKey = ConsoleKey.Escape;

        const string backMsg = "@BACK@";
        const string menuMsg = "@MENU@";

        const char horPipe = (char)9552;

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
                    MainView(View.Login);
                }
            }
            else
            {
                MainView(View.Login);
            }
        }

        static void Header(string _title = "")
        {
            string title;
            if (_title == string.Empty)
            {
                title = "Gimpies voorraadbeheer";
            }
            else
            {
                title = "Gimpies voorraadbeheer | " + _title;
            }
            Console.Title = title;
            Console.Clear();
            if (_username != string.Empty)
            {
                string headerText = "Welkom " + globalClass.FIRST_CHAR_UC(_username) + "! " + menuKey.ToString() + "=Menu " + backKey.ToString() + "=Terug";
                string hr = "##################################################################";
                //Welkom bericht in het midden:
                //Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (headerText.Length / 2)) + "}", headerText)); 
                //Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (hr.Length / 2)) + "}", hr));
                Console.WriteLine(headerText);
                WindowLine();
                Console.Write("\n");
            }
        }

        static void MainView(View view, string prevInput = "")
        {
            try
            {
                switch (view)
                {
                    case View.Login:
                        DisplayLoginUsername();
                        break;
                    case View.Menu:
                        if (_username != string.Empty) {
                            DisplayMenu(prevInput);
                        }
                        else
                        {
                            DisplayLoginUsername();
                        }
                        break;
                    case View.BekijkVoorraad:
                        DisplayVoorraad();
                        break;
                    case View.BekijkEditVoorraadList:
                        VoorraadBewerkenLijst();
                        break;
                    case View.EditVoorraad:
                        VoorraadBewerken(Int64.Parse(prevInput));
                        break;
                    case View.EXIT:
                        //Niks dus sluit hij af
                        break;
                    default:
                        MainView(View.Login);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Environment.NewLine + ex.ToString());
                Console.ReadLine();
                MainView(view);
            }
        }

        static void DisplayLoginUsername(int tries = 0)
        {
            Header("Login");
            Console.Write("Username: ");
            DisplayLoginPassword(tries, ReadLine());
        }

        static void DisplayLoginPassword(int tries = 0, string username = "")
        {
            if (username == string.Empty)
            {
                DisplayLoginUsername();
            }
            else
            {
                Console.Write("\nPassword: ");
                if (globalClass.LOGIN(ReadPassword()))
                {
                    _username = username;
                    MainView(View.Menu);
                }
                else
                {
                    tries = tries + 1;
                    if (tries < globalClass.MAX_LOGIN_TRIES())
                    {
                        DisplayLoginUsername(tries);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wachtwoord 3 keer fout");
                        Console.ReadKey();
                    }
                }
            }
        }

        static void DisplayMenu(string input)
        {
            Header("Menu");
            Console.WriteLine("\t1. Voorraad bekijken" +
                "\n\t2. Voorraad wijzigen\n" +
                "\n\t9. Uitloggen" +
                "\n\t0. Afsluiten");
            Console.Write("\nKeuze: ");
            if (input == string.Empty)
            {
                MainView(View.Menu, ReadLine());
            }
            else if (input == "1")
            {
                MainView(View.BekijkVoorraad);
            }
            else if (input == "2")
            {
                MainView(View.BekijkEditVoorraadList);
            }
            else if (input == "9")
            {
                _username = "";
                MainView(View.Login);
            }
            else if (input == "0")
            {
                MainView(View.EXIT);
            }
            else
            {
                MainView(View.Menu);
            }
        }

        static void DisplayVoorraad()
        {
            Header("Bekijk voorraad");
            voorraad = globalClass.VOORRAAD_LOAD();
            ShowVoorraad();
            Console.ReadKey();
            MainView(View.Menu);
        }

        static void VoorraadBewerkenLijst()
        {
            Header("Voorraad bewerken");
            voorraad = globalClass.VOORRAAD_LOAD();

            ShowVoorraad();
            WindowLine();
            Console.WriteLine("Typ het ID in van het item dat je wilt aanpassen");
            string keuze = ReadLine();
            if (keuze == menuMsg || keuze == backMsg)
            {
                MainView(View.Menu);
            }
            else if (keuze == string.Empty)
            {
                MainView(View.BekijkEditVoorraadList);
            }
            else
            {
                bool itemBestaat = voorraad.Any(r => r.ItemID == Int64.Parse(keuze));
                if (itemBestaat)
                {
                    MainView(View.EditVoorraad,keuze);
                }
                else
                {
                    Console.WriteLine("\n\nItem bestaat niet");
                    Console.ReadLine();
                    MainView(View.BekijkEditVoorraadList);
                }
            }
        }

        static void VoorraadBewerken(long id)
        {
            Header("Bewerken");
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                var item = voorraad.Find(r => r.ItemID == id);
                Header(item.ItemDesc+" bewerken");
                ShowVoorraad(id);
                WindowLine();
                Console.WriteLine("Huidig aantal: " + item.ItemAmount);
                Console.WriteLine();
                Console.Write("Verandering: ");
                string input = ReadLine();
                if (input == menuMsg)
                {
                    MainView(View.Menu);
                }
                else if (input == backMsg)
                {
                    MainView(View.BekijkEditVoorraadList);
                }
                else if (input == string.Empty)
                {
                    //Als hij leeg is juist zelfde laten en naar volgende gaan
                    MainView(View.BekijkEditVoorraadList);
                }
                else
                {
                    if (item.ItemAmount + Int64.Parse(input) < 0)
                    {
                        Console.WriteLine("Er kunnen er niet meer weg zijn dan dat er beschikbaar is. Er zijn er maar " + item.ItemAmount + " beschikbaar.");
                        Console.ReadLine();
                        MainView(View.EditVoorraad, id.ToString());
                    }
                    else
                    {
                        //Veranderen in lokale lijst
                        item.ItemAmount += Int64.Parse(input);

                        //Naar volgende gaan (beschrijving ofzo)

                        //Lokale lijst opslaan in lijst
                        globalClass.VOORRAAD_SAVE(voorraad);
                        MainView(View.BekijkEditVoorraadList);
                    }
                }
            }
            else
            {
                Console.WriteLine("Item bestaat niet");
                Console.ReadLine();
                MainView(View.BekijkEditVoorraadList);
            }
        }

        static void ShowVoorraad(long id = 0)
        {
            //Voorraad laten zien
            string[] alignment = {
                "-10", //ID
                "-30", //BESCHRIJVING
                "-10", //AANTAL
                "-10", //MAAT
                "-10"  //PRIJS
            };
            Console.WriteLine("{0," + alignment[0] + "}{1," + alignment[1] + "}{2," + alignment[2] + "}{3," + alignment[3] + "}{4," + alignment[4] + "}", "ID", "BESCHRIJVING", "AANTAL", "MAAT", "PRIJS");
            if (id == 0)
            {
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
            else
            {
                bool itemBestaat = voorraad.Any(r => r.ItemID == id);
                if (itemBestaat)
                {
                    var item = voorraad.Find(r => r.ItemID == id);
                    Console.WriteLine("{0," + alignment[0] + "}{1," + alignment[1] + "}{2," + alignment[2] + "}{3," + alignment[3] + "}{4," + alignment[4] + "}",
                                        item.ItemID,
                                        item.ItemDesc,
                                        item.ItemAmount,
                                        item.ItemMaat,
                                        item.ItemPrijs);
                }
                else
                {
                    Console.WriteLine("Item bestaat niet");
                }
            }
        }

        static void WindowLine()
        {
            Console.Write(new string(horPipe, Console.WindowWidth));
        }
        static string ReadLine()
        {
            string input = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == menuKey)
                {
                    return menuMsg;
                }
                if (key.Key == backKey)
                {
                    return backMsg;
                }
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, (input.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            return input;
        }
        static string ReadPassword()
        {
            string input = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == menuKey)
                {
                    return menuMsg;
                }
                if (key.Key == backKey)
                {
                    return backMsg;
                }
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    input += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, (input.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            return input;
        }
        
        enum View
        {
            Login,
            Menu,
            BekijkVoorraad,
            BekijkEditVoorraadList,
            EditVoorraad,
            EXIT
        }
    }
}