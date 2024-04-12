using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MonsterHealthBar : MonoBehaviour
{
    // Reference to the slider UI element for health visualization
    public Slider mHealthSlider;
   

    // Reference to the Damageable component attached to the player
    EnemyDamageable EnemyDamageable;

    private void Awake()
    {
        // Finding the GameObject tagged as "Player"
        GameObject enemy = GameObject.FindGameObjectWithTag("Boss");

        // Checking if the player GameObject is found
        if (enemy == null)
        {
            Debug.Log("No player found in the scene. Make sure it has tag 'Boss'");
        }

        // Getting the Damageable component attached to the player GameObject
        EnemyDamageable = enemy.GetComponent<EnemyDamageable>();
    }

    
    void Start()
    {
        // Setting initial health values on the UI elements
        mHealthSlider.value = CalculateSliderPercentage(EnemyDamageable.Health, EnemyDamageable.MaxHealth);
        
    }

    // Called when the object becomes enabled and active
    private void OnEnable()
    {
        // Adding listener to the healthChanged event of the player's Damageable component
       EnemyDamageable.healthChanged.AddListener(OnMonsterHealthChanged);
    }

    // Called when the object becomes disabled or inactive
    private void OnDisable()
    {
        // Removing listener from the healthChanged event of the player's Damageable component
        EnemyDamageable.healthChanged.RemoveListener(OnMonsterHealthChanged);
    }

    // Method to calculate the percentage of health for updating the slider
    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    // Callback method for handling player health changes
    private void OnMonsterHealthChanged(int newHealth, int maxHealth)
    {
        // Updating health values on the UI elements
        mHealthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        
    }
}
