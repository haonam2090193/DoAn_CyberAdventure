using UnityEngine;

public class Ranged : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage = 10f;

    [SerializeField] private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float range = 1f;
    [SerializeField] private float colliderDistance = 0.5f;
    [SerializeField] private BoxCollider2D boxCollider;

    public Transform target; // The target to shoot
    public GameObject bulletPrefab; // The bullet prefab to use
    public float bulletSpeed = 10f; // The speed of the bullet
    public float fireRate = 1f; // The rate of fire
    public float shootDistance = 10f; // The maximum distance to shoot from
    public Transform shootPoint; // The point where the bullet will spawn
    [SerializeField] private bool isAttacking = false;
    private bool isMoving;
    private Vector3 targetPosition;
    private Animator animator;

    private Transform player;
    private float nextFireTime = 0.0f;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        targetPosition = pointA.position;
        animator = GetComponent<Animator>();
    } 
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            animator.SetBool("moving", false);
            isMoving = false;
            if (cooldownTimer >= attackCooldown)
            {
                Attack();
            }

        }
        else
            {
                isAttacking = false;
                animator.SetBool("Attack", false);
                animator.SetBool("moving", true);
                isMoving = true;
            }

        if (isMoving)
            {
                Move();
            }
    }
    private void Attack()
    {
        cooldownTimer = 0;
        animator.SetTrigger("Attack");
        Debug.Log("shot");
    }
    private void Move()
    {
        if (isAttacking || !isMoving)
        {
            return;
        }

        Vector3 direction = (targetPosition - transform.position).normalized;
        float distanceSquared = (targetPosition - transform.position).sqrMagnitude;

        transform.Translate(direction * speed * Time.deltaTime);
        animator.SetBool("moving", true);

        if (distanceSquared < 0.01f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
            Flip();
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
             Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
             0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
