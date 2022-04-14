using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CanvasGroup pauseGroup;

    private bool isActive = false;
    private float opacity = 0;

    private float timeScaleNormal = 1;

    private void Awake()
    {
        timeScaleNormal = Time.timeScale;
    }
    private void Update()
    {
        if(Input.GetKeyDown(PlayerKeybindings.pause))
        {
            if(Time.timeScale == timeScaleNormal)
            {
                Time.timeScale = 0;
                pauseGroup.alpha = 1;
            }
            else
            {
                Time.timeScale = timeScaleNormal;
                pauseGroup.alpha = 0;
            }
        }

        ChangeOpacity();
    }
    private void ChangeOpacity()
    {
        if (isActive)
        {
            if (opacity < 1)
            {
                canvasGroup.alpha = opacity;
                opacity += Time.deltaTime;
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
    public void ChangeState(bool newState)
    {
        isActive = newState;
    }
}
