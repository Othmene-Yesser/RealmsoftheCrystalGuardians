using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 7;
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //see if it can be hit
        Damagable damageable = collision.GetComponent<Damagable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockBack = transform.parent.localScale.x > 0 ? knockback: new Vector2(-knockback.x, knockback.y);
            //hit traget
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockBack);
            if (gotHit)
            {
                Debug.Log(collision.name + " been Hit for"+ attackDamage);
            }
            
        }
        
    }
}
