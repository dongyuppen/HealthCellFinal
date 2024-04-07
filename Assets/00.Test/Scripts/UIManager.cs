using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Prefabs for damage and health text
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    // Reference to the game canvas
    public Canvas gameCanvas;

    private void Awake()
    {
        // Find the canvas in the scene
        gameCanvas = FindObjectOfType<Canvas>();
    }

    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        // Subscribe to character damaged and healed events
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    // OnDisable is called when the behaviour becomes disabled
    private void OnDisable()
    {
        // Unsubscribe from character damaged and healed events
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    // Method to handle character taking damage
    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        // Create text at character hit position
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // Instantiate damage text prefab on the canvas
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        // Set the text to display the damage received
        tmpText.text = damageReceived.ToString();
    }

    // Method to handle character being healed
    public void CharacterHealed(GameObject character, int healthRestored)
    {
        // Create text at character heal position
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // Instantiate health text prefab on the canvas
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        // Set the text to display the health restored
        tmpText.text = healthRestored.ToString();
    }
}
