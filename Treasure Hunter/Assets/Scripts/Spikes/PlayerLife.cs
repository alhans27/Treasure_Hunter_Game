using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private Health healthPlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (healthPlayer.currentHealth <= 0)
            {
                Die();
            }   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            healthPlayer.TakeDamage(1);

            if (healthPlayer.currentHealth > 0)
            {
                Hurt();
            } 

        }
    }

    private void Die()
    {
        //rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Dead");
        RestartLevel();
    }

    private void Hurt()
    {
        rb.velocity = new Vector2(rb.velocity.x, 15f);
        anim.SetBool("Jump", true);
        anim.SetTrigger("Hurt");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
