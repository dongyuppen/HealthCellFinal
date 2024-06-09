using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float flashDuration = 0.1f;
    public int numberOfFlashes = 3;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void OnBossDefeated()
    {
        StartCoroutine(FlashAndShake());
    }

    private IEnumerator FlashAndShake()
    {
        // Flashing effect
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }

        // Shaking effect
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

       
        transform.position = originalPosition;
    }
}