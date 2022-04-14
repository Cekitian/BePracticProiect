using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI highscore;

    private bool isActive = false;
    private float opacity = 0;
    private void Update()
    {
        if (isActive)
        {
            if (opacity < 1)
            {
                canvasGroup.alpha = opacity;
                opacity += Time.deltaTime;
            }

            if (Input.GetKeyDown(PlayerKeybindings.jump))
            {
                isActive = false;
                RestartGame();
            }
        }
        else
        {
            if (opacity > 0)
            {
                canvasGroup.alpha = opacity;
                opacity -= Time.deltaTime;

                if (opacity < 0)
                {
                    opacity = 0;
                    canvasGroup.alpha = opacity;
                }
            }

        }
    }
    public void Lose()
    {
        isActive = true;
        score.text = ScoreCounter.Instance.GetScore().ToString();
        highscore.text = ScoreCounter.Instance.GetHighScore().ToString();
    }
    private void RestartGame()
    {
        GameManager.Instance.StartGame();
    }
}
