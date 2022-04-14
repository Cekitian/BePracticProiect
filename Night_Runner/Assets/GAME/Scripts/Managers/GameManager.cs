using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static System.Action OnGameStart;
    public static System.Action OnGameStop;

    [SerializeField] private PoolManager poolMangerRef;
    [Space]
    [SerializeField] private GameMenu gameMenu;
    [Space]
    [SerializeField] private Transform[] lanePosRef;
    [SerializeField] private float distanceFromPosRef; 
    [SerializeField] private float distanceBetweenObjects; 
    [SerializeField] private Transform parentOfObjects;
    [SerializeField] [Min(5)] private float objSpeed;
    [SerializeField] private float speedPerSecond;

    private int laneCount;
    private bool gameStarted = false;
    private int initialBatch = 25;
    private float initialObjSpeed; 

    private Vector3 futureCollidersSpawnPos;
    private float timeCounter;

    private int lastObstaclePos = -1;
    private void Awake()
    {
        Instance = this;
        laneCount = lanePosRef.Length;
        initialObjSpeed = objSpeed;
    }
    private void Start()
    {
        //StartGame();
    }
    private void FixedUpdate()
    {
        if (!gameStarted)
            return;

        timeCounter += Time.fixedDeltaTime;

        if(timeCounter > 1)
        {
            objSpeed += speedPerSecond;
            timeCounter = 0;
        }

        MoveObstacles();
    }
    private void OnDrawGizmos()
    {
        foreach(Transform x in lanePosRef)
        {
            Gizmos.DrawLine(x.position, x.position + Vector3.forward * distanceFromPosRef);
        }
    }
    public float GetObjectSpeed()
    {
        return objSpeed;
    }
    public void StartGame()
    {
        poolMangerRef.ResetPoolObjects();

        if (gameStarted)
            return;

        gameStarted = true;
        Player.Instance.hasDied = false;
        gameMenu.ChangeState(true);

        InstantiateInitialBatch();
    }
    public bool HasGameStarted()
    {
        return gameStarted;
    }
    public void StopGame()
    {
        gameStarted = false;
        gameMenu.ChangeState(false);
        ResetObjSpeed();
    }
    public void ChangePauseState()
    {
        gameStarted = !gameStarted;
    }
    public void ChangeObjectLane(LaneObject theObject, int newLane)
    {
        StartCoroutine(MoveObject(theObject, newLane));
    }
    public int GetLaneCount()
    {
        return laneCount;
    }
    public void InstantiateNewObject()
    {
        GameObject x = poolMangerRef.GetObjectFromPool();
        int laneIndex;
        if (x.GetComponent<Obstacle>().laneRestriction == Obstacle.LaneRestriction.NONE)
        {
            laneIndex = Random.Range(0, lanePosRef.Length);
            while (laneIndex == lastObstaclePos)
            {
                laneIndex = Random.Range(0, lanePosRef.Length);
            }
        }
        else
        {
             laneIndex = (int)x.GetComponent<Obstacle>().laneRestriction;

        }
        lastObstaclePos = laneIndex;

        x.transform.parent = parentOfObjects;
        x.transform.position = Vector3.up * futureCollidersSpawnPos.y
            + Vector3.forward * futureCollidersSpawnPos.z
            + Vector3.right * lanePosRef[laneIndex].position.x;
    }

    private void InstantiateInitialBatch()
    {         
        for(int i = 0; i < initialBatch; i++)
        {
            GameObject x = poolMangerRef.GetObjectFromPool();
            int laneIndex;
            if (x.GetComponent<Obstacle>().laneRestriction == Obstacle.LaneRestriction.NONE)
            {
                laneIndex = Random.Range(0, lanePosRef.Length);
                while (laneIndex == lastObstaclePos)
                {
                    laneIndex = Random.Range(0, lanePosRef.Length);
                }
            }
            else
            {
                laneIndex = (int)x.GetComponent<Obstacle>().laneRestriction;

            }
            lastObstaclePos = laneIndex;

            Vector3 pos = lanePosRef[laneIndex].position;

            x.transform.parent = parentOfObjects;
            x.transform.position = pos + Vector3.forward * (distanceFromPosRef + distanceBetweenObjects * i);
            
            if(i == initialBatch -2)
            futureCollidersSpawnPos = x.transform.position;
        }

    }
    private IEnumerator MoveObject(LaneObject theObject,int newLane)
    {
        float increment = 0f;

        Vector3 initPos = theObject.transform.position;
        while(increment < 1)
        {
            theObject.transform.position = new Vector3(Mathf.Lerp(initPos.x,lanePosRef[newLane].position.x,increment), initPos.y, initPos.z);
            Debug.Log(increment);
            increment += Time.fixedDeltaTime * theObject.GetSpeed() * (objSpeed / initialObjSpeed);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        theObject.transform.position = new Vector3(lanePosRef[newLane].position.x, initPos.y, initPos.z);
        yield break;
    }
    private void MoveObstacles()
    {
        parentOfObjects.transform.localPosition -= Vector3.forward * Time.fixedDeltaTime * objSpeed;
    }
    private void ResetObjSpeed()
    {
        objSpeed = initialObjSpeed;
    }
}
