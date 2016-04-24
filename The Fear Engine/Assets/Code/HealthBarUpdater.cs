using UnityEngine;
using System.Collections;

public class HealthBarUpdater : MonoBehaviour
{
    private GameObject healthAnchor;

    private float health; // 0 to maxHealth
    private float maxHealth = 100;

    private bool changed;

    // Use this for initialization
    void Start()
    {
        healthAnchor = transform.FindChild("HealthAnchor").gameObject;
        health = maxHealth;

        changed = true;
    }
    
    void Update()
    {
        if(changed)
        {
            healthAnchor.transform.localScale = new Vector3((health / maxHealth), 1, 1);
            changed = false;
        }

    }

    public void SetHealth(float newHealth)
    {
        health = newHealth;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }
        changed = true;
    }

    public void ChangeMaxHealth(float newMax)
    {
        maxHealth = newMax;
        health = maxHealth;
        changed = true;
    }
}
