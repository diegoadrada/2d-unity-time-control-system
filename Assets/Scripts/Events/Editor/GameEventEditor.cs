/*
* Copyright (c) Gametopia Studios
* http://www.gametopiastudios.com/
*/

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameEvent gameEvent = target as GameEvent;

        if (GUILayout.Button("Raise"))
        {
            gameEvent.Raise();
        }

    }
}
