using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public void PickUp()
    {
        OnPickup();
        DestroyPickup();
    }
    protected abstract void OnPickup();
    private void DestroyPickup()
    {
        Destroy(gameObject);
    }


}
