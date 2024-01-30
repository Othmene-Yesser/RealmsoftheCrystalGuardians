using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 5;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2 (1.5f, 0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //see if it can be hit
        Damagable damageable = collision.GetComponent<Damagable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockBack = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            //hit traget
            bool gotHit = damageable.Hit(damage, deliveredKnockBack);
            if (gotHit)
            {
                Debug.Log(collision.name + " been Hit for" + damage);
                Destroy(gameObject);
            }

        }
    }

}
