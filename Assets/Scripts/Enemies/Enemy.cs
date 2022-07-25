using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    protected float damageTimer;
    public int damageDealOnCollision;
    public GameObject leftPatrolPoint;
    private SpriteRenderer sprite;
    //private float maxDamageTimer;
    
    public GameObject rightPatrolPoint;
    protected bool isPatrolling;
    [SerializeField]
    protected float speed;
    protected bool goingLeft;

    protected float leftBorder;
    protected float rightBorder;

    protected void Awake()
    {
        //maxDamageTimer = 0.2f;
        sprite = GetComponent<SpriteRenderer>();
        if (leftPatrolPoint != null && rightPatrolPoint != null)
        {
            isPatrolling = true;
            leftBorder = leftPatrolPoint.transform.position.x;
            rightBorder = rightPatrolPoint.transform.position.x;
        }
        else isPatrolling = false;
    }

    virtual protected void FixedUpdate()
    {
        EnemyFixedUpdate();
    }

    protected void EnemyFixedUpdate()
    {
        if (damageTimer >= 0) damageTimer -= Time.fixedDeltaTime;
        if (isPatrolling) Patrolling();
        if (damageTimer > 0) ChangeColorOnHit();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("playerAttack"))
            TakeDamage(1);
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("playerAttack"))
            TakeDamage(1);
    }

    protected void DealDamageToPlayer(GameObject player)
    {
        if (player.GetComponent<PlayerMove>())
            if (transform.position.x < player.transform.position.x)
                player.GetComponent<PlayerMove>().TakeDamage(damageDealOnCollision, false);
            else player.GetComponent<PlayerMove>().TakeDamage(damageDealOnCollision, true);
    }

    protected void ChangeColorOnHit()
    {
        float color = 1 - damageTimer * 5;
        sprite.color = new(1, color, color, 1);
    }

    virtual protected void TakeDamage(int damage)
    {
        if (damageTimer <= 0)
        {
            hp -= damage;
            damageTimer = 0.5f;
            if (hp <= 0) Dead(0.5f);
        }
    }

    virtual protected void Dead(float timeToDestroy)
    {
        Destroy(gameObject, timeToDestroy);
        Destroy(this);
    }

    virtual protected void Patrolling()
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