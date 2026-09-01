namespace ZgadnijLiczbe2
{
    public class GameSettings
    {
        // ENKAPSULACJA: właściwości zarządzające stanem ustawień
        public string CurrentLanguage { get; set; } = "PL";
        public bool AskForBetMode { get; set; } = true;

        public string GetText(string pl, string en)
        {
            return CurrentLanguage == "PL" ? pl : en;
        }
    }
}