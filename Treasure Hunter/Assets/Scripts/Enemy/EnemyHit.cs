using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHit : MonoBehaviour
{
    [SerializeField] private float damagePoint;
    [SerializeField] private float hitTime;
    [SerializeField] private float health;
    [SerializeField] private Animator anim;
    [SerializeField] private EnemyPatrol patrol;
    [SerializeField] private FireBullet playerDamage;

    private float _hitTimer = 0;
    private bool _canHit = true;
    public Health player;
    

    private void Update() {
        _hitTimer += Time.deltaTime;

        if (_hitTimer > hitTime)
        {
            _canHit = true;
        } else
        {
        _canHit = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D Collide)
    {
        if(Collide.name == "Player" && _canHit == true)
        {
            if (!gameObject.CompareTag("Enemy Bats"))
            {
                patrol.Behaviour("Attack");
            }
            player.TakeDamage(damagePoint);
            _hitTimer = 0;
        }

        if(Collide.name == "Fire Bullet")
        {
            TakeDamage(playerDamage.damagePoint);
        }
    }



    public void TakeDamage(float dp)
    {
        health -= dp;
        if (health > 0)
        {
            anim.SetTrigger("Hit");
            anim.SetBool("Walk", true);
        } else {
            Destroy(gameObject, 0.5f);
            anim.SetTrigger("Dead");
        }
    }
}
