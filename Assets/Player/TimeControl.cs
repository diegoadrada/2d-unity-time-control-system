 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */
 
using UnityEngine;
using System.Collections.Generic;

public class TimeControl : MonoBehaviour
{
    public List<Vector2> objectRecordedPositions = new List<Vector2>();

    private TimeManager timeController;

    private void Start()
    {
        timeController = FindObjectOfType<TimeManager>();
    }

    private void FixedUpdate()
    {
        if (timeController.timeStatus == TimeStatus.NORMAL)
        {
            RecordObjectPosition();
        }

        if (timeController.timeStatus == TimeStatus.REWIND)
        {
            RewindObjectPosition();
        }
    }

    private void RecordObjectPosition()
    {
        objectRecordedPositions.Add(transform.position);
    }

    private void RewindObjectPosition()
    {
        if (objectRecordedPositions.Count > 0)
        {
            transform.position = objectRecordedPositions[objectRecordedPositions.Count - 1];
            objectRecordedPositions.RemoveAt(objectRecordedPositions.Count - 1);
        }
    }
}
