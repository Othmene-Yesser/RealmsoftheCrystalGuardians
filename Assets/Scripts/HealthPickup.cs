using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestored = 10;

    AudioSource pickupSound;

    private void Awake()
    {
        pickupSound= GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damageable = collision.GetComponent<Damagable>();

        if (damageable)
        {
            bool wasHealed = damageable.heal(healthRestored);
            if (wasHealed)
            {
                if (pickupSound)
                {
                    AudioSource.PlayClipAtPoint(pickupSound.clip, gameObject.transform.position, pickupSound.volume);
                }
                Destroy(gameObject);
            }
        }
    }
}
