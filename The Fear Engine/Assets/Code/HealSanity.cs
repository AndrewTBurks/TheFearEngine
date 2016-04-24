using UnityEngine;
using System.Collections;

public class HealSanity : MonoBehaviour {

    public GameObject healObject;
    public Transform targetPlayer;
    public SanityManagerScript sanity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // slow spin effect
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 60 * Time.deltaTime);

        float seperation = Vector3.Distance(this.transform.position, targetPlayer.transform.position);

        if ((seperation <= 2))
        {
            float tSan;
            tSan = sanity.GetSanity();
            if (tSan >= 50)
            {
                sanity.UpdateSanity(100);
            }
            else
            {
                sanity.UpdateSanity(tSan + 50);
            }

            Destroy(healObject);
        }
    }

    
}
