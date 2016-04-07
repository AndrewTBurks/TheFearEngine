using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {
    public Vector3 checkPoint;
    public Rigidbody player;
    public SanityManagerScript playerhealth;
	// Use this for initialization
	void Start () {
        checkPoint = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("1"))
        {
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



    }

}

