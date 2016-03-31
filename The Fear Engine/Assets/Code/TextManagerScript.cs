using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextManagerScript : MonoBehaviour {
    public Text dialogueText;
    public RawImage textBG;
    
	void Awake () {
        dialogueText.text = null;
        textBG.color = new Color(textBG.color.r, textBG.color.g, textBG.color.b, 0);
    }
	

    public void DisplayText(string message)
    {
        dialogueText.text = message;
        textBG.color = new Color(textBG.color.r, textBG.color.g, textBG.color.b, 0.2f);
        StartCoroutine(TextWait(10));
        
    }

    IEnumerator TextWait(int time)
    {
        yield return new WaitForSeconds(time);
        dialogueText.text = null;
        textBG.color = new Color(textBG.color.r, textBG.color.g, textBG.color.b, 0);
    }
}
