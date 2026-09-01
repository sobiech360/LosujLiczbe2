using System;

namespace ZgadnijLiczbe2
{
    // ZASADA 1: ABSTRAKCJA
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
}