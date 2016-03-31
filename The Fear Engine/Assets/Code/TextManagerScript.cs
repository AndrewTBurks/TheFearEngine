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
	

	public void DisplayText(string[] messages, int[] times)
    {
        textBG.color = new Color(textBG.color.r, textBG.color.g, textBG.color.b, 0.2f);
		StartCoroutine(TextWait(messages, times));
        
    }

	IEnumerator TextWait(string[] messages, int[] times)
    {
		for(int i = 0; i < messages.Length; i++){
			dialogueText.text = messages [i];
			yield return new WaitForSeconds(times[i]);
		}

        dialogueText.text = null;
        textBG.color = new Color(textBG.color.r, textBG.color.g, textBG.color.b, 0);
    }
}
