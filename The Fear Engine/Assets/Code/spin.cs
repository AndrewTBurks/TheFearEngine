﻿using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.rotation = transform.rotation * Quaternion.Euler(0, 1, 0 * Time.deltaTime);
    }
}
