using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour  
{
    private Camera mainCam;
    private Vector3 mousePos;
    private PlayerMovement PlayerMovement;
    private Animator anim;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        anim = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && PlayerMovement.canAttack())
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
        
    }
    private void Attack() 
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }
}
