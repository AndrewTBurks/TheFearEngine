using UnityEngine;
using System.Collections;

public class RemoveWall : MonoBehaviour {
    private GameObject[] enemies;
	// Use this for initialization
	void Start () {

	    
	}
	
	// Update is called once per frame
	void Update () {
        enemies = GameObject.FindGameObjectsWithTag("ProximityEnemy");
        if (enemies.Length == 0)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
