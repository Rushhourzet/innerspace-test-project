using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Manages the state of the level </summary>
public class ScoreManager : MonoBehaviour
{
    Canvas canvas => FindObjectOfType<Canvas>();
    Text scoreText => canvas.GetComponentInChildren<Text>();
    public int score { get; private set; }
    
    void Start()
    {
        
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public void IncrementScore()
    {
        score++;
    }

    public void Reset()
    {
        score = 0;
        // reset logic
    }
}
