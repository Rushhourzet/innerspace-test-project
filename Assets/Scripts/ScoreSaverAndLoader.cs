using System.Collections.Generic;
using UnityEngine;

//1hr 15mins, First time working with json in C#, still need to make it load when opening and save when closing
//planning on making a whole scoreboard
public static class ScoreSaverAndLoader {
    private const string SAVE_FILE_NAME = "scoreboard.json";

    public static void SaveScores(SortedDictionary<int, Score> scores) {
        string save = JsonUtility.ToJson(scores.Values);
        System.IO.File.WriteAllText(SAVE_FILE_NAME, save);
    }

    public static SortedDictionary<int, Score> GetScores() {
        string jsonString = System.IO.File.ReadAllText(SAVE_FILE_NAME);
        SortedDictionary<int, Score>.ValueCollection score = JsonUtility.FromJson<SortedDictionary<int, Score>.ValueCollection>(jsonString);
        SortedDictionary<int, Score> scores = new SortedDictionary<int, Score>();
        foreach (Score s in score) {
            scores.Add(s.score, s);
        }
        return scores;
    }
}
