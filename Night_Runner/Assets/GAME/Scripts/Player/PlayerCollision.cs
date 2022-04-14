using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
   [SerializeField] private float collisionDist;
   [SerializeField] private LayerMask obstacleLayer;
   [SerializeField] private LayerMask collectibleLayer;
    private Player playerRef;
    private void Awake()
    {
        playerRef = gameObject.GetComponent<Player>();
        
    }
    private void Update()
    {
        CheckForObstacle();
        CheckForCollectible();

    }
    private void CheckForObstacle()
    {
        bool hitObstacle = Physics.Raycast(gameObject.transform.position, Vector3.forward, out RaycastHit objectHit, 0.75f, obstacleLayer);

        if (hitObstacle)
        {
            if (!playerRef.hasDied)
            {
                playerRef.Die();
            }
            Debug.Log("OUCH");

        }
    }
    private void CheckForCollectible()
    {
        bool hitCollectible = Physics.Raycast(gameObject.transform.position, Vector3.forward,out RaycastHit objectHit, 0.75f, collectibleLayer);

            Debug.Log( hitCollectible);
        if (hitCollectible)
        {
            Debug.Log("HIT COLLECTIBLE");
            objectHit.collider.gameObject.GetComponent<Collectible>().PickUp();
        }
    }
}
