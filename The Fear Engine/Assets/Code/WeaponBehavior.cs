using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour {

    private bool canPickup;
    private GameObject playerInRange;

    private bool isAttached;
    public bool obtained = false;

    public bool isAttacking;
    public bool enemyHit;

	// Use this for initialization
	void Start () {
        canPickup = false;
        playerInRange = null;
        isAttached = false;

        isAttacking = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isAttached) // idle rotate on ground...
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0, 90*Time.deltaTime, 0);
        }
        else if(isAttached && !isAttacking)
        {
            // face same direction as main camera
            transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0, -80, 10);
        }

        // pick up a weapon on the ground
        if (Input.GetKeyDown("e") && canPickup && playerInRange != null)
        {
            // use pickup 

            AttachWeapon();
            obtained = true;
            
        }
        // drop this weapon on the ground if you have one
        if(Input.GetKeyDown("g") && isAttached)
        {
            // use drop

            DetachWeapon();
            obtained = false;

        }

        if(Input.GetMouseButtonDown(0) && isAttached && !isAttacking)
        {
            isAttacking = true;
            GetComponent<Animation>().Play();
        }
        
    }

    public void AttackDone()
    {
        isAttacking = false;
        enemyHit = false;
    }

    // actions for picking up sword or dropping sword
    void AttachWeapon()
    {
        transform.rotation = playerInRange.transform.rotation * Quaternion.Euler(0, -80, 10);
        transform.position = playerInRange.transform.position + playerInRange.transform.right + playerInRange.transform.forward;
        transform.parent = playerInRange.transform;

        canPickup = false;
        isAttached = true;

        isAttacking = false;
    }

    void DetachWeapon()
    {
        transform.position = new Vector3(transform.parent.position.x, 1, transform.parent.position.z);
        transform.parent = null;

        canPickup = true;
        isAttached = false;

        isAttacking = false;
    }


    //Displays press e to pick up if colliding with pick upable object.
    // handles collision detection if the player is in range
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isAttached)
        {
            canPickup = true;
            playerInRange = other.gameObject;
        }

       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !isAttached)
        {
            canPickup = false;
            playerInRange = null;
        }
    }

    void OnGUI()
    {
        if (canPickup)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 300, 300), "Press E to pick up");

        }
    }

    
}
