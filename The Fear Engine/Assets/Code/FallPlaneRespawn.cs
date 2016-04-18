using UnityEngine;
using System.Collections;

/* 
  This script checks the position of the player character, and if it ever falls below the position of the "fallPlane", the player is set to respawn at
  the "spawnlocation" with their sanity decremented (I'm thinking maybe - 50% of current sanity).  Note that this script is just for
  level "killzone", it probably wouldn't work well for random pits

    playerChar should be the FPScontroller
    spawnlocation should be an empty child object
    fallplane is just plane
*/
public class FallPlaneRespawn : MonoBehaviour {

    public Transform fallPlane;
    public Transform playerChar;
    public SanityManagerScript manager;

    public int furthestCheckpointReached;
    public GameObject startCheckpoint;
    private GameObject lastCheckpoint;

    private float san;
    
	// Use this for initialization
	void Awake () {
        furthestCheckpointReached = 0; // start of the level
        startCheckpoint.GetComponent<CheckpointScript>().SetReached();
	}
	
	// Update is called once per frame
	void Update () {

     
        if ((playerChar.position.y <= fallPlane.position.y))
        {
            // respawn at furthest checkpoint
            playerChar.position = lastCheckpoint.transform.position;
            playerChar.rotation = lastCheckpoint.transform.rotation;
            san = manager.GetSanity();
            san = san / 2;
            manager.AddSanity(-san);

        }
	}

    public void UpdateFurthest(int number, GameObject checkpoint)
    {
        if(number > furthestCheckpointReached)
        {
            furthestCheckpointReached = number;
        }
    }
}
