using UnityEngine;
using System.Collections;

public class HealthBarUpdater : MonoBehaviour
{
    private GameObject healthAnchor;

    private float health = 100; // 0 to 100
    private bool changed;

    // Use this for initialization
    void Start()
    {
        healthAnchor = transform.FindChild("HealthAnchor").gameObject;
        changed = true;
    }
    
    void Update()
    {
        if(changed)
        {
            healthAnchor.transform.localScale = new Vector3((health / 100f), 1, 1);
            changed = false;
        }

    }

    public void SetHealth(float newHealth)
    {
        if (health >= 0 && health <= 100)
        {
            health = newHealth;
            changed = true;
        }
    }
}
