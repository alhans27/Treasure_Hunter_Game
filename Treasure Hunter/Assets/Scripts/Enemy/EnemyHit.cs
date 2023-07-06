using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHit : MonoBehaviour
{
    [SerializeField] private float damagePoint;
    [SerializeField] private float hitTime;
    [SerializeField] private Animator anim;
    [SerializeField] private EnemyPatrol patrol;
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
        if(Collide.name == "Player")
        {
            if (_canHit == true)
            {
                if (anim)
                {
                    patrol.Behaviour("Attack");
                }
                Debug.Log ("You got Hit");
                player.TakeDamage(damagePoint);
                _hitTimer = 0;
            }
        }
    }
}
