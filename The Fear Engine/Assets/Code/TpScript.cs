using UnityEngine;
using System.Collections;

public class TpScript : MonoBehaviour {
    public GameObject tp;
    public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        player.transform.position = tp.transform.position;
    }
}
