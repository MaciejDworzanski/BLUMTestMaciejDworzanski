using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    public Animator anim;
    
    void Start()
    {
        if (isPatrolling) anim.SetBool("isPatrolling", true);
    }

    override protected void FixedUpdate()
    {
        EnemyFixedUpdate();
    }
    


    override protected void Dead(float timeToDestroy)
    {
        Destroy(gameObject, timeToDestroy);
        anim.SetBool("isDead", true);
        Destroy(this);
    }
}
