/*
 * Written by: Andrew Burks
 *
 * Code for the "Sanity" controller to be attached to SanityManager prefab. 
 * Uses the instance of Camera and Light source which are children of the prefab.
 *
 * Will only update the objects if the variable 'sanity' is changed. This can be
 * changed by calling the UpdateSanity(int newSanity) method.
 *
*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SanityManagerScript : MonoBehaviour {
    private Light myLight;
    private Camera myCamera;
    public RawImage myImage;
    public Slider SanitySlider;

    public GameObject myPlayer;

    private float sanity;
    private bool sanityUpdated;

    /* starting numbers for light/camera/movement properties */
    // light source
    private float intensityStart = .01f;
    private float intensityEnd = .018f;
    private float rangeStart = 30;
    private float rangeEnd = 15;
    private Color colorStart = new Color(100, 100, 100);
    private Color colorEnd = new Color(150, 50, 50);
    // camera
    private float fovStart = 40;
    private float fovEnd = 55;
    // image
    private float alphaStart = 0;
    private float alphaEnd = 0.8f;

    private float proximityDecayCoeff = 0.25f;

    // movement (UNUSED FOR NOW)
    private float speedStart = 10;
    private float speedEnd = 7;
    /*=======================================================*/

	void Start ()
    {
        // get image, light and camera objects which are children of the SanityManager

        myLight = GetComponentInChildren<Light>();
        myCamera = GetComponentInChildren<Camera>();
        myImage = GetComponentInChildren<Canvas>().GetComponentInChildren<RawImage>();

        myPlayer = GameObject.Find("FPSController");

        myLight.transform.parent = myPlayer.transform;

        // initialize all image, light and camera values to be starting values for full sanity

        myLight.intensity = intensityStart;
        myLight.range = rangeStart;
        myLight.color = colorStart;

        myCamera.fieldOfView = fovStart;

        myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, alphaStart);

        // initialize sanity number to be 100
        sanity = 100;
        SanitySlider.value = 100;
        sanityUpdated = false;
	}

    void Update ()
    {
        if (sanityUpdated) // if sanity is updated then update the light/camera/etc.
        {
            float newIntensity;
            float newRange;
            Color newColor;
            float newFieldOfView;
            float newAlphaVal;

            float sanityPercent = sanity / 100;

            // Calculate new light Intensity
            newIntensity = intensityEnd - ((intensityEnd - intensityStart) * sanityPercent);
            // Calculate new light Range
            newRange = rangeEnd - ((rangeEnd - rangeStart) * sanityPercent);
            // Calculate new light Color
            newColor = new Color(colorEnd.r - ((colorEnd.r - colorStart.r)* sanityPercent),
                colorEnd.g - ((colorEnd.g - colorStart.g) * sanityPercent),
                colorEnd.b - ((colorEnd.b - colorStart.b) * sanityPercent));
            // Calculate new Field of View
            newFieldOfView = fovEnd - ((fovEnd - fovStart) * sanityPercent);
            // Calculate new Alpha value for image
            newAlphaVal = alphaEnd - ((alphaEnd - alphaStart) * sanityPercent);


            // Now update the image, light and camera to have these new values
            myLight.intensity = newIntensity;
            myLight.range = newRange;
            myLight.color = newColor;

            myCamera.fieldOfView = newFieldOfView;

            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, newAlphaVal);

            SanitySlider.value = sanity;

            // all values are correct for the current sanity now
            sanityUpdated = false;
        }

        // to decrease sanity depending on how close the proximity enemies are
        AddSanity(CalculateSanityDecreaseByEnemy());
	}


    /*
     * To return the value of the player's sanity call this function
     * 
    */
    public float GetSanity()
    {
        return sanity;
    }


    /*
     * To update the player's sanity to a value call this function
     * 
    */
    public void UpdateSanity(float newSanity)
    {
        if (sanity > 0)
        {
            sanity = newSanity;

            // the values are no longer correct for the current sanity value
            sanityUpdated = true;
        }
    }

    /*
     * To add a number to the player's sanity call this function
     * 
    */
    public void AddSanity(float increment)
    {
        if (sanity > 0)
        {
            sanity += increment;
            
            // the values are no longer correct for the current sanity value
            sanityUpdated = true;
        }
    }

    float CalculateSanityDecreaseByEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ProximityEnemy");
        float sanityDecrease = 0;

        foreach (var e in enemies)
        {
            float distance = Vector3.Distance(myPlayer.transform.position, e.transform.position);

            if (distance < 30) // upper bound on distance at which it begins to decrease
            {
                // adds at most 2f ( so value doesn't go to infinity at close distance )
                sanityDecrease += proximityDecayCoeff / distance > proximityDecayCoeff ? proximityDecayCoeff : proximityDecayCoeff / distance;
            }
        }

        return -1 * sanityDecrease;
    }
}