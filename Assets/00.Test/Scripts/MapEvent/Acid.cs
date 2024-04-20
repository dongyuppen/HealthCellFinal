using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Acid : MonoBehaviour
{
    public int acidDamage = 40;

    public static bool isAcid = false;
    [SerializeField] private float acidDrag;
    private float worldDrag = 10;

    void Start()
    {
        //Nomal Drag is Zero
        worldDrag = 0;        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            //Acid damage is not nockback
            Vector2 Notnockback = new Vector2(0, 0);
            bool gotHit = damageable.Hit(acidDamage, Notnockback);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InAcid(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OutAcid(collision);
        }
    }

    private void InAcid(Collider2D Player)
    {
        isAcid = true;
        //if Player in a Acid, player drag rises
        Player.transform.GetComponent<Rigidbody2D>().drag = acidDrag;
    }


    private void OutAcid(Collider2D Player)
    {
        if(isAcid)
        {
            isAcid = false;
            //if Player out acid, player drag is normalization
            Player.transform.GetComponent<Rigidbody2D>().drag = worldDrag;
        }
    }
}

