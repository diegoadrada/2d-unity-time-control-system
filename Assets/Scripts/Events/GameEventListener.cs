 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */

using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent EventToListen;
    public UnityEvent ResponseToEvent;

    private void OnEnable()
    {
        if (EventToListen != null)
        {
            EventToListen.RegisterListener(this);     
        }        
    }

    private void OnDisable()
    {
        if (EventToListen != null)
        {
            EventToListen.UnregisterListener(this);
        }
    }

    public void OnEventRaised()
    {
        if (ResponseToEvent != null)
        {
            ResponseToEvent.Invoke();
        }        
    }

}
