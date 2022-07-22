using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public float damageTimer;
    public int damageDealOnCollision;
    public GameObject leftPatrolPoint;
    
    public GameObject rightPatrolPoint;
    protected bool isPatrolling;
    [SerializeField]
    protected float speed;
    protected bool goingLeft;

    protected float leftBorder;
    protected float rightBorder;

    protected void Awake()
    {
        if (leftPatrolPoint != null && rightPatrolPoint != null)
        {
            isPatrolling = true;
            leftBorder = leftPatrolPoint.transform.position.x;
            rightBorder = rightPatrolPoint.transform.position.x;
        }
        else isPatrolling = false;
    }

    protected void FixedUpdate()
    {
        if (damageTimer >= 0) damageTimer -= Time.fixedDeltaTime;
        if (isPatrolling) Patrolling();
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("playerAttack"))
            TakeDamage(1);

        if(collision.CompareTag("Player"))
        {
            if (transform.position.x < collision.transform.position.x)
                collision.GetComponent<PlayerMove>().TakeDamage(damageDealOnCollision, false);
            else collision.GetComponent<PlayerMove>().TakeDamage(damageDealOnCollision, true);
        }
    }

    protected void TakeDamage(int damage)
    {
        if (damageTimer <= 0)
        {
            hp -= damage;
            damageTimer = 0.2f;
            if (hp <= 0) Dead(0.5f);
        }
    }

    virtual protected void Dead(float timeToDestroy)
    {
        Destroy(gameObject, timeToDestroy);
        Destroy(this);
    }

    protected void Patrolling()
    {
        if (goingLeft)
        {
            transform.position = new(transform.position.x - speed, transform.position.y);
            if (transform.position.x < leftBorder)
            {
                goingLeft = false;
                transform.localScale = new(-transform.localScale.x, transform.localScale.y);
            }
        }
        else
        {
            transform.position = new(transform.position.x + speed, transform.position.y);
            if (transform.position.x > rightBorder)
            {
                goingLeft = true;
                transform.localScale = new(-transform.localScale.x, transform.localScale.y);
            }
        }
    }
}