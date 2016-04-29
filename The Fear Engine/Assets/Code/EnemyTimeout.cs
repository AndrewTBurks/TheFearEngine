using UnityEngine;
using System.Collections;

public class EnemyTimeout : MonoBehaviour
{
    public float waitTime = 5;

    void Awake()
    {
        StartCoroutine(SelfTimeoutDestroy());
    }


    IEnumerator SelfTimeoutDestroy()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
