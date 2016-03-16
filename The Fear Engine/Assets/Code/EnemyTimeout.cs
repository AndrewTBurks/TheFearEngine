using UnityEngine;
using System.Collections;

public class EnemyTimeout : MonoBehaviour
{

    void Awake()
    {
        StartCoroutine(SelfTimeoutDestroy());
    }


    IEnumerator SelfTimeoutDestroy()
    {
        yield return new WaitForSeconds(5);
        GameObject.Destroy(gameObject);
    }
}
