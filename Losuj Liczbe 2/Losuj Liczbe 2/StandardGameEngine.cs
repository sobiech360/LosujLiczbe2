using System;

namespace ZgadnijLiczbe2
{
    // ZASADA 3: DZIEDZICZENIE
    public class StandardGameEngine : BaseEngine
    {
        public StandardGameEngine(GameSettings settings, ScoreManager scoreManager, MenuController menu)
            : base(settings, scoreManager, menu) { }

        // ZASADA 4: POLIMORFIZM
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