using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneObject : MonoBehaviour
{
    [SerializeField] protected float laneChangeSpeed;
    protected int currentLane;
    public int GetLane()
    {
        return currentLane;
    }
    public float GetSpeed()
    {
        return laneChangeSpeed;
    }
}
