using UnityEngine;
using System.Collections;

public class WebTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
        
=======
	}
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag != "Spider"){
			Object.Destroy(this.gameObject)	
		}
>>>>>>> origin/master
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Spider")
        { Object.Destroy(this.gameObject); }
    }
}
