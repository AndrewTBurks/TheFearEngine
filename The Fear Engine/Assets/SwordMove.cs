using UnityEngine;
using System.Collections;

public class SwordMove : MonoBehaviour {
    GameObject camera4sword;
    // Use this for initialization
    void Start () {
        camera4sword = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = camera4sword.transform.rotation;
	}
}
