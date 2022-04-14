using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    [SerializeField] private PoolManager obstaclePool;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            obstaclePool.SendObjectToPool(other.gameObject);
            GameManager.Instance.InstantiateNewObject();
        }
    }
}
