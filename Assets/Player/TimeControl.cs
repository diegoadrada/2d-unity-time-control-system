 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */
 
using UnityEngine;
using System.Collections.Generic;

public class TimeControl : MonoBehaviour
{
    public int secondsToRewind = 5;
    public List<TimeRecordData> objectRecordedPositions = new List<TimeRecordData>();

    private TimeManager timeController;

    private const int keyFrame = 5;

    private int frameCounter = 0;
    private int reverseCounter = 5;

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
                rewindInitialized = false;
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

            TimeRecordData timeRecordData = new TimeRecordData
            {
                position = transform.position
            };

            objectRecordedPositions.Add(timeRecordData);
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
            rewindInitialized = true;
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

    private void RestoreObjectPositions()
    {
        int lastIndex = objectRecordedPositions.Count - 1;
        int secondToLastIndex = objectRecordedPositions.Count - 2;

        if (secondToLastIndex >= 0)
        {
            currentPosition =  objectRecordedPositions[lastIndex].position;
            previousPosition = objectRecordedPositions[secondToLastIndex].position;

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
