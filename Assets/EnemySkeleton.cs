using UnityEngine;

public class EnemySkeleton : Entity
{
    bool isAttacking;

    [Header("Move info")]
    [SerializeField] private float moveSpeed;

    [Header("Player detection")]
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private LayerMask playerLayer;
    private RaycastHit2D isPlayerDetected;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(facingDir * moveSpeed * 3f, rb.velocity.y);
                isAttacking = false;

                Debug.Log("Warning! see the player");
            }
            else
            {
                Debug.Log("Attack" + isPlayerDetected.collider.gameObject.name);
                isAttacking = true;
            }
        }

        if (!isGrounded || isWallDetected)
        {
            Flip();
        }

        Movement();
    }

    private void Movement()
    {
        if (!isAttacking)
        {
            rb.velocity = new Vector2(facingDir * moveSpeed, rb.velocity.y);
        }
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, distanceToPlayer * facingDir, playerLayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + distanceToPlayer * facingDir, transform.position.y));
    }
}
