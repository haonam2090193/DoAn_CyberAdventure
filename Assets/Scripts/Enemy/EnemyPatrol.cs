using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 2f;
    public float MovingSpeed 
    { get{ return movingSpeed; }
        set { movingSpeed = value; } }
    [SerializeField] private GameObject[] PatrolPoint;
    [SerializeField] private int currentPatrolIndex = 0;

    [SerializeField] private Animator anim;
    private MeleeEnemies meleeEnemies;
    private void Start()
    {
        meleeEnemies = GetComponent<MeleeEnemies>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetBool("moving", true);
        if (Vector2.Distance(PatrolPoint[currentPatrolIndex].transform.position, transform.position) < 0.1f)
        {           
            currentPatrolIndex++;
            if (currentPatrolIndex >= PatrolPoint.Length)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                currentPatrolIndex = 0;
            }
            else 
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, PatrolPoint[currentPatrolIndex].transform.position, movingSpeed * Time.deltaTime);
        if (meleeEnemies.PlayerInSight())
        {
            anim.SetBool("moving", false);
            movingSpeed = 0f;
        }
        else
        {
            movingSpeed = 2f;
        }
    }       
}
