using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public delegate void AnimationsEvent(Vector2 direction, Animator anim);

    public AnimationsEvent animationsEvent;



    public void StartForPlayersEvent()
    {
        animationsEvent += RunDetecting;
        animationsEvent += JumpDetecting;
    }

    private void RunDetecting(Vector2 direction, Animator anim)
    {
        if (direction.x != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void JumpDetecting(Vector2 direction, Animator anim)
    {
        if (direction.y != 0)
        {
            anim.SetBool("isJumping", true);

        }
        else
        {
            anim.SetBool("isJumping", false);
        }
    }

}
