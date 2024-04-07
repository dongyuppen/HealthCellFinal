using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HealthBar : MonoBehaviour
{
    // Reference to the slider UI element for health visualization
    public Slider healthSlider;
    // Reference to the text UI element for displaying health information
    public TMP_Text healthBarText;

    // Reference to the Damageable component attached to the player
    Damageable playerDamageable;

    private void Awake()
    {
        // Finding the GameObject tagged as "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Checking if the player GameObject is found
        if (player == null)
        {
            Debug.Log("No player found in the scene. Make sure it has tag 'Player'");
        }

        // Getting the Damageable component attached to the player GameObject
        playerDamageable = player.GetComponent<Damageable>();
    }

    
    void Start()
    {
        // Setting initial health values on the UI elements
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    // Called when the object becomes enabled and active
    private void OnEnable()
    {
        // Adding listener to the healthChanged event of the player's Damageable component
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    // Called when the object becomes disabled or inactive
    private void OnDisable()
    {
        // Removing listener from the healthChanged event of the player's Damageable component
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    // Method to calculate the percentage of health for updating the slider
    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    // Callback method for handling player health changes
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        // Updating health values on the UI elements
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }
}
