using System;

namespace ZgadnijLiczbe2
{
    // ZASADA 3: DZIEDZICZENIE
    public class BetGameEngine : BaseEngine
    {
        private int _limitProb;

        public BetGameEngine(GameSettings settings, ScoreManager scoreManager, MenuController menu, int limitProb)
            : base(settings, scoreManager, menu)
        {
            _limitProb = limitProb;
        }

        // ZASADA 4: POLIMORFIZM
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

                    _scoreManager.ZapiszWynik(new ScoreEntry(imie, liczbaProb, poziom, sekundy, false));
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