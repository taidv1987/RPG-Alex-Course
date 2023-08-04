using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;

    protected int facingDir = 1;
    protected bool facingRight = true;

    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float distanceToGround;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float distanceToWall;
    [Space]
    [SerializeField] protected LayerMask setGroundLayer;

    protected bool isGrounded;
    protected bool isWallDetected;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        if(wallCheck == null)
        {
            wallCheck = transform;
        }


    }


    protected virtual void Update()
    {
        CollisionCheck();
    }

    protected virtual void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, distanceToGround, setGroundLayer);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, distanceToWall * facingDir, setGroundLayer);
    }

    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - distanceToGround));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + distanceToWall * facingDir, wallCheck.position.y));
    }
}
