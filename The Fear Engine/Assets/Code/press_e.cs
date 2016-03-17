using UnityEngine;
using System.Collections;

public class press_e : MonoBehaviour {
    private bool pickup;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;

    // Use this for initialization
    void Start () {
        
         
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e") && pickup)
        {
            pickup = false;
            Destroy(this.gameObject);
            controller.hasSword = true;

            
        }
       
	
	}

    //Displays press e to pick up if colliding with pick upable object.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pickup = true;
            print(pickup);

        }
    }
    void OnGUI()
   {
        if (pickup)
        {
            GUI.Label(new Rect(Screen.width/2,Screen.height/2, 300, 300), "Press E to pick up");

        }
    }
}
