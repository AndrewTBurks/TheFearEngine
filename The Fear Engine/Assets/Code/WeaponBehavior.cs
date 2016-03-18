using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour {

    private bool canPickup;
    private GameObject playerInRange;

    private bool isAttached;

    private bool canSwing;

	// Use this for initialization
	void Start () {
        canPickup = false;
        playerInRange = null;
        isAttached = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isAttached) // idle rotate on ground...
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0, 90*Time.deltaTime, 0);
        }
        else if(isAttached && canSwing)
        {
            // face same direction as main camera
            transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0, -80, 10);
        }

        // pick up a weapon on the ground
        if (Input.GetKeyDown("e") && canPickup && playerInRange != null)
        {
            // use pickup 

            AttachWeapon();
            
        }
        // drop this weapon on the ground if you have one
        if(Input.GetKeyDown("g") && isAttached)
        {
            // use drop

            DetachWeapon();

        }

        if(Input.GetMouseButtonDown(0) && isAttached && canSwing)
        {
            StartCoroutine(SwingWeapon());
        }
    }



    // actions for picking up sword or dropping sword
    void AttachWeapon()
    {
        transform.rotation = playerInRange.transform.rotation * Quaternion.Euler(0, -80, 10);
        transform.position = playerInRange.transform.position + playerInRange.transform.right + playerInRange.transform.forward;
        transform.parent = playerInRange.transform;

        canPickup = false;
        isAttached = true;

        canSwing = true;
    }

    void DetachWeapon()
    {
        transform.position = new Vector3(transform.parent.position.x, 1, transform.parent.position.z);
        transform.parent = null;

        canPickup = true;
        isAttached = false;

        canSwing = false;
    }

    // Swing weapon to attempt to hit enemy
    IEnumerator SwingWeapon() // should probably use animator in future
    {
        canSwing = false;
        // pull up to swing
        for(int i = 0; i < 25; i++)
        {
            transform.rotation *= Quaternion.Euler(0, 0, 2);
            yield return new WaitForSeconds(0.0005f);
        }

        // swing down
        for(int i = 0; i < 20; i++)
        {
            transform.rotation *= Quaternion.Euler(0, 0, -4);
            yield return new WaitForSeconds(0.0001f);
        }

        // back to normal height
        for(int i = 0; i < 30; i++)
        {
            transform.rotation *= Quaternion.Euler(0, 0, 1);
            yield return new WaitForSeconds(0.001f);
        }

        // .5 second wait time after 
        yield return new WaitForSeconds(0.5f);
        canSwing = true;
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
