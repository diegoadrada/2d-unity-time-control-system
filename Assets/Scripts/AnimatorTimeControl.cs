 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */
 
using UnityEngine;

public class AnimatorTimeControl : MonoBehaviour
{
    private TimeManager timeController;
    private Animator myAnimator;

    private bool isRecording;
    private bool isPlayingBack;
    private bool isPlayinBackFinished;

    private int recordFrameCounter;

    private void Start()
    {
        timeController = FindObjectOfType<TimeManager>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        recordFrameCounter++;

        if (timeController.timeStatus == TimeStatus.NORMAL)
        {
            if (isPlayingBack)
            {
                myAnimator.StopPlayback();
                isPlayingBack = false;
            }

            if (!isRecording)
            {
                myAnimator.StartRecording(1000);
                isRecording = true;
            }
        }

        if (timeController.timeStatus == TimeStatus.REWIND)
        {
            if (isRecording)
            {
                myAnimator.StopRecording();
                isRecording = false;
            }

            if (!isPlayingBack)
            {
                myAnimator.StartPlayback();
                myAnimator.playbackTime = myAnimator.recorderStopTime;
                isPlayingBack = true;
                isPlayinBackFinished = false;
            }

            if (isPlayingBack)
            {
                if (!isPlayinBackFinished)
                {
                    float newPlaybackTime = myAnimator.playbackTime - Time.deltaTime;

                    if (myAnimator.playbackTime > myAnimator.recorderStartTime)
                    {
                        myAnimator.playbackTime = newPlaybackTime;
                    }                    

                    if (newPlaybackTime < 0)
                    {   
                        //newPlaybackTime = 0;                        
                        Debug.Log("Finish at: " + Time.frameCount+","+recordFrameCounter);
                        isPlayinBackFinished = true;
                    }
                }
                
            }
        }
    }
}
