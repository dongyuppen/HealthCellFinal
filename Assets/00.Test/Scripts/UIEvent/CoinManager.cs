using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int coins;
    [SerializeField] private TMP_Text coinsDisplay;

    private void Awake()
    {
        /*if (!instance)
        {
            instance = this;
        }*/
    if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        coins = ItemDatabase.instance.money;
    }

    public void UpdateCoinsDisplay()
    {
        coinsDisplay.text = coins.ToString();
    }

    
    public void ChangeCoins(int amount)
    {
        coins += amount;
        UpdateCoinsDisplay();
        ItemDatabase.instance.money = coins;
        
    }
}
