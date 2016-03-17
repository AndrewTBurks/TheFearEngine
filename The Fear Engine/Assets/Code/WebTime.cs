using UnityEngine;
using System.Collections;

public class WebTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Object.Destroy(this.gameObject, 2.0f);
	}
}
