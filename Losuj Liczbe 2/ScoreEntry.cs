namespace ZgadnijLiczbe2
{
    // ENKAPSULACJA & ABSTRAKCJA: prywatne setter'y chronią przed modyfikacją po utworzeniu
    public class ScoreEntry
    {
        public string Name { get; private set; }
        public int Attempts { get; private set; }
        public int Difficulty { get; private set; }
        public int DurationSeconds { get; private set; }
        public bool IsNewGamePlus { get; private set; }

        public ScoreEntry(string name, int attempts, int difficulty, int durationSeconds, bool isNewGamePlus = false)
        {
            Name = name;
            Attempts = attempts;
            Difficulty = difficulty;
            DurationSeconds = durationSeconds;
            IsNewGamePlus = isNewGamePlus;
        }
    }
}