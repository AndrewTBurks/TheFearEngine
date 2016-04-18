using UnityEngine;
using System.Collections;

public class CheckpointScript: MonoBehaviour
{
    public int checkpointNumber;
    public GameObject pSystem;

    // Update is called once per frame
    void Update()
    {

    }

    public void SetReached()
    {
        print("Reached Checkpoint" + checkpointNumber);
        pSystem.SetActive(false);
        // update the record of furthest checkpoint
        transform.parent.GetComponent<FallPlaneRespawn>().UpdateFurthest(checkpointNumber, this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SetReached();
        }
    }
}
