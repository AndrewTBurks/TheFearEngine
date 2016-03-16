using UnityEngine;
using System.Collections;

public class HealthBarUpdater : MonoBehaviour
{
    public GameObject healthAnchor;
    public float health; // 0 to 100

    // Use this for initialization
    void Start()
    {
        healthAnchor.transform.localScale = new Vector3(.5f, 1f, 1f);
    }
    
    public void RescaleHealth(float newHealth)
    {
        print("In RescaleHealth");
        health = newHealth;
        healthAnchor.transform.localScale = new Vector3((newHealth / 100f), healthAnchor.transform.localScale.y, healthAnchor.transform.localScale.z);
    }
}
