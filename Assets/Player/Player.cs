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

    private Rigidbody2D myRigidbody2D;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw(InputNames.Horizontal);
        verticalInput = Input.GetAxisRaw(InputNames.Vertical);
    
        myRigidbody2D.velocity = new Vector2(horizontalInput * playerSpeed, verticalInput * playerSpeed);
    }
}
