using UnityEngine;

public class MeleeEnemies : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    [SerializeField] private Animator anim;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
   
    [SerializeField] private LayerMask playerLayer;
    private void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;              
        DamagePlayer();
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {            
            if (cooldownTimer >= attackCooldown)
            {
        
                anim.SetTrigger("attack");
                cooldownTimer = 0;
              playerHealth.TakeDamage(damage);               
            }
        }     
    }
    
    
    public bool PlayerInSight()
        {
         RaycastHit2D hit =
               Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
               new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);
            return hit.collider != null;
        }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
