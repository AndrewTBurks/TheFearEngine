using UnityEngine;
using System.Collections;

public class LongJump : MonoBehaviour {
    public Rigidbody player;
    public Transform startMarker;
    public Transform endMarker;
    private float speed = 1.0F;
    private float starttime;
    private float journeyLength;
	// Use this for initialization
	void Start () {
        starttime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        float distCovered = (Time.time - starttime) * speed;
        float fracjourney = distCovered / journeyLength;
        player.transform.position = Vector3.Lerp(startMarker.position, endMarker.position,fracjourney);
        
    }
}
