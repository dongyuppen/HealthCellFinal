using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatForm : MonoBehaviour
{
    private float destroyTime = 2f;

    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y - 0.8f)
            {
                StartCoroutine(Fall());
            }
        }
    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.5f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyTime);
    }
}
