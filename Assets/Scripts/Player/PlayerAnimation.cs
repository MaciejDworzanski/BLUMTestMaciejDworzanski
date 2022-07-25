using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update


    public void AttackAnimation()
    {
        anim.SetBool("isAttacking", true);
    }

    public void EndAttack()
    {
        anim.SetBool("isAttacking", false);
    }
    public void FallingAnimation()
    {
        anim.SetBool("isFalling", true);
    }

    public void EndFalling()
    {
        anim.SetBool("isFalling", false);
    }

    public void JumpAnimation()
    {
        anim.SetBool("isJumping", true);
    }

    public void EndJump()
    {
        anim.SetBool("isJumping", false);
    }

    public void RunningAnimation()
    {
        anim.SetBool("isRunning", true);
    }

    public void EndRunning()
    {
        anim.SetBool("isRunning", false);
    }

    public void DeadAnimation()
    {
        anim.SetBool("isDead", true);
    }
}
