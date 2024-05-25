using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    private RandomCircleDamage randomCircleDamage;

    // Start is called before the first frame update
    void Start()
    {
        randomCircleDamage = FindObjectOfType<RandomCircleDamage>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            randomCircleDamage.StartBombSpawning();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            randomCircleDamage.StopBombSpawning();
        }
    }
}
