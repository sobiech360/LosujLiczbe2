using System;

namespace ZgadnijLiczbe2
{
    // ZASADA 3: DZIEDZICZENIE
    public class NewGamePlusEngine : BaseEngine
    {
        public NewGamePlusEngine(GameSettings settings, ScoreManager scoreManager, MenuController menu)
            : base(settings, scoreManager, menu) { }

        // ZASADA 4: POLIMORFIZM (Przelosowanie co 6/7/8 strzałów, brak zakładu)
        public override void UruchomRozgrywke(int poziom, int zakresMax)
        {
            int wylosowana = _rand.Next(1, zakresMax + 1);
            int liczbaProb = 0;
            DateTime czasStartu = DateTime.Now;

            int interwalPrzelosowania = _rand.Next(6, 9);
            int strzalyOdPrzelosowania = 0;

            Console.WriteLine(_settings.GetText("\n--- NOWA GRA PLUS (Liczba zmienia sie w trakcie!) ---", "\n--- NEW GAME PLUS MODE ---"));

            while (true)
            {
                liczbaProb++;
                strzalyOdPrzelosowania++;

                if (strzalyOdPrzelosowania >= interwalPrzelosowania)
                {
                    wylosowana = _rand.Next(1, zakresMax + 1);
                    strzalyOdPrzelosowania = 0;
                    interwalPrzelosowania = _rand.Next(6, 9);
                    Console.WriteLine(_settings.GetText("\n[NG+] Liczba zostala przelosowana!", "\n[NG+] The number has been reshuffled!"));
                }

                Console.Write(_settings.GetText($"\n[NG+] Proba {liczbaProb}: Podaj liczbe: ", $"\n[NG+] Attempt {liczbaProb}: Enter number: "));
                if (!int.TryParse(Console.ReadLine(), out int strzal))
                {
                    liczbaProb--;
                    strzalyOdPrzelosowania--;
                    continue;
                }

                if (strzal == wylosowana)
                {
                    int sekundy = (int)(DateTime.Now - czasStartu).TotalSeconds;
                    Console.WriteLine(_settings.GetText($"\nWygrana w NG+ w {liczbaProb} probach! Czas: {sekundy}s", $"\nWon NG+ in {liczbaProb} attempts! Time: {sekundy}s"));

                    Console.Write(_settings.GetText("Podaj imie: ", "Enter name: "));
                    string imie = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(imie)) imie = "Gracz";

                    _scoreManager.ZapiszWynik(new ScoreEntry(imie, liczbaProb, poziom, sekundy, true));
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