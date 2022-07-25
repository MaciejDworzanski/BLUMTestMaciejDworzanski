using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public GameObject findEdge;
    public GameObject findWall;
    public LayerMask groundLayer;
    public GameObject moveDirection;
    public bool goLeft;
    private bool groundIsThere;
    private bool wallIsHere;
    private float rotationAngle;


    void Start()
    {
        isPatrolling = false;
        if (!goLeft)
        {
            transform.localScale = new(-6, 6, 1);
            rotationAngle = -90;
        }
        else rotationAngle = 90;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckIfGroundIsThere();
    }

    private void CheckIfGroundIsThere()
    {
        groundIsThere = Physics2D.OverlapCircle(findEdge.transform.position, 0.2f, groundLayer);
        wallIsHere = Physics2D.OverlapCircle(findWall.transform.position, 0.1f, groundLayer);
        if (groundIsThere && !wallIsHere)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveDirection.transform.position, speed);
        }
        if (wallIsHere)
        {
            transform.Rotate(new(0, 0, -rotationAngle));
            //transform.position = moveDirection.transform.position;
        }
        if (!groundIsThere)
        {
            transform.Rotate(new(0, 0, rotationAngle));
            transform.position = moveDirection.transform.position;
        }
    }

}
