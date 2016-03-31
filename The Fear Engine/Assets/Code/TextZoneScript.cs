using UnityEngine;
using System.Collections;

public class TextZoneScript : MonoBehaviour {
    public string zoneText = "Test";

    bool seen;      // whether the player has already seen the text at this point

    TextManagerScript managerScript;

    void Awake()
    {
        seen = false;
        managerScript = transform.parent.gameObject.GetComponent<TextManagerScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !seen)
        {
            print("Player Entered Zone");
            managerScript.DisplayText(zoneText);
            seen = true;
        }
        
    }

}
