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
        static bool exit = false;

        const char horPipe = (char)9552;

        static Global globalClass = new Global();

        static void Main(string[] args)
        {
            if (args.Length>0)
            {
                if (args[0] == "-gui")
                {
                    //Console verbergen
                    var handle = GetConsoleWindow();
                    ShowWindow(handle, SW_HIDE);
                    //Form starten
                    Application.EnableVisualStyles();
                    //Login loginForm = new Login();
                    //loginForm.Show();
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
            if (exit) { return; }
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
                Console.WriteLine(headerText);
                WindowLine();
                Console.Write("\n");
            }
        }

        static void MainView(View view, string prevInput = "")
        {
            if (exit) { return; }
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
                    case View.EditVoorraadMenu:
                        VoorraadMenu();
                        break;
                    case View.ArtikelToevoegen:
                        ArtikelToevoegen();
                        break;
                    case View.ArtikelVerwijderen:
                        ArtikelVerwijderen();
                        break;
                    case View.EditVoorraad:
                        VoorraadBewerken(Int64.Parse(prevInput));
                        break;
                    case View.EditAantal:
                        BewerkAantal(Int64.Parse(prevInput));
                        break;
                    case View.EditBeschrijving:
                        BewerkBeschrijving(Int64.Parse(prevInput));
                        break;
                    case View.EditMaat:
                        BewerkMaat(Int64.Parse(prevInput));
                        break;
                    case View.EditPrijs:
                        BewerkPrijs(Int64.Parse(prevInput));
                        break;
                    case View.EXIT:
                        exit = true;
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
            if (exit) { return; }
            Header("Login");
            Console.Write("Username: ");
            DisplayLoginPassword(tries, ReadLine());
        }

        static void DisplayLoginPassword(int tries = 0, string username = "")
        {
            if (exit) { return; }
            if (username == string.Empty)
            {
                DisplayLoginUsername(tries);
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
            if (exit) { return; }
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
                MainView(View.EditVoorraadMenu);
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
            if (exit) { return; }
            Header("Bekijk voorraad");
            voorraad = globalClass.VOORRAAD_LOAD();
            ShowVoorraad();
            Console.ReadKey();
            MainView(View.Menu);
        }

        static void VoorraadBewerkenLijst()
        {
            if (exit) { return; }
            Header("Voorraad bewerken");
            voorraad = globalClass.VOORRAAD_LOAD();

            ShowVoorraad();
            WindowLine();
            Console.WriteLine("Typ het ID in van het item dat je wilt aanpassen");
            string keuze = ReadLine();
            if (keuze == menuMsg)
            {
                MainView(View.Menu);
            }
            else if (keuze == backMsg)
            {
                MainView(View.EditVoorraadMenu);
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

        static void VoorraadMenu()
        {
            if (exit) { return; }
            Header("Voorraad menu");
            Console.WriteLine("\t1. Artikel toevoegen" +
            "\n\t2. Artikel bewerken" +
            "\n\t3. Ariktel verwijderen");
            //Bewerken van de andere toevoegen
            Console.Write("\nKeuze: ");
            string input = ReadLine();
            if (input == menuMsg || input == backMsg)
            {
                MainView(View.Menu);
            }
            else if (input == "1")
            {
                //Naar artikel toevoegen gaan
                MainView(View.ArtikelToevoegen);
            }
            else if (input == "2")
            {
                //Naar artikel bewerken gaan
                MainView(View.BekijkEditVoorraadList);
            }
            else if (input == "3")
            {
                //Naar artikel verwijderen gaan
                MainView(View.ArtikelVerwijderen);
            }
            else
            {
                MainView(View.EditVoorraadMenu);
            }
        }

        static void VoorraadBewerken(long id)
        {
            if (exit) { return; }
            Header("Bewerken");
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                var item = voorraad.Find(r => r.ItemID == id);
                Header(item.ItemDesc+" bewerken");
                ShowVoorraad(id);
                WindowLine();
                Console.WriteLine("\t1. Beschrijving bewerken" +
                "\n\t2. Aantal bewerken" +
                "\n\t3. Maat bewerken" +
                "\n\t4. Prijs bewerken");
                //Bewerken van de andere toevoegen
                Console.Write("\nKeuze: ");
                string input = ReadLine();
                if (input == menuMsg)
                {
                    MainView(View.Menu);
                }
                else if (input == backMsg)
                {
                    MainView(View.BekijkEditVoorraadList);
                }
                else if (input == "1")
                {
                    //Naar beschrijving bewerken gaan
                    MainView(View.EditBeschrijving, id.ToString());
                }
                else if (input == "2")
                {
                    //Naar aantal bewerken gaan
                    MainView(View.EditAantal, id.ToString());
                }
                else if (input == "3")
                {
                    //Naar maat bewerken gaan
                    MainView(View.EditMaat, id.ToString());
                }
                else if (input == "4")
                {
                    //Naar maat bewerken gaan
                    MainView(View.EditPrijs, id.ToString());
                }
                else
                {
                    MainView(View.EditVoorraad, id.ToString());
                }
            }
            else
            {
                Console.WriteLine("Item bestaat niet");
                Console.ReadLine();
                MainView(View.BekijkEditVoorraadList);
            }
        }
        
        static void ArtikelVerwijderen()

        {
            if (exit) { return; }
            Header("Artikel verwijderen");

            voorraad = globalClass.VOORRAAD_LOAD();

            ShowVoorraad();
            WindowLine();
            Console.WriteLine("Typ het ID in van het item dat je wilt verwijderen");
            string keuze = ReadLine();
            if (keuze == menuMsg)
            {
                MainView(View.Menu);
            }
            else if (keuze == backMsg)
            {
                MainView(View.EditVoorraadMenu);
            }
            else if (keuze == string.Empty)
            {
                MainView(View.ArtikelVerwijderen);
            }
            else if (keuze == "*")
            {
                voorraad = new List<Voorraad>();
                globalClass.VOORRAAD_SAVE(voorraad);
                MainView(View.EditVoorraadMenu);
            }
            else
            {
                bool bestaatItem = voorraad.Any(r => r.ItemID == Int64.Parse(keuze));
                if (bestaatItem)
                {
                    voorraad.RemoveAll(x => x.ItemID == Int64.Parse(keuze));
                    globalClass.VOORRAAD_SAVE(voorraad);
                    MainView(View.ArtikelVerwijderen, keuze);
                }
                else
                {
                    Console.WriteLine("\n\nItem bestaat niet");
                    Console.ReadLine();
                    MainView(View.ArtikelVerwijderen);
                }
            }
        }

        static void BewerkAantal(long id)
        {
            if (exit) { return; }
            Header("Aantal bewerken");
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                var item = voorraad.Find(r => r.ItemID == id);
                Header(item.ItemDesc + " aantal bewerken");
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
                    MainView(View.EditVoorraad, id.ToString());
                }
                else if (input == string.Empty)
                {
                    MainView(View.EditVoorraad, id.ToString());
                }
                else
                {
                    if (item.ItemAmount + Int64.Parse(input) < 0)
                    {
                        Console.WriteLine("Er kunnen er niet meer weg zijn dan dat er beschikbaar is. Er zijn er maar " + item.ItemAmount + " beschikbaar.");
                        Console.ReadLine();
                        MainView(View.EditAantal, id.ToString());
                    }
                    else
                    {
                        //Veranderen in lokale lijst
                        item.ItemAmount += Int64.Parse(input);
                        //Lokale lijst opslaan in lijst
                        globalClass.VOORRAAD_SAVE(voorraad);
                        MainView(View.EditVoorraad, id.ToString());
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

        static void BewerkBeschrijving(long id)
        {
            if (exit) { return; }
            Header("Beschrijving bewerken");
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                var item = voorraad.Find(r => r.ItemID == id);
                Header(item.ItemDesc + " beschrijving bewerken");
                ShowVoorraad(id);
                WindowLine();
                Console.WriteLine("Huidige beschrijving: " + item.ItemDesc);
                Console.WriteLine();
                Console.Write("Nieuwe beschrijving: ");
                string input = ReadLine();
                if (input == menuMsg)
                {
                    MainView(View.Menu);
                }
                else if (input == backMsg)
                {
                    MainView(View.EditVoorraad, id.ToString());
                }
                else if (input == string.Empty)
                {
                    MainView(View.EditVoorraad, id.ToString());
                }
                else
                {
                    //Veranderen in lokale lijst
                    item.ItemDesc = input;
                    //Lokale lijst opslaan in lijst
                    globalClass.VOORRAAD_SAVE(voorraad);
                    MainView(View.EditVoorraad, id.ToString());
                }
            }
            else
            {
                Console.WriteLine("Item bestaat niet");
                Console.ReadLine();
                MainView(View.BekijkEditVoorraadList);
            }
        }

        static void BewerkMaat(long id)
        {
            if (exit) { return; }
            Header("Maat bewerken");
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                var item = voorraad.Find(r => r.ItemID == id);
                Header(item.ItemDesc + " maat bewerken");
                ShowVoorraad(id);
                WindowLine();
                Console.WriteLine("Huidige maat: " + item.ItemMaat);
                Console.WriteLine();
                Console.Write("Nieuwe maat: ");
                string input = ReadLine();
                if (input == menuMsg)
                {
                    MainView(View.Menu);
                }
                else if (input == backMsg)
                {
                    MainView(View.EditVoorraad, id.ToString());
                }
                else if (input == string.Empty)
                {
                    MainView(View.EditVoorraad, id.ToString());
                }
                else
                {
                    //Veranderen in lokale lijst
                    item.ItemMaat = Int64.Parse(input);
                    //Lokale lijst opslaan in lijst
                    globalClass.VOORRAAD_SAVE(voorraad);
                    MainView(View.EditVoorraad, id.ToString());
                }
            }
            else
            {
                Console.WriteLine("Item bestaat niet");
                Console.ReadLine();
                MainView(View.BekijkEditVoorraadList);
            }
        }

        static void BewerkPrijs(long id)
        {
            if (exit) { return; }
            Header("Prijs bewerken");
            bool itemBestaat = voorraad.Any(r => r.ItemID == id);
            if (itemBestaat)
            {
                var item = voorraad.Find(r => r.ItemID == id);
                Header(item.ItemDesc + " prijs bewerken");
                ShowVoorraad(id);
                WindowLine();
                Console.WriteLine("Huidige prijs: " + item.ItemPrijs);
                Console.WriteLine();
                Console.Write("Nieuwe prijs: ");
                string input = ReadLine();
                if (input == menuMsg)
                {
                    MainView(View.Menu);
                }
                else if (input == backMsg)
                {
                    MainView(View.EditVoorraad, id.ToString());
                }
                else if (input == string.Empty)
                {
                    MainView(View.EditVoorraad, id.ToString());
                }
                else
                {
                    // , vervangen door .
                    input = input.Replace(',', '.');
                    //Veranderen in lokale lijst
                    item.ItemPrijs = input;
                    //Lokale lijst opslaan in lijst
                    globalClass.VOORRAAD_SAVE(voorraad);
                    MainView(View.EditVoorraad, id.ToString());
                }
            }
            else
            {
                Console.WriteLine("Item bestaat niet");
                Console.ReadLine();
                MainView(View.BekijkEditVoorraadList);
            }
        }

        static void ArtikelToevoegen()
        {
            if (exit) { return; }
            voorraad = globalClass.VOORRAAD_LOAD();
            bool invulLoop = false;
            bool opslaan = false;
            int index = 0;
            Voorraad temp = new Voorraad();
            while (!invulLoop)
            {
                Header("Artikel Toevoegen");
                if (voorraad.Count <=0)
                {
                    temp.ItemID = 1;
                }
                else
                {
                    temp.ItemID = voorraad[voorraad.Count - 1].ItemID + 1;
                }
                Console.WriteLine("\tID: " + temp.ItemID+
                    "\n\tBeschrijving: " + temp.ItemDesc +
                    "\n\tAantal: " + temp.ItemAmount +
                    "\n\tMaat: " + temp.ItemMaat +
                    "\n\tPrijs: " + temp.ItemPrijs);
                switch (index)
                {
                    case 0:
                        Console.Write("\nBeschrijving: ");
                        break;
                    case 1:
                        Console.Write("\nAantal: ");
                        break;
                    case 2:
                        Console.Write("\nMaat: ");
                        break;
                    case 3:
                        Console.Write("\nPrijs: ");
                        break;
                    default:
                        invulLoop = true;
                        MainView(View.Menu);
                        break;
                }
                string input = ReadLine();
                if (input == menuMsg)
                {
                    MainView(View.Menu);
                    break;
                }
                else if (input == backMsg)
                {
                    MainView(View.EditVoorraadMenu);
                    break;
                }
                else
                {
                    switch (index)
                    {
                        case 0:
                            temp.ItemDesc = input;
                            index++;
                            break;
                        case 1:
                            temp.ItemAmount = Int64.Parse(input);
                            index++;
                            break;
                        case 2:
                            temp.ItemMaat = Int64.Parse(input);
                            index++;
                            break;
                        case 3:
                            temp.ItemPrijs = input.Replace(',','.');
                            invulLoop = true;
                            opslaan = true;
                            break;
                        default:
                            invulLoop = true;
                            MainView(View.Menu);
                            break;
                    }
                }
            }
            if (opslaan)
            {
                //Opslaan in lokale lijst
                voorraad.Add(temp);
                //Lokale lijst opslaan in lijst
                globalClass.VOORRAAD_SAVE(voorraad);
                MainView(View.EditVoorraadMenu);
            }
            else
            {
                MainView(View.EditVoorraadMenu);
            }
        }

        static void ShowVoorraad(long id = 0)
        {
            if (exit) { return; }
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
            ArtikelToevoegen,
            ArtikelVerwijderen,
            EditVoorraadMenu,
            EditVoorraad,
            EditAantal,
            EditBeschrijving,
            EditMaat,
            EditPrijs,
            EXIT
        }
    }
}