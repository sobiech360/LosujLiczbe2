using System;

namespace ZgadnijLiczbe2
{
    public class MenuController
    {
        // ========================================================================
        // ZASADA 2: HERMETYZACJA
        // Prywatne, chronione referencje obiektów.
        // ========================================================================
        private GameSettings _settings;
        private ScoreManager _scoreManager;

        public MenuController(GameSettings settings, ScoreManager scoreManager)
        {
            _settings = settings;
            _scoreManager = scoreManager;
        }

        public void WyczyscEkran() => Console.Clear();

        public void Pauza()
        {
            Console.WriteLine(_settings.GetText("\nNacisnij ENTER aby kontynuowac...", "\nPress ENTER to continue..."));
            Console.ReadLine();
        }

        public void PokazMenuGlowne()
        {
            WyczyscEkran();
            Console.WriteLine("   _________________________________________________ ");
            Console.WriteLine("  |                                                 |");
            Console.WriteLine("  |    .-------.    ZGADNIJ LICZBE 2 / GUESS NO. 2  |");
            Console.WriteLine("  |   /   o   /|    ------------------------------  |");
            Console.WriteLine("  |  /_______/ |    By: Mikolaj Sobiech             |");
            Console.WriteLine("  |  |   o   | /                                    |");
            Console.WriteLine("  |  '-------'                                      |");
            Console.WriteLine("  |_________________________________________________|");

            string jezyk = _settings.CurrentLanguage;
            string zaklad = _settings.AskForBetMode ? _settings.GetText("TAK", "YES") : _settings.GetText("NIE", "NO");
            Console.WriteLine($"  |  Jez./Lang: [ {jezyk} ] | Pytaj o zaklad/Bet: [ {zaklad} ]");
            Console.WriteLine("  |_________________________________________________|");
            Console.WriteLine("  |                                                 |");
            Console.WriteLine("  |  [ 1 ] " + _settings.GetText("ROZPOCZNIJ GRE", "START GAME"));

            if (_scoreManager.CzySaWyniki())
                Console.WriteLine("  |  [ 2 ] Hall of Fame (TOP 5)                     |");
            else
                Console.WriteLine("  |  [ X ] " + _settings.GetText("Hall of Fame (Zablokowane)", "Hall of Fame (Locked)"));

            Console.WriteLine("  |  [ 3 ] " + _settings.GetText("USTAWIENIA", "SETTINGS"));
            Console.WriteLine("  |  [ 4 ] " + _settings.GetText("ZAKONCZ PROGRAM", "EXIT PROGRAM"));
            Console.WriteLine("  |_________________________________________________|");
            Console.Write("\n  " + _settings.GetText("Wybor: ", "Choice: "));
        }

        public void OtworzUstawienia()
        {
            while (true)
            {
                WyczyscEkran();
                Console.WriteLine("===== USTAWIENIA / SETTINGS =====");
                Console.WriteLine("1. " + _settings.GetText($"Zmien jezyk (Aktualny: {_settings.CurrentLanguage})", $"Change Language (Current: {_settings.CurrentLanguage})"));
                Console.WriteLine("2. " + _settings.GetText($"Pytaj o tryb zakladu (Aktualnie: {(_settings.AskForBetMode ? "TAK" : "NIE")})", $"Ask for Bet Mode (Current: {(_settings.AskForBetMode ? "YES" : "NO")})"));
                Console.WriteLine("3. " + _settings.GetText("Wyczysc Hall of Fame", "Clear Hall of Fame"));
                Console.WriteLine("4. " + _settings.GetText("Powrot", "Back"));
                Console.Write(_settings.GetText("Wybor: ", "Choice: "));

                string wybor = Console.ReadLine();
                if (wybor == "1")
                {
                    _settings.CurrentLanguage = (_settings.CurrentLanguage == "PL") ? "EN" : "PL";
                }
                else if (wybor == "2")
                {
                    _settings.AskForBetMode = !_settings.AskForBetMode;
                }
                else if (wybor == "3")
                {
                    Console.Write(_settings.GetText("Czy na pewno chcesz wyczyscic wyniki? (T/N): ", "Are you sure? (Y/N): "));
                    string potw = Console.ReadLine().ToUpper();
                    if (potw == "T" || potw == "Y")
                    {
                        _scoreManager.WyczyscWyniki();
                        Console.WriteLine(_settings.GetText("Wyczyszczono!", "Cleared!"));
                        Pauza();
                    }
                }
                else if (wybor == "4") break;
            }
        }

        public void PokazHallOfFame()
        {
            int filtrPoziomu = 1;
            while (true)
            {
                WyczyscEkran();
                Console.WriteLine("===== Hall of Fame =====");
                Console.WriteLine(_settings.GetText($"Poziom: {ZwrocNazwePoziomu(filtrPoziomu)}", $"Difficulty: {ZwrocNazwePoziomu(filtrPoziomu)}"));
                Console.WriteLine("------------------------------------");

                ScoreEntry[] top5 = _scoreManager.PobierzTop5(filtrPoziomu);
                int limit = top5.Length > 5 ? 5 : top5.Length;

                if (limit == 0)
                {
                    Console.WriteLine(_settings.GetText("Brak wynikow.", "No records."));
                }
                else
                {
                    for (int i = 0; i < limit; i++)
                    {
                        Console.WriteLine($"{i + 1}. {top5[i].Name} | " +
                            _settings.GetText("Proby: ", "Attempts: ") + $"{top5[i].Attempts} | " +
                            _settings.GetText("Czas: ", "Time: ") + $"{top5[i].DurationSeconds}s");
                    }
                }

                Console.WriteLine("------------------------------------");
                Console.WriteLine(_settings.GetText("1-3. Zmien poziom | 4. Powrot", "1-3. Change difficulty | 4. Back"));
                Console.Write(_settings.GetText("Wybor: ", "Choice: "));

                string wybor = Console.ReadLine();
                if (wybor == "1") filtrPoziomu = 1;
                else if (wybor == "2") filtrPoziomu = 2;
                else if (wybor == "3") filtrPoziomu = 3;
                else if (wybor == "4") break;
            }
        }

        private string ZwrocNazwePoziomu(int p)
        {
            if (p == 1) return _settings.GetText("Latwy (1-50)", "Easy (1-50)");
            if (p == 2) return _settings.GetText("Sredni (1-100)", "Medium (1-100)");
            return _settings.GetText("Trudny (1-250)", "Hard (1-250)");
        }
    }
}