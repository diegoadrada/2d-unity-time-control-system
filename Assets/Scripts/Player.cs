/*
* Copyright (c) Diego Adrada
* http://www.diegoadrada.com/
*/

using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public struct InputNames
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
    }

    public float playerSpeed = 10f;

    public GameEvent playerJumpEvent;

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
        GetPlayerAxisInputs();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (timeController.timeStatus == TimeStatus.NORMAL)
        {
            //myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            NormalPlayerMovement();
        }

        if (timeController.timeStatus == TimeStatus.REWIND)
        {
            //horizontalInput = 0;
            //verticalInput = 0;
            //myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void GetPlayerAxisInputs()
    {
        horizontalInput = Input.GetAxisRaw(InputNames.Horizontal);
        verticalInput = Input.GetAxisRaw(InputNames.Vertical);
    }

    private void NormalPlayerMovement()
    {
        myRigidbody2D.velocity = new Vector2(horizontalInput * playerSpeed, verticalInput * playerSpeed);
    }

    private void Jump()
    {
        playerJumpEvent.Raise();
    }
}
