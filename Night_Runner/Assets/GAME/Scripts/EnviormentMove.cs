using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentMove : MonoBehaviour
{
   [SerializeField] private SpriteRenderer floorRenderer;
   [SerializeField] private float speedMultiplier = 1;

    private Vector2 initialSize;
    private Vector3 initialPos;

    private void Awake()
    {
        initialSize = floorRenderer.size;
        initialPos = floorRenderer.gameObject.transform.position;
    }
    private void FixedUpdate()
    {
        if(GameManager.Instance.HasGameStarted())
        MoveSprite();
    }
    private void MoveSprite()
    {
        floorRenderer.size += Vector2.up * Time.fixedDeltaTime * GameManager.Instance.GetObjectSpeed() * speedMultiplier;
        floorRenderer.gameObject.transform.position -= Vector3.forward * Time.fixedDeltaTime * GameManager.Instance.GetObjectSpeed() * speedMultiplier /(2 / gameObject.transform.localScale.x);
    
        if(floorRenderer.size.y > 1000)
        {
            floorRenderer.size = initialSize;
            floorRenderer.gameObject.transform.position = initialPos;
        }
    }
}
