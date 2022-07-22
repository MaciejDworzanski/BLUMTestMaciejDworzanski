using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public float damageTimer;
    public int damageDealOnCollision;

    private void FixedUpdate()
    {
        if (damageTimer >= 0) damageTimer -= Time.fixedDeltaTime;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("playerAttack"))
            TakeDamage(1);

        if(collision.CompareTag("Player"))
        {
            if (transform.position.x < collision.transform.position.x)
                collision.GetComponent<PlayerMove>().TakeDamage(damageDealOnCollision, true);
            else collision.GetComponent<PlayerMove>().TakeDamage(damageDealOnCollision, false);
        }
    }

    protected void TakeDamage(int damage)
    {
        hp -= damage;
        damageTimer = 0.2f;
        if (hp <= 0) Dead(0.5f);
    }

    void Dead(float timeToDestroy)
    {
        Destroy(gameObject, timeToDestroy);
    }
}