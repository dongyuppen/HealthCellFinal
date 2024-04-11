using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    // Movement speed of the text object in pixels per second
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    // Time taken for the text to fade out in seconds
    public float timeToFade = 1f;

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    // Variables for tracking time elapsed and initial color of the text
    private float timeElapsed = 0f;
    private Color startColor;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    private void Update()
    {
        // Move the text object based on moveSpeed
        textTransform.position += moveSpeed * Time.deltaTime;

        // Update time elapsed
        timeElapsed += Time.deltaTime;

        // Fade out the text gradually over time
        if (timeElapsed < timeToFade)
        {
            // Calculate the alpha value for fading out
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            // Set the color of the text with adjusted alpha value
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            // If time to fade is elapsed, destroy the text object
            Destroy(gameObject);
        }
    }
}
