using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBubble : Collectible
{
    [SerializeField] private GameObject bubble;
    protected override void OnPickup()
    {
        Instantiate(bubble, Player.Instance.gameObject.transform.position, Quaternion.identity, Player.Instance.gameObject.transform);
    }
}
