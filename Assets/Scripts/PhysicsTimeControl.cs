 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */
 
using UnityEngine;
using System.Collections.Generic;

public class PhysicsTimeControl : MonoBehaviour
{
    [Range (1,10)]
    public int dataSamplesPerSecond = 5;

    public int secondsToRewind = 5;

    public List<TimeRecordData> recordedDataSamples = new List<TimeRecordData>();

    private TimeManager timeController;

    private const float fixedCallsPerSecond = 50;
    private float sampleThreshold;
    private float sampleRecordTimer;
    private float sampleRewindTimer;
    private int maxDataRecorded;

    private Vector2 currentPosition;
    private Vector2 previousPosition;

    private Vector2 currentVelocity;
    private Vector2 previousVelocity;

    private bool interpolationStarted;

    private Rigidbody2D myRigidbody2D;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();

        timeController = FindObjectOfType<TimeManager>();

        sampleThreshold = fixedCallsPerSecond / dataSamplesPerSecond;
        maxDataRecorded = secondsToRewind * dataSamplesPerSecond;
    }


    private void FixedUpdate()
    {
        if (timeController.timeStatus == TimeStatus.NORMAL)
        {
            if (interpolationStarted)
            {
                interpolationStarted = false;
            }

            RecordObjectData();
            LimitRecordedData();
        }

        if (timeController.timeStatus == TimeStatus.REWIND)
        {
            if (recordedDataSamples.Count >= 2)
            {
                if (!interpolationStarted)
                {
                    SetPositionsToInterpolate();
                    interpolationStarted = true;
                }

                RewindObjectData();
                InterpolateObjectPositions();
            }
        }
    }

    private void RecordObjectData()
    {
        if (sampleRecordTimer < sampleThreshold)
        {
            sampleRecordTimer++;
        }
        else
        {
            recordedDataSamples.Add(new TimeRecordData(myRigidbody2D.position, myRigidbody2D.velocity));
            sampleRecordTimer = 0;
        }        
    }

    private void LimitRecordedData()
    {
        if (recordedDataSamples.Count > maxDataRecorded)
        {
            recordedDataSamples.RemoveAt(0);
        }
    }

    private void RewindObjectData()
    {
        if (sampleRewindTimer < sampleThreshold)
        {
            sampleRewindTimer++;
        }
        else
        {
            SetPositionsToInterpolate();
            sampleRewindTimer = 0;            
        }
    } 

    private void SetPositionsToInterpolate()
    {
        int lastIndex = recordedDataSamples.Count - 1;
        int secondToLastIndex = recordedDataSamples.Count - 2;

        currentPosition = recordedDataSamples[lastIndex].position;
        previousPosition = recordedDataSamples[secondToLastIndex].position;

        currentVelocity = recordedDataSamples[lastIndex].velocity;
        previousVelocity = recordedDataSamples[secondToLastIndex].velocity;

        recordedDataSamples.RemoveAt(lastIndex);
    }

    private void InterpolateObjectPositions()
    {
        float interpolationPercentage = sampleRewindTimer / sampleThreshold;

        myRigidbody2D.position = Vector2.Lerp(currentPosition, previousPosition, interpolationPercentage);
        myRigidbody2D.velocity = Vector2.Lerp(currentVelocity, previousVelocity, interpolationPercentage);
    }
}
