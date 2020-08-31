using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

//3hr after 3hr of trying to get the board global running i give up q.q
public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Text scoreboardBox;
    [SerializeField] private int scoresToDisplay = 20;
    private Score sessionScore;
    private List<Score> scores;
    private bool callbackCheck;
    private ScoreSaverAndLoader.Callback callback => Callback;

    private void Start() {
        if (scores == null) {
            scores = ScoreSaverAndLoader.GetScores(callback);
        }
        if (!callbackCheck) {
            scores = new List<Score>();
        }
    }
    private void Update() {
        DisplayScores();
    }

    private bool Callback(bool callback) {
        callbackCheck = callback;
        return callback;
    }
    public void AddScore(Score score) {
        scores.Add(score);
        scores.Sort((a,b) => b.score - a.score);
    }
    
    public void DisplayScores() {
        string textToDisplay = GenerateText(scores);
        scoreboardBox.text = textToDisplay;
        //display scores;
    }

    private string GenerateText(List<Score> scores) {
        string text = "";
        if (callbackCheck) {
            if(scores.Count >= scoresToDisplay) {
                text += DisplayNumberOfElements(scoresToDisplay);
            } else {
                text += DisplayNumberOfElements(scores.Count);
            }
        }
        else {
            text = "There are no Scores yet.";
        }
        return text;
    }

    private string DisplayNumberOfElements(int loops) {
        string text = "";
        for (int i = 0; i < loops; i++) {
            Score s = scores.ElementAt<Score>(i);
            text += i + ".\t" + s.playerName + "\t" + s.score + "\n";
        }
        return text;
    }

}
