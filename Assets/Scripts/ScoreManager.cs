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
public class ScoreManager : MonoBehaviour
{
    Canvas canvas => FindObjectOfType<Canvas>();
    Text scoreText => canvas.GetComponentInChildren<Text>();
    SortedDictionary<int, Score> scoreBoard;
    private Score _score;

    //45mins getting stuck on how to interact with the score thats DontDestroyOnLoad from the player
    void Start()
    {
        _score = new Score();

        scoreBoard = ScoreSaverAndLoader.GetScores();
        if (scoreBoard == null) scoreBoard = new SortedDictionary<int, Score>();

        //if (scoreBoard == null) scoreBoard = ScoreSaverAndLoader.GetScores();
        if (canvas == null) FindObjectOfType<Canvas>();
        if (scoreText == null) canvas.GetComponentInChildren<Text>();
    }

    void Update()
    {
        scoreText.text = "Score: " + _score.score;
    }

    public void IncrementScore()
    {
        _score.score++;
    }
    public void SetName(string name) {
        _score.playerName = name;
    }
    
    public void SaveGame() {
        scoreBoard.Add(_score.score, _score);
        ScoreSaverAndLoader.SaveScores(scoreBoard);
    }
    public void Reset()
    {
        _score.score = 0;
        // reset logic
    }

   
}
