using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UImanager : MonoBehaviour {

    public GameObject pausePanel;
    private GameObject cam;
    private GameObject player;
    public bool isPaused;
	// Use this for initialization
	void Start () {
        isPaused = false;
        cam = GameObject.Find("MainCamera");
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
	    if (isPaused)
        {
            PauseGame(true);
        }
        else
        {
            PauseGame(false);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            SwitchPause();
        }
	}

    void PauseGame(bool state)
    {
        if (state)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
        }
    }

    public void SwitchPause()
    {
        if (isPaused)
        {
            isPaused = false; //Changes the value
        }

        else
        {
            isPaused = true;
        }
    }
}
