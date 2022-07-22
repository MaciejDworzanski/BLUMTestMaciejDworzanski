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

   /* override protected void TakeDamage(int damage)
    {
        if (damageTimer <= 0)
        {
            hp -= damage;
            damageTimer = 0.2f;
            if (hp <= 0) Dead(0.5f);
            else anim.SetBool("isHit", true);
        }
    }*/
    


    override protected void Dead(float timeToDestroy)
    {
        Destroy(gameObject, timeToDestroy);
        anim.SetBool("isDead", true);
        Destroy(this);
    }
}
