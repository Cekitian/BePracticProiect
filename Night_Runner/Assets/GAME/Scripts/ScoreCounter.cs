using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int currentScore = 0;
    private int highScore;
    private float timeCounter;

    private void Awake()
    {
        Instance = this;

        SetHighScore();
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.HasGameStarted())
        {
            currentScore = 0;
            scoreText.text = currentScore.ToString();

            return;
        }

        timeCounter += Time.fixedDeltaTime;
        if(timeCounter >= 0.5f)
        {
            timeCounter = 0;
            IncreaseScore(1);

        }
    }
    public void IncreaseScore(int amountAdded)
    {
        currentScore += amountAdded;

        scoreText.text = currentScore.ToString();

        if (currentScore >= highScore)
        UpdateHighScore();
    }
    public int GetScore()
    {
        return currentScore;
    }
    public int GetHighScore()
    {
        return highScore;
    }
    private void SetHighScore()
    {
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", 0);
            highScore = 0;
        }
        else
        {
            highScore = PlayerPrefs.GetInt("Highscore");
        }

        highScoreText.text = highScore.ToString();

    }
    private void UpdateHighScore()
    {
        PlayerPrefs.SetInt("Highscore", currentScore);
        highScore = currentScore;

        highScoreText.text = highScore.ToString();
    }
}
