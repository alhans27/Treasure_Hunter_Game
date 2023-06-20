using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 21f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("p_speed", Mathf.Abs(horizontal));
        Flip();

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log(rb.velocity);
            animator.SetBool("p_isJumping", true);
        }

        Falling();

        // if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        // }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        // return Physics2D.OverlapCircle(groundCheck.position, -0.2f, groundLayer);
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    private void Falling()
    {
        if (rb.velocity.y < -0.1f)
        {
            animator.SetBool("p_isFalling", true);
            animator.SetBool("p_isJumping", false);
        }
        else if (rb.velocity.y > 1f)
        {
            animator.SetBool("p_isFalling", false);
        }
        else
        {
            animator.SetBool("p_isFalling", false);
        }
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            sprite.flipX = isFacingRight;
            isFacingRight = !isFacingRight;
            // Vector3 localScale = transform.localScale;
            // localScale.x *= -1f;
            // transform.localScale = localScale;
        }
    }
}
