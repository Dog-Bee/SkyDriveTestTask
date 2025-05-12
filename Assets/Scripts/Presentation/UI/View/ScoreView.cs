using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI asteroidPassText;
    [SerializeField] private TextMeshProUGUI highScoreText;


    public void SetFlightTime(string formattedTime)
    {
        timeText.text = $"Time: \n{formattedTime}";
    }

    public void SetScore(int score, int highScore)
    {
        scoreText.text = $"Score: \n{score}";
        highScoreText.text = $"High Score: \n{highScore}";
    }

    public void SetAsteroidPassText(int count)
    {
        asteroidPassText.text = $"Asteroid Pass: \n{count}";
    }
}
