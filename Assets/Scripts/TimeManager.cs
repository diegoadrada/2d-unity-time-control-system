 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */
 
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    public TimeStatus timeStatus;

    private void Start()
    {
        timeStatus = TimeStatus.NORMAL;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            timeStatus = TimeStatus.REWIND;
            Time.timeScale = 2f;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            timeStatus = TimeStatus.NORMAL;
            Time.timeScale = 1f;
        }
    }
}
