using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    public PlayerMovement playerMovement;
    public PlayerCollision playerCollision;
    [Space]
    public bool hasDied;
    public bool canDie = true;

    [SerializeField] private LoseMenu loseMenu;
    private void Awake()
    {
        Instance = this;
    }
    public void Die()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (!canDie)
            return;
        
        hasDied = true;
        loseMenu.Lose();
        GameManager.Instance.StopGame();
    }
}
