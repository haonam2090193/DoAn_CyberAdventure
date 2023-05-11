using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask ladderLayer; 
    [SerializeField] private float climbSpeed; 
    [SerializeField] public bool canAttack; 
    [SerializeField] private float attackDelay; 
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange; 
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int damage; 
    private float originalGravity;

    [Header("Player component")]
  
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D body;

   
    private float dirX; 
    private float vertical; 
    private bool isLadder; 
    private bool isClimbing; 
    private float attackTimer; 

    private enum MovementState { Idle, Running, Jumping, Falling, Climbing }
    private MovementState movementState;
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        originalGravity = body.gravityScale;
    }
    private void Update()
    {
        
        dirX = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        
        isLadder = Physics2D.OverlapCircle(transform.position, 0.2f, ladderLayer);

      
        if (isLadder && Mathf.Abs(vertical) > 0)
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }

        
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        
        Moving();

       
        Jumping();

        
       Climbing();
    }

    private void Moving()
    {
        // Get the horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Set the horizontal velocity of the rigidbody
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

        // Flip the sprite according to the direction of movement
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Jumping()
    {
        // Check if the player is grounded and presses the jump button
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            // Add an upward force to the rigidbody
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
    }

    private void Climbing()
    {
        bool isTouchingLadder = Physics2D.OverlapCircle(transform.position, 0.1f, ladderLayer);

        if (isTouchingLadder && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            body.gravityScale = 0;

            float vertical = Input.GetAxis("Vertical");
            body.velocity = new Vector2(body.velocity.x, vertical * climbSpeed);
        }
        else
        {
            body.gravityScale = 1;
        }
    }

    private bool IsGrounded()
    {
        // Use a box cast to check if the player is touching the ground layer
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void UpdateAnimations()
    {
        if (isClimbing)
        {
            movementState = MovementState.Climbing;
        }
        else if (!IsGrounded() && body.velocity.y < 0)
        {
            movementState = MovementState.Falling;
        }
        else if (!IsGrounded() && body.velocity.y > 0)
        {
            movementState = MovementState.Jumping;
        }
        else if (IsGrounded() && Mathf.Abs(dirX) > 0.01f)
        {
            movementState = MovementState.Running;
        }
        else
        {
            movementState = MovementState.Idle;
        }

        animator.SetInteger("State", (int)movementState);
    }     
}