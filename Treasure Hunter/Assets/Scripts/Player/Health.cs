using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField]
    private float startingHealth { get; set; }
    public float currentHealth { get; private set; }

    private Animator anim;

    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Player Hurt
            anim.SetTrigger("Hurt");
        }
        else
        {
            // Player Dead
            if (!dead)
            {
                anim.SetTrigger("Dead");
                GetComponent<PlayerController>().enabled = false;
                dead = true;
            }
        }
    }

    // For Debuging Enemy
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     TakeDamage(1);
        // }
    }

    public void LoadHealth(float health)
    {
        StartCoroutine(Load(health));
    }

    private IEnumerator Load(float health)
    {
        yield return null;
        currentHealth = health;
    }
}
