using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private LayerMask groundLayer;
    private bool isGrounded;
    [SerializeField]
    private List<GameObject> groundPoitnts;

    private Rigidbody2D rig;
    PlayerInputActions playerInputActions;

    private float speed;
    [SerializeField]
    private GameObject attackObject;

    private float attackTimer;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        playerInputActions = new();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Attack.performed += Attack;
        attackTimer = 0;
    }

    void FixedUpdate()
    {
        speed = playerInputActions.Player.Movement.ReadValue<float>() / 10;
        HandleTimers();
        Movement();
    }


    public void Movement()
    {
        if (speed > 0) transform.localScale = new(6, 6, 1);
        else if (speed < 0) transform.localScale = new Vector3(-6, 6, 1);
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (attackTimer <= 0)
            {
                attackObject.SetActive(true);
                attackTimer = 0.5f;
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isGrounded = Physics2D.OverlapArea(groundPoitnts[0].transform.position, groundPoitnts[1].transform.position, groundLayer);
            if (isGrounded)
            {
                rig.AddForce(Vector2.up * jumpForce);
            }
        }
    }

    void HandleTimers()
    {
        if(attackTimer >=0)
        {
            attackTimer -= Time.fixedDeltaTime;
        }
    }
}