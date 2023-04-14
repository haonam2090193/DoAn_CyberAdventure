using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float lSpeed = 5f;
    private bool isLadder;
    private bool isClimbing;
    [SerializeField] private Animator anim;

    [SerializeField] private Rigidbody2D rb;
     
    void Update()
    {
        Climbing();
    }
    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * lSpeed);
            anim.SetBool("IsClimbing",true);
        }
        else
        {         
            rb.gravityScale = 2.5f;
            anim.SetBool("IsClimbing", false);
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
    private void Climbing()
    {
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0)
        {
            isClimbing = true;
        }             
    }
}
