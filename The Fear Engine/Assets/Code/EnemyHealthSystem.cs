using UnityEngine;
using System.Collections;

public class EnemyHealthSystem : MonoBehaviour {
    public GameObject healthBar;

    private GameObject myHealthBar;
    private HealthBarUpdater hbu;
    private float myHealth;

	// Use this for initialization
	void Awake ()
    {
        myHealth = 20;
        myHealthBar = Instantiate(healthBar);
        hbu = healthBar.GetComponent<HealthBarUpdater>();
        myHealthBar.transform.position = gameObject.transform.position + (Vector3.up * 2f);
        myHealthBar.transform.parent = gameObject.transform;

        // scale once to start
        hbu.RescaleHealth(myHealth);
    }
	
	// Update is called once per frame
	void Update ()
    {
        myHealthBar.transform.LookAt(Camera.main.transform);
	}

    /* Add Positive of Negative amount of health to this Enemy */
    public void AddHealth(float deltaHealth)
    {
        print("In AddHealth");
        print(myHealth);
        // increment local health val and scale the bar
        myHealth = (myHealth + deltaHealth)%100;
        hbu.RescaleHealth(myHealth);
    }


    void OnDestroy()
    {
        Destroy(myHealthBar);
    }
}
