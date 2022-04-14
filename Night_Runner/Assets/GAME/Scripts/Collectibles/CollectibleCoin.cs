using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCoin : Collectible
{
    protected override void OnPickup()
    {
        ScoreCounter.Instance.IncreaseScore(10);
    }
}
