using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float speedAcceleration;
    public LayerMask groundLayer;
    private bool isGrounded;
    [SerializeField]
    private List<GameObject> groundPoitnts;
    private Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Jump();
        Move();
    }

    private void Move()
    {
        bool isMoving = false;
        //if(Physics2D.OverlapBox(transform.position, new(0.035))
        if(Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            if (speed > -maxSpeed)
            {
                speed -= speedAcceleration;
            }
            else speed = -maxSpeed;
        }
        if(Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            if (speed < maxSpeed)
            {
                speed += speedAcceleration;
            }
            else speed = maxSpeed;
        }
        if (!isMoving) speed = 0;
        transform.position = new(transform.position.x + speed, transform.position.y);
        //rig.MovePosition(new Vector2(rig.position.x + speed, rig.position.y + rig.velocity.y));
        /* if (rig.velocity.x < 5) rig.MovePosition(new Vector2(rig.position.x + speed, rig.position.y));
         if (!isMoving)
         {
             speed = 0;
             rig.velocity = new(0, rig.velocity.y);
         }*/
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapArea(groundPoitnts[0].transform.position, groundPoitnts[1].transform.position, groundLayer);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(Vector2.up * jumpForce);
        }
    }
}