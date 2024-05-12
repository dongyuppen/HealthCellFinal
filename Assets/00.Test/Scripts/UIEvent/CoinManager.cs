using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int coins;
    [SerializeField] private TMP_Text coinsDisplay;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
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
