using UnityEngine;
using System.Collections;

public class EnemyHealthSystem : MonoBehaviour {
    public GameObject healthBar;     // prefab health bar to use
    private Vector3 slamDistance;
    private GameObject myHealthBar;
    private HealthBarUpdater hbu;
    private float myHealth;
    private float atkDamage = -10; //change number here for more damage.
    private Rigidbody rb;
    // Use this for initialization
    void Start ()
    {
        myHealth = 100;
        myHealthBar = Instantiate(healthBar);
        hbu = myHealthBar.GetComponent<HealthBarUpdater>();
        myHealthBar.transform.position = gameObject.transform.position + (Vector3.up * 2f);
        myHealthBar.transform.parent = gameObject.transform;
        rb = GetComponent<Rigidbody>();
        // scale once to start
        hbu.SetHealth(myHealth);
    }
	
	// Update is called once per frame
	void Update ()
    {
        myHealthBar.transform.LookAt(Camera.main.transform);
        if (myHealth <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    /* Add Positive of Negative amount of health to this Enemy */
    public void AddHealth(float deltaHealth)
    {
        // increment local health val and scale the bar
        myHealth = myHealth + deltaHealth;
        if (myHealth > 100)
        {
            myHealth = 100;
        }
        else if (myHealth < 0)
        {
            myHealth = 0;
        }
        hbu.SetHealth(myHealth);
    }

    void OnTriggerEnter(Collider other)
    {
        // other's weapon script
        WeaponBehavior wb = other.gameObject.GetComponent<WeaponBehavior>();

        if (other.tag == "PickUp" && wb.isAttacking && !wb.enemyHit)
        {
            AddHealth(atkDamage);
            wb.enemyHit = true;
            
        }
        if (other.tag == "PickUp" && wb.isPushing && !wb.enemyHit)
        {
            AddHealth(atkDamage);
            rb.AddForce(transform.forward * -1000);
            rb.AddForce(transform.up * 1000);
            wb.enemyHit = true;

        }
    }

    IEnumerator DamageCooldown(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void OnDestroy()
    {
        Destroy(myHealthBar);
    }

    
}
