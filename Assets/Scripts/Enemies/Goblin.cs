using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    private Animator anim;
    public float distanceToAttack;
    public GameObject attackObject;
    public GameObject frontDirection;
    public LayerMask playerLayer;

    private bool playerIsClose;
    private float attackTimer;
    private bool isAttacking;
    

    private void Start()
    {
        playerIsClose = false;
        isAttacking = false;
        anim = GetComponent<Animator>();
        if (isPatrolling) anim.SetBool("isPatrolling", true);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckIfPlayerIsClose();
    }

    override protected void Dead(float timeToDestroy)
    {
        anim.SetBool("isDead", true);
        transform.GetChild(0).gameObject.tag = "Untagged";
        transform.GetChild(1).gameObject.tag = "Untagged";
        base.Dead(timeToDestroy);
    }

    private void CheckIfPlayerIsClose()
    {
        //RaycastHit2D hit;
        Vector2 dir = frontDirection.transform.position - transform.position;
        
        playerIsClose = Physics2D.Raycast(transform.position, dir, distanceToAttack, playerLayer);

        Debug.DrawRay(transform.position, dir * distanceToAttack, Color.blue, 0.1f);
        if (playerIsClose && attackTimer <= 0) StartCoroutine(Attack());
        if (attackTimer > 0) attackTimer -= Time.fixedDeltaTime;
    }

    protected override void Patrolling()
    {
        if (!isAttacking)
        {
            base.Patrolling();
        }
    }

    public IEnumerator Attack()
    {
        anim.SetBool("isAttacking", true);
        isAttacking = true;
        attackObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
        anim.SetBool("isAttacking", false);
        attackObject.SetActive(false);
        attackTimer = 1;
    }
}
