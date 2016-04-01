/*
Code taken form Unity tutorial
https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/creating-a-scene-menu
*/

using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

    //public GameObject loadingImage;

	public void LoadScene(int level)
    {
        //loadingImage.SetActive(true);
        Application.LoadLevel(level);
    }
}
