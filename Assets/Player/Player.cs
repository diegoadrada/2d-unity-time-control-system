/*
* Copyright (c) Diego Adrada
* http://www.diegoadrada.com/
*/

using UnityEngine;


public class Player : MonoBehaviour
{
    public struct InputNames
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
    }

    public float playerSpeed = 10f;

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
            myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            NormalPlayerMovement();
        }

        if (timeController.timeStatus == TimeStatus.REWIND)
        {
            myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void GetPlayerInputs()
    {
        horizontalInput = Input.GetAxisRaw(InputNames.Horizontal);
        verticalInput = Input.GetAxisRaw(InputNames.Vertical);
    }

    private void NormalPlayerMovement()
    {
        myRigidbody2D.velocity = new Vector2(horizontalInput * playerSpeed, verticalInput * playerSpeed);
    }
}
