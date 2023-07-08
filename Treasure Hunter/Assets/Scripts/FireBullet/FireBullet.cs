using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletRange;
    [SerializeField] public float damagePoint;
    private float pos;
    private float direction;
    private bool hit;
    private bool flip = true;
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
        if ((transform.position.x > (pos + bulletRange) && direction == 1) || (transform.position.x < (pos - bulletRange) && direction == -1))
        {
            hit = true;
            coll.enabled = false;
            anim.SetTrigger("Explode");
        }
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
    
    public void Position(float p)
    {
        pos = p;
    }

    public void SetDirection(bool _isFlip, float _direction)
    {
        gameObject.SetActive(true);
        hit = false;
        coll.enabled = true;

        float localScaleX = transform.localScale.x;
        direction = _direction;

        if (_isFlip != flip)
        {
            localScaleX = -localScaleX;
            flip = _isFlip;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
