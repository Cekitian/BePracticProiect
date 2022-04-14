using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] possibleObstacles;
    [SerializeField] private int poolCount;
    [SerializeField] private Transform poolParent;

    private List<GameObject> pooledObstacles = new List<GameObject>();
    private void Awake()
    {
        GeneratePool();
    }
    public GameObject GetObjectFromPool()
    {
        int objIndex = Random.Range(0, pooledObstacles.Count);
        int maxSearchIndex = 30;
        while(pooledObstacles[objIndex].active && maxSearchIndex > 0)
        {
            objIndex = Random.Range(0, pooledObstacles.Count);

            maxSearchIndex--;
        }

        pooledObstacles[objIndex].GetComponent<Obstacle>().OnSpawn();
        pooledObstacles[objIndex].SetActive(true);

        return pooledObstacles[objIndex];
    }
    public void SendObjectToPool(GameObject theObject)
    {
        theObject.transform.parent = poolParent;
        theObject.GetComponent<Obstacle>().OnDespawn();

        theObject.SetActive(false);
    }    
    public void ResetPoolObjects()
    {
        foreach (GameObject x in pooledObstacles)
        {
            x.SetActive(false);
        }
    }
    private void GeneratePool()
    {
        int objCount = poolCount / possibleObstacles.Length;

        for(int i = 0; i < possibleObstacles.Length; i++)
        {
            float objWeight = possibleObstacles[i].GetComponent<Obstacle>().objectPoolWeight;

            for(float j = 1; j <= objCount * objWeight; j++)
            {
                GameObject pooledObject = Instantiate(possibleObstacles[i], poolParent);

                pooledObstacles.Add(pooledObject);
                pooledObject.SetActive(false);
            }
        }
    }
}
