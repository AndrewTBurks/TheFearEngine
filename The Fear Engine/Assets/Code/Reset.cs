using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {
    public Vector3 checkPoint;
    public Rigidbody player;
    public SanityManagerScript playerhealth;
    public GameObject firstTP;
    public GameObject secondTP;
    public GameObject thirdTP;
    public Camera cam;
    private float numCheckPoints;
    // Use this for initialization
    void Start () {
        checkPoint = player.transform.position;
        numCheckPoints = 10;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("1"))
        {
            numCheckPoints -= 1;
            checkPoint = this.transform.position;
        }

        if (Input.GetKeyDown("2"))
        {
            this.transform.position = checkPoint;
            playerhealth.AddSanity(-5);
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FLOOR")
        {
            this.transform.position = checkPoint;
        }

        if (other.tag == "BossTP")
        {
            this.transform.position = firstTP.transform.position;
            cam.farClipPlane = 1000;

        }
        if (other.tag == "BossTP2")
        {
            this.transform.position = secondTP.transform.position;
        }
       
    }

}

