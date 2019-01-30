 /*
 * Copyright (c) Gametopia Studios
 * http://www.gametopiastudios.com/
 */
 
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private const string playerSpeedParameter = "PlayerSpeed";

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float velocityX = myRigidbody2D.velocity.x;

        myAnimator.SetFloat(playerSpeedParameter, Mathf.Abs(velocityX));


        if (velocityX < 0f)
        {
            mySpriteRenderer.flipX = true;
        }

        if (velocityX > 0f)
        {
            mySpriteRenderer.flipX = false;
        }
    }

    public void OnJump()
    {

    }
}
