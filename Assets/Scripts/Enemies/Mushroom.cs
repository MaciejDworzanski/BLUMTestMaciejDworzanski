using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        if (isPatrolling) anim.SetBool("isPatrolling", true);
    }

    override protected void Dead(float timeToDestroy)
    {
        transform.GetChild(0).gameObject.tag = "Untagged";
        anim.SetBool("isDead", true);
        base.Dead(timeToDestroy);
        Destroy(this);
    }
}
