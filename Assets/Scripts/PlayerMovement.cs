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

    private float vertical;
    private float lSpeed = 5f;
    private bool isLadder;
    private bool isClimbing;

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

        Climbing();
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
    private void Climbing()
    {
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0)
        {
            isClimbing = true;
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
    private void FixedUpdate()
    {
        if (isClimbing)
        {
            body.gravityScale = 0f;
            body.velocity = new Vector2(body.velocity.x, vertical * lSpeed);
            animator.SetBool("IsClimbing", true);
        }
        else
        {
            body.gravityScale = 2.5f;
            animator.SetBool("IsClimbing", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
   
}
