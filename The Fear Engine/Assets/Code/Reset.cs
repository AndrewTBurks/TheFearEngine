using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Reset : MonoBehaviour {
    public Vector3 checkPoint;
    public Rigidbody player;
    public SanityManagerScript playerhealth;
    public Camera cam;
    private float numCheckPoints;
    private float timer;
    private float chargeTime;
    private bool charging;
    private bool chargingtwo;
    private float timertwo;
    public Slider slider;
    // Use this for initialization
    void Start () {
        checkPoint = player.transform.position;
        numCheckPoints = 10;
	}
	
	// Update is called once per frame
	void Update () {
        
	    if (Input.GetKeyDown("1"))
        {
            charging = true;
            
        }
        if (Input.GetKeyUp("1"))
        {
            charging = false;
            timer = 0;
            slider.value = timer;
        }
        if (timer >= 1.0f)
        {
            numCheckPoints -= 1;
            checkPoint = this.transform.position;
            timer = 0;
            slider.value = timer;
        }

        if (Input.GetKeyDown("2"))
        {
            chargingtwo = true;
        }

        if (Input.GetKeyUp("2"))
        {
            chargingtwo = false;
            timertwo = 0;
            slider.value = timertwo;
        }

        if (timertwo >= 1.0f)
        {
            this.transform.position = checkPoint;
            playerhealth.AddSanity(-5);
            timertwo = 0;
            slider.value = timertwo;
        }

        if (charging)
        {
            timer += Time.deltaTime;
            slider.value = timer;
        }

        if (chargingtwo)
        {
            timertwo += Time.deltaTime;
            slider.value = timertwo;
        }

        if (Input.GetKeyDown("q"))
        {
            Physics.gravity = new Vector3(1f, 5f, 0f);
        }

        if (Input.GetKeyUp("q"))
        {
            Physics.gravity = new Vector3(1f, -9.81f, 0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FLOOR")
        {
            this.transform.position = checkPoint;
            playerhealth.AddSanity(-5);
        }
       
    }

}

