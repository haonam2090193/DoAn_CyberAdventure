using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("meleeAttack");
        
    }
}
