using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
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
    private bool isAttacking;
    private bool isFalling;
    private bool isJumping;
    private bool isRunning;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerInputActions = new();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Attack.performed += Attack;
        attackTimer = 0;
    }

    void FixedUpdate()
    {
        Falling();
        speed = playerInputActions.Player.Movement.ReadValue<float>() / 10;
        HandleTimers();
        Movement();
    }


    public void Movement()
    {
        if (speed != 0)
        {
            if (speed > 0)
            {
                transform.localScale = new(6, 6, 1);
            }
            else if (speed < 0)
            {
                transform.localScale = new Vector3(-6, 6, 1);
            }
            if (!isRunning)
            {
                isRunning = true;
                playerAnimation.RunningAnimation();
            }
        }
        else if(isRunning)
        {
            isRunning = false;
            playerAnimation.EndRunning();
        }
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (attackTimer <= 0)
            {
                isAttacking = true;
                attackObject.SetActive(true);
                attackTimer = 0.5f;
                playerAnimation.AttackAnimation();
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded)
            {
                isJumping = true;
                playerAnimation.JumpAnimation();
                rig.AddForce(Vector2.up * jumpForce);
            }
        }
    }

    public void Falling()
    {
        isGrounded = Physics2D.OverlapArea(groundPoitnts[0].transform.position, groundPoitnts[1].transform.position, groundLayer);
        if (rig.velocity.y < 0)
        {
            isJumping = false;
            isFalling = true;
            playerAnimation.FallingAnimation();
            playerAnimation.EndJump();
        }
        if(isGrounded && isFalling)
        {
            isFalling = false;
            playerAnimation.EndFalling();
        }
    }

    void HandleTimers()
    {
        if (attackTimer >= 0)
        {
            attackTimer -= Time.fixedDeltaTime;
        }
        else if (isAttacking)
        {
            isAttacking = false;
            attackObject.SetActive(false);
            playerAnimation.EndAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("money"))
        {
            Destroy(collision.gameObject);
            Global.Instance.money++;
        }
    }
}