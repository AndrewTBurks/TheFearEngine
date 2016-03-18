using UnityEngine;
using System.Collections;

public class EnemyHealthSystem : MonoBehaviour {
    public GameObject healthBar;     // prefab health bar to use

    private GameObject myHealthBar;
    private HealthBarUpdater hbu;
    private float myHealth;
    private float atkDamage = -10; //change number here for more damage.

    // Use this for initialization
    void Start ()
    {
        myHealth = 100;
        myHealthBar = Instantiate(healthBar);
        hbu = myHealthBar.GetComponent<HealthBarUpdater>();
        myHealthBar.transform.position = gameObject.transform.position + (Vector3.up * 2f);
        myHealthBar.transform.parent = gameObject.transform;

        // scale once to start
        hbu.SetHealth(myHealth);
    }
	
	// Update is called once per frame
	void Update ()
    {
        myHealthBar.transform.LookAt(Camera.main.transform);
        if (myHealth == 0)
        {
            Destroy(this.gameObject);
        }
	}

    /* Add Positive of Negative amount of health to this Enemy */
    public void AddHealth(float deltaHealth)
    {
        // increment local health val and scale the bar
        myHealth = myHealth + deltaHealth;
        hbu.SetHealth(myHealth);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp" && Input.GetMouseButton(0) )
        {
            AddHealth(atkDamage); 
        }
    }

    void OnDestroy()
    {
        Destroy(myHealthBar);
    }
}
