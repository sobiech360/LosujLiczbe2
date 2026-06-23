using System;

namespace ZgadnijLiczbe2
{
    public class GameSettings
    {
        public string CurrentLanguage = "PL";
        public bool AskForBetMode = true;

        public string GetText(string pl, string en)
        {
            if (CurrentLanguage == "PL") return pl;
            return en;
        }
    }
}