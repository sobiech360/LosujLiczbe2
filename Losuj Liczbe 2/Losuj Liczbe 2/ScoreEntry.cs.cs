using System;

namespace ZgadnijLiczbe2
{
    // ========================================================================
    // ZASADA 1: ABSTRAKCJA
    // Mój model obiektu wyniku.
    // ========================================================================
    public class ScoreEntry
    {
        public string Name;
        public int Attempts;
        public int Difficulty;
        public int DurationSeconds;

        public ScoreEntry(string name, int attempts, int difficulty, int durationSeconds)
        {
            Name = name;
            Attempts = attempts;
            Difficulty = difficulty;
            DurationSeconds = durationSeconds;
        }
    }
}