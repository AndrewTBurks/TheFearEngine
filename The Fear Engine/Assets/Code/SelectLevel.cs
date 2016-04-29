using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectLevel : MonoBehaviour {

    public Dropdown myDropdown;


	// Use this for initialization
	public void chooseLevel()
    {
        myDropdown.onValueChanged.AddListener(delegate{ myDropdownValueChangedHandler(myDropdown); });
    }
    private void myDropdownValueChangedHandler(Dropdown target)
    {
        Debug.Log("selected: " + target.value);
    }

    public void SetDropdownIndex(int index)
    {
        myDropdown.value = index;
    }
}
