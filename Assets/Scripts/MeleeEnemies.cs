using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemies : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D hitRange;
    [SerializeField] private float range;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask hitLayer;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(hitRange.bounds.center + transform.right * range * transform.localScale.x * distance,
            new Vector3(hitRange.bounds.size.x * range,hitRange.bounds.size.y,hitRange.bounds.size.z)
            , 0, Vector2.left,0,hitLayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(hitRange.bounds.center + transform.right * range * transform.localScale.x* distance, 
            new Vector3(hitRange.bounds.size.x * range, hitRange.bounds.size.y, hitRange.bounds.size.z));
    }
    
}
