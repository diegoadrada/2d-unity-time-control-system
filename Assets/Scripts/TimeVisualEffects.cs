 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */
 
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class TimeVisualEffects : MonoBehaviour
{
    private Grayscale grayscaleEffect;
    private TimeManager timeController;

    private void Start()
    {
        grayscaleEffect = FindObjectOfType<Grayscale>();
        timeController = FindObjectOfType<TimeManager>();
    }

    private void Update()
    {
        if (timeController.timeStatus == TimeStatus.NORMAL)
        {
            if (grayscaleEffect.enabled)
            {
                grayscaleEffect.enabled = false;
            }            
        }

        if (timeController.timeStatus == TimeStatus.REWIND)
        {
            if (!grayscaleEffect.enabled)
            {
                grayscaleEffect.enabled = true;
            }
        }
    }
}
