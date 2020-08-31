using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

//1hr 15mins, First time working with json in C#, still need to make it load when opening and save when closing
//planning on making a whole scoreboard
// not sure if that saving works, couldnt test it yet
public static class ScoreSaverAndLoader {
    private const string SAVE_FILE_NAME = "scoreboard.json";
    public delegate bool Callback(bool callbackValue);
    public static void SaveScore(Score score) {
        List<Score> scores = GetScores();
        if(scores == null) {
            scores = new List<Score>();
        }
        scores.Add(score);
        scores.Sort((a, b) => b.score - a.score);
        SaveScores(scores);
        
    }
    public static void SaveScores(List<Score> scores) {
        string save = JsonUtility.ToJson(scores);
        System.IO.File.WriteAllText(SAVE_FILE_NAME, save);
    }

    public static List<Score> GetScores(Callback callback) {
        string jsonString = System.IO.File.ReadAllText(SAVE_FILE_NAME);
        List<Score> scores = JsonUtility.FromJson<List<Score>>(jsonString);
        if(scores != null) {
            callback(true);
        }
        else {
            callback(false);
        }
        return scores;
    }
    public static List<Score> GetScores() {
        string jsonString = System.IO.File.ReadAllText(SAVE_FILE_NAME);
        List<Score> scores = JsonUtility.FromJson<List<Score>>(jsonString);
        return scores;
    }
}
