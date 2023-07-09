using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;
    private float cooldownTimer = Mathf.Infinity;
    private Vector3 position;
    private CheckpointMaster cm;

    [Header("Required Player Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Player Move and Jump")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 21f;

    [Header("Player Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBullets;

    private void Start() {
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();
        transform.position = cm.lastCheckpointPos;
    }

    // Update is called once per frame
    void Update()
    {
        // Player Walk Right and Left
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Flip();

        // Player Jumping
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("Jump", true);
        }

        // if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        // }

        // Player Falling
        Falling();

        // Player Attack
        position = firePoint.position;
        if (Input.GetButtonDown("Fire1") && cooldownTimer > attackCooldown)
        {
            animator.SetTrigger("Fire");
        }

        cooldownTimer += Time.deltaTime;

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
        if (rb.velocity.y < -0.1f && !isGrounded())
        {
            animator.SetBool("Fall", true);
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Fall", false);
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

    private void Attack()
    {
        cooldownTimer = 0;

        //FirePoint Position
        if (!isFacingRight)
        {
            position.x -= 1;
        }

        // Pooling Firebullet
        fireBullets[FindFireball()].GetComponent<FireBullet>().Position(firePoint.position.x);
        fireBullets[FindFireball()].transform.position = position;
        fireBullets[FindFireball()].GetComponent<FireBullet>().SetDirection(isFacingRight, Mathf.Sign(isFacingRight? 1 : -1));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireBullets.Length; i++)
        {
            if (!fireBullets[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
