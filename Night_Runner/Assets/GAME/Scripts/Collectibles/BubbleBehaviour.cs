using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    private IEnumerator Start()
    {
        Player.Instance.canDie = false;
            yield return new WaitForSeconds(5f);
        Player.Instance.canDie = true;

        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        gameObject.transform.localEulerAngles += Vector3.up * Time.fixedDeltaTime * 180;
    }
}
