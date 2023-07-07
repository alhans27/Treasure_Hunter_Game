using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] public float damagePoint;
    private float direction;
    private bool hit;
    private CircleCollider2D coll;
    private Animator anim;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = bulletSpeed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.gameObject.CompareTag("Enemy") || collison.gameObject.CompareTag("Enemy Bats"))
        {
            hit = true;
            coll.enabled = false;
            anim.SetTrigger("Explode");
        }
    }

    public void SetDirection(bool _isFlip, float _direction)
    {
        gameObject.SetActive(true);
        hit = false;
        coll.enabled = true;

        float localScaleX = transform.localScale.x;
        direction = _direction;

        if (!_isFlip)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
