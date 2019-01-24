/*
* Copyright (c) Diego Adrada
* http://www.diegoadrada.com/
*/

using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public struct InputNames
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
    }

    public float playerSpeed = 10f;

    public List<Vector2> playerRecordedPositions = new List<Vector2>();

    private float horizontalInput;
    private float verticalInput;

    private TimeManager timeController;

    private Rigidbody2D myRigidbody2D;  

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        timeController = FindObjectOfType<TimeManager>();
    }

    private void Update()
    {
        GetPlayerInputs();
    }

    private void FixedUpdate()
    {
        if (timeController.timeStatus == TimeStatus.NORMAL)
        {
            myRigidbody2D.velocity = new Vector2(horizontalInput * playerSpeed, verticalInput * playerSpeed);
            RecordPlayerPosition();
        }
        
        if (timeController.timeStatus == TimeStatus.REWIND)
        {
            RewindPlayerPosition();
        }
    }

    private void GetPlayerInputs()
    {
        horizontalInput = Input.GetAxisRaw(InputNames.Horizontal);
        verticalInput = Input.GetAxisRaw(InputNames.Vertical);
    }

    private void RecordPlayerPosition()
    {
        playerRecordedPositions.Add(myRigidbody2D.position);
    }

    private void RewindPlayerPosition()
    {
        if (playerRecordedPositions.Count > 0)
        {
            myRigidbody2D.MovePosition(playerRecordedPositions[playerRecordedPositions.Count - 1]);
            playerRecordedPositions.RemoveAt(playerRecordedPositions.Count - 1);
        }
    }
}
