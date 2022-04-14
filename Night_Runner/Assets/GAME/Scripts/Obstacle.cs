using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float objectPoolWeight = 1;
    public LaneRestriction laneRestriction = LaneRestriction.NONE;
    public enum LaneRestriction
    {
        NONE = -1, LEFT = 0, CENTER = 1, RIGHT = 2
    }
    [SerializeField] private Transform collectibleSpawnPoint;
    [SerializeField] private GameObject[] possibleCollectible;
    [Space]
    [SerializeField] private int chanceForCollectible;

    private GameObject spawnedCollectible;
    public void OnSpawn()
    {
        SpawnCollectible();
    }
    public void OnDespawn()
    {
        if(spawnedCollectible != null)
        {
            Destroy(spawnedCollectible);
            spawnedCollectible = null;
        }
    }
    private void SpawnCollectible()
    {
        int chance = Random.Range(1, 101);

        if(chance <= chanceForCollectible)
        {
            int index = Random.Range(0, possibleCollectible.Length);

            GameObject theColl = Instantiate(possibleCollectible[index],Vector3.zero,Quaternion.identity, collectibleSpawnPoint);
            theColl.transform.localPosition = Vector3.zero;     
        }
    }

}
