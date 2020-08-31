using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Score {
    public int placement;
    public string playerName;
    public int score;
}

/// <summary> Manages the state of the level </summary>
public class SessionScore : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Text scoreText;
    private Score _score;

    //45mins getting stuck on how to interact with the score thats DontDestroyOnLoad from the player => made a new Scoreboard class that loads scores when loocking at scoreboard
    public void Start(string name) {
    }
    private void Start() {
        if (canvas == null) canvas = FindObjectOfType<Canvas>();
        if (scoreText == null) scoreText = canvas.GetComponentInChildren<Text>();

        _score = new Score();
    }

    private void Update() {
        scoreText.text = "Score: " + _score.score;
    }

    public void IncrementScore()
    {
        _score.score++;
    }
    public void SetName(string name) {
        _score.playerName = name;
    }

    public void SaveScore() {
        ScoreSaverAndLoader.SaveScore(_score);
    }
    
    public void Reset()
    {
        _score.score = 0;
        // reset logic
    }

   
}
