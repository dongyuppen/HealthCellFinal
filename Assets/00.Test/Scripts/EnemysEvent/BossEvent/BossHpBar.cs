using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpBar : MonoBehaviour
{
    public GameObject BossHpbar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossHpbar.SetActive(true);
        }
    }
}
