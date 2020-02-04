using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float jump = 3;
    [SerializeField] float jumpTime = 0.5f;

    Rigidbody2D rb = null;
    Collider2D col = null;

    bool jumping = false;
    float holdTime = 0;
    float horizontal = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumping = true;
            holdTime = jumpTime;
        }
        else if (Input.GetButton("Jump") && holdTime > 0)
        {
            holdTime -= Time.deltaTime;
        }
        else
        {
            jumping = false;
            holdTime = 0;
        }

        horizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = horizontal * speed;

        if (jumping)
        {
            velocity.y = 5;
        }

        rb.velocity = velocity;
    }

    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(col.bounds.center.x, col.bounds.center.y), new Vector2(col.bounds.size.x, 0.01f), 0, Vector2.down, 0.01f);
        return hit.transform != null;
    }
}
