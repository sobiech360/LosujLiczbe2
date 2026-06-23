using System;

namespace ZgadnijLiczbe2
{
    // ========================================================================
    // ZASADA 1: ABSTRAKCJA
    // Moja ogólna baza silnika.
    // ========================================================================
    public abstract class BaseEngine
    {
        protected GameSettings _settings;
        protected ScoreManager _scoreManager;
        protected MenuController _menu;
        protected Random _rand = new Random();

        protected string[] _zaMaloPL = { "Za mala!", "Celujesz za nisko!", "Podaj wieksza liczbe.", "Sproboj wyzej.", "Nadal za malo!" };
        private string[] _zaMaloEN = { "Too small!", "Aim higher!", "The number is bigger.", "Go up.", "Still too low!" };
        protected string[] _zaDuzoPL = { "Za duza!", "Celuj nizej!", "Ukryta liczba jest mniejsza.", "Mniej.", "Zmniejsz wartosc!" };
        private string[] _zaDuzoEN = { "Too big!", "Aim lower!", "The number is smaller.", "Less.", "Lower your guess!" };

        public BaseEngine(GameSettings settings, ScoreManager scoreManager, MenuController menu)
        {
            _settings = settings;
            _scoreManager = scoreManager;
            _menu = menu;
        }

        protected void PokazPodpowiedz(int strzal, int wylosowana)
        {
            int losIndex = _rand.Next(0, 5);
            if (strzal < wylosowana)
                Console.WriteLine(_settings.CurrentLanguage == "PL" ? _zaMaloPL[losIndex] : _zaMaloEN[losIndex]);
            else
                Console.WriteLine(_settings.CurrentLanguage == "PL" ? _zaDuzoPL[losIndex] : _zaDuzoEN[losIndex]);
        }

        public abstract void UruchomRozgrywke(int poziom, int zakresMax);
    }

    // ========================================================================
    // ZASADA 3: DZIEDZICZENIE
    // Przejmuję bazę od BaseEngine.
    // ========================================================================
    public class StandardGameEngine : BaseEngine
    {
        public StandardGameEngine(GameSettings settings, ScoreManager scoreManager, MenuController menu)
            : base(settings, scoreManager, menu) { }

        // ========================================================================
        // ZASADA 4: POLIMORFIZM
        // Nadpisuję logikę standardowej rozgrywki.
        // ========================================================================
        public override void UruchomRozgrywke(int poziom, int zakresMax)
        {
            int wylosowana = _rand.Next(1, zakresMax + 1);
            int liczbaProb = 0;
            DateTime czasStartu = DateTime.Now;

            while (true)
            {
                liczbaProb++;
                Console.Write(_settings.GetText($"\n[Standard] Proba {liczbaProb}: Podaj liczbe: ", $"\n[Standard] Attempt {liczbaProb}: Enter number: "));
                if (!int.TryParse(Console.ReadLine(), out int strzal)) { liczbaProb--; continue; }

                if (strzal == wylosowana)
                {
                    int sekundy = (int)(DateTime.Now - czasStartu).TotalSeconds;
                    Console.WriteLine(_settings.GetText($"\nZgadleś w {liczbaProb} probach! Czas: {sekundy}s", $"\nGuessed in {liczbaProb} attempts! Time: {sekundy}s"));

                    Console.Write(_settings.GetText("Podaj imie: ", "Enter name: "));
                    string imie = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(imie)) imie = "Gracz";

                    _scoreManager.ZapiszWynik(new ScoreEntry(imie, liczbaProb, poziom, sekundy));
                    _menu.Pauza();
                    return;
                }
                else
                {
                    PokazPodpowiedz(strzal, wylosowana);
                }
            }
        }
    }

    // ========================================================================
    // ZASADA 3: DZIEDZICZENIE
    // Druga podklasa dziedzicząca bazę.
    // ========================================================================
    public class BetGameEngine : BaseEngine
    {
        private int _limitProb;

        public BetGameEngine(GameSettings settings, ScoreManager scoreManager, MenuController menu, int limitProb)
            : base(settings, scoreManager, menu)
        {
            _limitProb = limitProb;
        }

        // ========================================================================
        // ZASADA 4: POLIMORFIZM
        // Nadpisuję unikalną logikę zakładu.
        // ========================================================================
        public override void UruchomRozgrywke(int poziom, int zakresMax)
        {
            int wylosowana = _rand.Next(1, zakresMax + 1);
            int liczbaProb = 0;
            DateTime czasStartu = DateTime.Now;

            while (true)
            {
                liczbaProb++;

                if (liczbaProb > _limitProb)
                {
                    Console.WriteLine(_settings.GetText($"\nPrzegrales zaklad! Limit to {_limitProb}.", $"\nYou lost! Limit was {_limitProb}."));
                    _menu.Pauza();
                    return;
                }

                Console.Write(_settings.GetText($"\n[ZAKLAD!] Proba {liczbaProb}/{_limitProb}: Podaj liczbe: ", $"\n[BET MODE!] Attempt {liczbaProb}/{_limitProb}: Enter number: "));
                if (!int.TryParse(Console.ReadLine(), out int strzal)) { liczbaProb--; continue; }

                if (strzal == wylosowana)
                {
                    int sekundy = (int)(DateTime.Now - czasStartu).TotalSeconds;
                    Console.WriteLine(_settings.GetText($"\nWygrales zaklad w {liczbaProb} probach! Czas: {sekundy}s", $"\nYou won the bet in {liczbaProb} attempts! Time: {sekundy}s"));

                    Console.Write(_settings.GetText("Podaj imie: ", "Enter name: "));
                    string imie = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(imie)) imie = "Gracz";

                    _scoreManager.ZapiszWynik(new ScoreEntry(imie, liczbaProb, poziom, sekundy));
                    _menu.Pauza();
                    return;
                }
                else
                {
                    PokazPodpowiedz(strzal, wylosowana);
                }
            }
        }
    }
}