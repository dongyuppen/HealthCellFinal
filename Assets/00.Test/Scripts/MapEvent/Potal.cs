
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Potal : MonoBehaviour
{
    public Image fadePanel;
    float time = 0f;
    float Fadetime = 1f;

    Rigidbody2D rb;
    Damageable damageable;

    private void Awake()
    {
        damageable = GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>();
        rb = GetComponent<Rigidbody2D>();
    }

    public static Potal Instance // singlton >> singlton call other script function
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Potal>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("Potal");
                    instance = instanceContainer.AddComponent<Potal>();
                }
            }
            return instance;
        }
    }
    private static Potal instance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potal"))
        {

            StartCoroutine(FadeOutIn());
            
        }
    }
    IEnumerator FadeOutIn()
    {
        damageable.isInvincible = true;
        damageable.invincibilityTime = 4;
        fadePanel.gameObject.SetActive(true);
        Color alpha = fadePanel.color;
        while(alpha.a < 1f)
        {
            time += Time.deltaTime / Fadetime;
            alpha.a = Mathf.Lerp(0, 1, time);
            fadePanel.color = alpha;
            yield return null;
        }
        time = 0f;
        rb.drag = 1000f;
        StageMgr.Instance.NextStage();
        yield return new WaitForSeconds(1f);
        rb.drag = 0f;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / Fadetime;
            alpha.a = Mathf.Lerp(1, 0, time);
            fadePanel.color = alpha;
            yield return null;
        }
        fadePanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        damageable.isInvincible = false;
        damageable.invincibilityTime = 1f;
        yield return null;
    }
}
