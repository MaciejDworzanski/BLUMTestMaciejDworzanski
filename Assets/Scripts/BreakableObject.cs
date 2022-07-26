using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public int minimumCoinDrop;
    public int maximumCoinDrop;
    public GameObject money;
    private Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim = GetComponent<Animator>();
        if(collision.CompareTag("playerAttack"))
        {
            DestroyMe();
        }
    }

    void DestroyMe()
    {
        if (money != null)
        {
            int randomDrop = Random.Range(minimumCoinDrop, maximumCoinDrop);
            anim.SetBool("isDestroyed", true);
            for (int i = 0; i < randomDrop; i++)
            {
                GameObject coin = Instantiate(money, transform.position, Quaternion.identity);
                float randomNumber = Random.Range(0.5f, 2.8f);
                coin.GetComponent<Rigidbody2D>().AddForce(350 * new Vector2(Mathf.Cos(randomNumber), Mathf.Sin(randomNumber)));
            }
        }
        Destroy(this);
    }

}
