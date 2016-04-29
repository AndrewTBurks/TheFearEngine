using UnityEngine;
using System.Collections;

public class mouseBugFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = true;
        Cursor.lockState = 0;

    }
}
