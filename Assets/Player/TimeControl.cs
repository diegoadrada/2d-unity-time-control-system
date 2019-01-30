 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */
 
using UnityEngine;
using System.Collections.Generic;

public class TimeControl : MonoBehaviour
{
    [Range (1,10)]
    public int dataSamplesPerSecond = 1;

    public int secondsToRewind = 5;

    public List<TimeRecordData> recordedData = new List<TimeRecordData>();

    private TimeManager timeController;

    private const float fixedCallsPerSecond = 50;
    private float sampleThreshold;
    private float sampleRecordTimer;
    private float sampleRewindTimer;
    private int maxDataRecorded;

    private Vector2 currentPosition;
    private Vector2 previousPosition;

    private bool interpolationStarted;

    private void Start()
    {
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
            if (recordedData.Count >= 2)
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
            recordedData.Add(new TimeRecordData(transform.position));
            sampleRecordTimer = 0;
        }        
    }

    private void LimitRecordedData()
    {
        if (recordedData.Count > maxDataRecorded)
        {
            recordedData.RemoveAt(0);
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
        int lastIndex = recordedData.Count - 1;
        int secondToLastIndex = recordedData.Count - 2;

        currentPosition = recordedData[lastIndex].position;
        previousPosition = recordedData[secondToLastIndex].position;

        recordedData.RemoveAt(lastIndex);
    }

    private void InterpolateObjectPositions()
    {
        float interpolationPercentage = sampleRewindTimer / sampleThreshold;
        transform.position = Vector2.Lerp(currentPosition, previousPosition, interpolationPercentage);
    }
}
