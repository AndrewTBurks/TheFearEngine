﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class JumpEndText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 if (Input.GetButtonDown("space"))
        {
            SceneManager.LoadScene("JumpLevel");
        }
	}
}
