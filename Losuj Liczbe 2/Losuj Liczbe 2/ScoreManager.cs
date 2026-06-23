using System;
using System.IO;

namespace ZgadnijLiczbe2
{
    public class ScoreManager
    {
        // ========================================================================
        // ZASADA 2: HERMETYZACJA
        // Ukrywam ścieżkę pliku tekstowego.
        // ========================================================================
        private const string FilePath = "scores.txt";

        public bool CzySaWyniki()
        {
            if (!File.Exists(FilePath)) return false;
            return File.ReadAllLines(FilePath).Length > 0;
        }

        public void ZapiszWynik(ScoreEntry entry)
        {
            string linia = $"{entry.Name};{entry.Attempts};{entry.Difficulty};{entry.DurationSeconds}";
            File.AppendAllText(FilePath, linia + Environment.NewLine);
        }

        public void WyczyscWyniki()
        {
            if (File.Exists(FilePath)) File.Delete(FilePath);
        }

        public ScoreEntry[] PobierzTop5(int poziomTrudnosci)
        {
            if (!File.Exists(FilePath)) return new ScoreEntry[0];

            string[] linie = File.ReadAllLines(FilePath);
            int licznik = 0;

            foreach (string linia in linie)
            {
                if (string.IsNullOrWhiteSpace(linia)) continue;
                string[] czesci = linia.Split(';');
                if (int.Parse(czesci[2]) == poziomTrudnosci) licznik++;
            }

            ScoreEntry[] wyniki = new ScoreEntry[licznik];
            int indeks = 0;

            foreach (string linia in linie)
            {
                if (string.IsNullOrWhiteSpace(linia)) continue;
                string[] czesci = linia.Split(';');
                int poziom = int.Parse(czesci[2]);

                if (poziom == poziomTrudnosci)
                {
                    wyniki[indeks] = new ScoreEntry(
                        czesci[0], int.Parse(czesci[1]), poziom, int.Parse(czesci[3])
                    );
                    indeks++;
                }
            }

            for (int i = 0; i < wyniki.Length; i++)
            {
                for (int j = i + 1; j < wyniki.Length; j++)
                {
                    bool zamien = false;

                    if (wyniki[j].Attempts < wyniki[i].Attempts)
                    {
                        zamien = true;
                    }
                    else if (wyniki[j].Attempts == wyniki[i].Attempts && wyniki[j].DurationSeconds < wyniki[i].DurationSeconds)
                    {
                        zamien = true;
                    }

                    if (zamien)
                    {
                        ScoreEntry temp = wyniki[i];
                        wyniki[i] = wyniki[j];
                        wyniki[j] = temp;
                    }
                }
            }
            return wyniki;
        }
    }
}