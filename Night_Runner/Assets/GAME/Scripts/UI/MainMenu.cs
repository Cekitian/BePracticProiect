using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private bool isActive = true;
    private float opacity = 1;
    private void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(PlayerKeybindings.jump))
            {
                isActive = false;
                StartGame();
            }
        }
        else
        {
            if(opacity > 0)
            {
                canvasGroup.alpha = opacity;
                opacity -= Time.deltaTime;

                if(opacity <= 0)
                {
                    opacity = 0;
                    canvasGroup.alpha = opacity;
                }
            }
           
        }
    }
    private void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
