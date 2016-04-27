using UnityEngine;
using System.Collections;

/*
  Code taken from tutorial credit video: https://www.youtube.com/watch?v=Qh1jXBfOQOc
    */

public class Credits : MonoBehaviour {

    public GameObject camera;
    public int speed = 1;
    public string level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        camera.transform.Translate(Vector3.down * Time.deltaTime * speed);
        StartCoroutine(waitFor());
	}

    IEnumerator waitFor(){

        yield return new WaitForSeconds(52);
        Application.LoadLevel(level);
        
    }
}
