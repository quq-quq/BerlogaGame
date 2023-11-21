using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    private Vector2 currentPosition, previousPosition, direction;
    private bool _isFacingRight;

    protected void BaseStart()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    protected void BaseUpdate()
    {
        currentPosition = transform.position;

        direction = currentPosition - previousPosition;
        previousPosition = currentPosition;


        if (direction.y != 0 )
        {
            anim.SetBool("isJumping", true);

        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if (direction.x != 0)
        {
            Flip(direction.x);
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }


    }

    private void Flip( float directionX)
    {
        if((directionX > 0))
        {
            transform.localScale = new Vector2(1, 1);
        }
        if(directionX < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

}
