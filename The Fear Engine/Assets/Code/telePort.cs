using UnityEngine;
using System.Collections;

public class telePort : MonoBehaviour {
    public GameObject otherTP;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = otherTP.transform.position + transform.up * 10;
    }
}
