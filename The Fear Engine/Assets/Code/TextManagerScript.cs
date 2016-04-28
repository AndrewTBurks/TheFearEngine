using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextManagerScript : MonoBehaviour {
    public Text dialogueText;
    public RawImage textBG;

    private Color tutColor = new Color(40.0f/255.0f, 240.0f/255.0f, 110.0f/255.0f);
    private Color storyColor = new Color(1, 1, 1);
    
	void Awake () {
        dialogueText.text = null;
        textBG.color = new Color(textBG.color.r, textBG.color.g, textBG.color.b, 0);
    }
	

	public void DisplayText(string[] messages, int[] times, bool isTutorial)
    {
        textBG.color = new Color(textBG.color.r, textBG.color.g, textBG.color.b, 0.3f);
		StartCoroutine(TextWait(messages, times, isTutorial));
        
    }

	IEnumerator TextWait(string[] messages, int[] times, bool isTutorial)
    {
		for(int i = 0; i < messages.Length; i++){
            textBG.color = new Color(textBG.color.r, textBG.color.g, textBG.color.b, 0.3f);
            
            if(isTutorial)
            {
                dialogueText.text = "Tip: " + messages[i];
                dialogueText.color = tutColor;
            }
            else
            {
                dialogueText.text = messages[i];
                dialogueText.color = storyColor;
            }
			yield return new WaitForSeconds(times[i]);
		}

        dialogueText.text = null;
        textBG.color = new Color(textBG.color.r, textBG.color.g, textBG.color.b, 0);
    }
}
