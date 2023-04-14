using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
     private Animator animator;
     private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX;
    private Rigidbody2D body;

    private enum MovementState { Idle, Running, Jumping, Falling }
    private MovementState movementState;
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Moving();

        Jumping();

        UpdateAnimations();
    }
    private void Moving()
    {
         dirX = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, body.velocity.y);
        if(dirX > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (dirX < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void UpdateAnimations()
    {
        if (dirX > 0f)
        {
            
            movementState = MovementState.Running;
        }
        else if (dirX < 0f)
        {
           
            movementState = MovementState.Running;
        }
        else
        {
            movementState = MovementState.Idle;
        }

        if (body.velocity.y > 0.1f)
        {
            movementState = MovementState.Jumping;
        }
        else if (body.velocity.y < -0.1f)
        {
            movementState = MovementState.Falling;
        }

        animator.SetInteger("State", (int)movementState);
    }
    public bool canAttack()
    {
        return dirX == 0 && IsGrounded();
    }
}
