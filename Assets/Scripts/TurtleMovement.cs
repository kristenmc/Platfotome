using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMovement : MonoBehaviour
{
    [SerializeField] bool movingRight = false;
    [SerializeField] float speed = 2;
    [SerializeField] bool canFall = false;
    [SerializeField] LayerMask groundCheckLayers = 1 << 8;

    Rigidbody2D rb;
    Collider2D col = null;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (movingRight) {
            if (rightWallCheck() || (!canFall && rightGroundCheck())) {
                movingRight = false;
            }
        } else {
            if (leftWallCheck() || (!canFall && leftGroundCheck())) {
                movingRight = true;
            }
        }

        Vector2 velocity = rb.velocity;
        velocity.x = movingRight ? speed : -speed;
        rb.velocity = velocity;
    }

    public bool rightWallCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(col.bounds.max.x, col.bounds.center.y), new Vector2(0.01f, col.bounds.size.y), 0, Vector2.right, 0.01f, groundCheckLayers.value);
        return hit.transform != null;
    }

    public bool leftWallCheck() {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(col.bounds.min.x, col.bounds.center.y), new Vector2(0.01f, col.bounds.size.y), 0, Vector2.left, 0.01f, groundCheckLayers.value);
        return hit.transform != null;
    }

    public bool rightGroundCheck() {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(col.bounds.max.x, col.bounds.min.y), Vector2.down, 0.02f, groundCheckLayers.value);
        return hit.transform == null;
    }

    public bool leftGroundCheck() {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(col.bounds.min.x, col.bounds.min.y), Vector2.down, 0.02f, groundCheckLayers.value);
        return hit.transform == null;
    }
}
