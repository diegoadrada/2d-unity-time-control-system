 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */
 
using UnityEngine;

[System.Serializable]
public struct TimeRecordData
{
    public Vector2 position;
    public Vector2 velocity;

    public TimeRecordData(Vector2 position, Vector2 velocityX)
    {
        this.position = position;
        this.velocity = velocityX;
    }
}
