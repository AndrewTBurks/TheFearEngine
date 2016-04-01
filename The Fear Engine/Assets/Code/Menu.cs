using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {


    public Canvas Quitmenu;
    public Button startText;
    //public Button exitText;


	// Use this for initialization
	void Start () {


	//Quitmenu = Quitmenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        //exitText = exitText.GetComponent<Button>();
      //  Quitmenu.enabled = false;
	}
	
	public void ExitPress()
    {
        //Quitmenu.enabled = true;
        startText.enabled= false;
        //exitText.enabled=false;
        //Debug.Log("ExitPressed");
    }

	public void NoPress()
    {
        //Quitmenu.enabled = false;
        startText.enabled = true;
        //exitText.enabled = true;
    }
	
    public void StartLevel()
    {
        Application.LoadLevel("hmwk8-2");
        Debug.Log("Start game pressed");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
