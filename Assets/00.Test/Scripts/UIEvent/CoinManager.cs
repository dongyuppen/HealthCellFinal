using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    // Reference to the ScriptableObject containing player's data
    public SOPlayer playerData;



    public int coins = 0;
    [SerializeField] private TMP_Text coinsDisplay;

    private void Awake()
    {

        if (!instance)
        {
            instance = this;
        }
        InitializeCoinsFromPlayerData();
        UpdateCoinsDisplay();
    }

   

    public void UpdateCoinsDisplay()
    {
        coinsDisplay.text = coins.ToString();
    }

    public void ChangeCoins(int amount)
    {
        coins += amount;
        UpdateCoinsDisplay();
    }


    private void InitializeCoinsFromPlayerData()
    {
        if (playerData != null)
        {
            coins = playerData.coins;
           
        }
       
    }

}