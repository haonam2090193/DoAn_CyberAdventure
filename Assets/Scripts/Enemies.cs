using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    private float movingSpeed = 2f;
    [SerializeField]
    private GameObject[] Waypoints;
    private int curWaypointIndex = 0;
    Animator anim;
    SpriteRenderer spriteRenderer;
    
    private void Start()
    {
       
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }
    void Update()
    {
        EMoving();
    }
    private void EMoving()
    {
        if (Vector2.Distance(Waypoints[curWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            curWaypointIndex++;
            if (curWaypointIndex >= Waypoints.Length)
            {
                curWaypointIndex = 0;
            }
        }
        anim.SetBool("IsRunning", true);
        transform.position = Vector2.MoveTowards(transform.position, Waypoints[curWaypointIndex].transform.position, movingSpeed * Time.deltaTime);
        Flip();
    }
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= 1;
        transform.localScale = localScale;
    }

}
