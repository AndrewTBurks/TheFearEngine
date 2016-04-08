using UnityEngine;
using System.Collections;

public class gateDestroy : MonoBehaviour {

    public Transform targetPlayer;
    public GameObject thisGate;
    public WeaponBehavior sword;

    public GameObject gateGuardian;

    private bool canOpen = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float seperation = Vector3.Distance(this.transform.position, targetPlayer.transform.position);

        if ((seperation <= 4))
        {
            canOpen = true;
            if (Input.GetMouseButtonDown(0) && sword.obtained && gateGuardian == null)
            {
                Destroy(thisGate);
            }
        }
        else
        {
            canOpen = false;
        }

    }

    void OnGUI()
    {
        if (canOpen && gateGuardian != null)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 300, 300), "Defeat the guardian before Destroying the Gate");
        }
        else if (canOpen)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 300, 300), "Swing sword to Destroy Gate");

        }
    }
}
