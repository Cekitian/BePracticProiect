using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrient : MonoBehaviour
{
    [SerializeField] private Vector3 constantRotationOffset;
    
    private GameObject cameraRef;
    private Transform child;

    private void Start()
    {
        cameraRef = CameraManager.Instance.gameObject;

        GameObject newChild = new GameObject();
        newChild.transform.parent = gameObject.transform;
        newChild.transform.position = Vector3.zero;

        child = newChild.transform;
    }
    private void Update()
    {
        child.LookAt(cameraRef.transform);
        gameObject.transform.eulerAngles = child.eulerAngles + constantRotationOffset;
    }
}
