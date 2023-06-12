using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static int _deathScore;
    private static float _timeScore;

    private static class HighScoreData
    {
        public static int DeathScore { get; set; }
        public static float TimeScore { get; set; }
        public static bool HasCompleted { get; set; }
    }

    public static void SetDeathScore(int deathCount)
    {
         = deathCount;
    }




}
