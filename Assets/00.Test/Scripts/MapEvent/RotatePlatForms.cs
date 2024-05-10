using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatForms : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.freezeRotation = false;
        }
    }    

    void Update()
    {
        
    }
}
