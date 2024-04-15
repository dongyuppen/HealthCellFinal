using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    public int JumpPower = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("������!");

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Determine if the "Player" GameObject has Rigidbody2D components
            if (playerRb != null)
            {
                // Apply jumping force to "Player" GameObject
                Vector2 jumpPowerVector = new Vector2(0, JumpPower);
                playerRb.AddForce(jumpPowerVector, ForceMode2D.Impulse);
            }
        }
    }

}