 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */
 
using UnityEngine;
using System.Collections.Generic;

public class TimeControl : MonoBehaviour
{
    public int secondsToRewind = 5;
    public List<Vector2> objectRecordedPositions = new List<Vector2>();

    private TimeManager timeController;

    private const int keyFrame = 5;
    private int frameCounter = 0;
    private int reverseCounter = 0;

    private Vector2 currentPosition;
    private Vector2 previousPosition;

    private bool rewindInitialized = false;

    private int rewindTimeThreshold;

    private void Start()
    {
        timeController = FindObjectOfType<TimeManager>();
        rewindTimeThreshold = secondsToRewind * keyFrame * 2;
    }

    private void FixedUpdate()
    {
        if (timeController.timeStatus == TimeStatus.NORMAL)
        {
            RecordObjectPosition();
            LimitRecordedPositions();

            if (rewindInitialized)
            {
                RestoreRewindInitialize();
            }          
        }

        if (timeController.timeStatus == TimeStatus.REWIND)
        {
            RewindObjectPosition();
        }
    }

    private void RecordObjectPosition()
    {
        if (frameCounter < keyFrame)
        {
            frameCounter += 1;
        }
        else
        {
            frameCounter = 0;
            objectRecordedPositions.Add(transform.position);
        }        
    }

    private void LimitRecordedPositions()
    {
        if (objectRecordedPositions.Count > rewindTimeThreshold)
        {
            objectRecordedPositions.RemoveAt(0);
        }
    }

    private void RewindObjectPosition()
    {
        if (!rewindInitialized)
        {
            InitializeRewind();
            RestoreObjectPositions();
        }

        if (reverseCounter > 0)
        {
            reverseCounter -= 1;
        }
        else
        {
            reverseCounter = keyFrame;
            RestoreObjectPositions();
        }

        InterpolateObjectPositions();
    }

    private void RestoreRewindInitialize()
    {
        rewindInitialized = false;
    }

    private void InitializeRewind()
    {
        rewindInitialized = true;
    }    

    private void RestoreObjectPositions()
    {
        int lastIndex = objectRecordedPositions.Count - 1;
        int secondToLastIndex = objectRecordedPositions.Count - 2;

        if (secondToLastIndex >= 0)
        {
            currentPosition =  objectRecordedPositions[lastIndex];
            previousPosition = objectRecordedPositions[secondToLastIndex];

            objectRecordedPositions.RemoveAt(lastIndex);
        }
    }

    private void InterpolateObjectPositions()
    {
        if (objectRecordedPositions.Count > 1)
        {
            float interpolation = (float)reverseCounter / (float)keyFrame;
            transform.position = Vector2.Lerp(previousPosition, currentPosition, interpolation);
        }
    }
}
