using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronDamaged : MonoBehaviour
{
    public int thronDamage = 10;
    public int NockbackX = 5;
    public int NockbackY = 5;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Left & right check / Player Damage & Nockback
            if (transform.position.x > collision.transform.position.x)
            {
                Damageable damageable = collision.GetComponent<Damageable>();
                Vector2 nockback = new Vector2(-NockbackX, NockbackY);
                bool gotHit = damageable.Hit(thronDamage, nockback);
            } else if (transform.position.x < collision.transform.position.x)
            {
                Damageable damageable = collision.GetComponent<Damageable>();
                Vector2 nockback = new Vector2(NockbackX, NockbackY);
                bool gotHit = damageable.Hit(thronDamage, nockback);
            }
           
        }
    }
}
