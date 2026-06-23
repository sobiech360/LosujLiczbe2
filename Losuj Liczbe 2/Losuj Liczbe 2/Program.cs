using System;

namespace ZgadnijLiczbe2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("GRA ZALADOWANA - Kliknij ENTER aby przejsc do Menu");
            Console.ReadLine();

            GameSettings ustawienia = new GameSettings();
            ScoreManager manager = new ScoreManager();
            MenuController menu = new MenuController(ustawienia, manager);

            while (true)
            {
                menu.PokazMenuGlowne();
                string wybor = Console.ReadLine();

                if (wybor == "1")
                {
                    menu.WyczyscEkran();
                    Console.WriteLine(ustawienia.GetText("Wybierz poziom:", "Select difficulty:"));
                    Console.WriteLine("1. Latwy (1-50)\n2. Sredni (1-100)\n3. Trudny (1-250)");
                    Console.Write(ustawienia.GetText("Wybor: ", "Choice: "));

                    if (!int.TryParse(Console.ReadLine(), out int poziom) || poziom < 1 || poziom > 3) continue;
                    int zakresMax = poziom == 1 ? 50 : (poziom == 2 ? 100 : 250);

                    bool czyZaklad = false;
                    int limitZakladu = 0;

                    if (ustawienia.AskForBetMode)
                    {
                        Console.Write(ustawienia.GetText("Czy tryb zakladu? (T/N): ", "Bet mode? (Y/N): "));
                        string odp = Console.ReadLine().ToUpper();
                        if (odp == "T" || odp == "Y")
                        {
                            Console.Write(ustawienia.GetText("Podaj limit prob: ", "Enter limit: "));
                            if (int.TryParse(Console.ReadLine(), out limitZakladu)) czyZaklad = true;
                        }
                    }

                    // ========================================================================
                    // ZASADA 4: POLIMORFIZM
                    // Ogólna referencja typu bazowego.
                    // ========================================================================
                    BaseEngine wybranySilnikGry;

                    if (czyZaklad)
                    {
                        wybranySilnikGry = new BetGameEngine(ustawienia, manager, menu, limitZakladu);
                    }
                    else
                    {
                        wybranySilnikGry = new StandardGameEngine(ustawienia, manager, menu);
                    }

                    // ========================================================================
                    // ZASADA 4: POLIMORFIZM
                    // Wywołanie metody w locie.
                    // ========================================================================
                    wybranySilnikGry.UruchomRozgrywke(poziom, zakresMax);
                }
                else if (wybor == "2" && manager.CzySaWyniki()) menu.PokazHallOfFame();
                else if (wybor == "3") menu.OtworzUstawienia();
                else if (wybor == "4") break;
                else menu.Pauza();
            }
        }
    }
}