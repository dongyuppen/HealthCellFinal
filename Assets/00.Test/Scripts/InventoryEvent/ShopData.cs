using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopData : MonoBehaviour
{
    public List<Item> stocks = new List<Item>();
    public bool[] soldOuts;

    // Start is called before the first frame update
    void Start()
    {
        stocks.Add(ItemDatabase.instance.itemDB[0]);
        stocks.Add(ItemDatabase.instance.itemDB[1]);
        stocks.Add(ItemDatabase.instance.itemDB[2]);
        stocks.Add(ItemDatabase.instance.itemDB[3]);
        stocks.Add(ItemDatabase.instance.itemDB[4]);
        stocks.Add(ItemDatabase.instance.itemDB[5]);
        // If you want to add an item, uncomment it below.
        //stocks.Add(ItemDatabase.instance.itemDB[6]);
        //stocks.Add(ItemDatabase.instance.itemDB[7]);
        //stocks.Add(ItemDatabase.instance.itemDB[8]);
        //stocks.Add(ItemDatabase.instance.itemDB[9]);
        soldOuts = new bool[stocks.Count];
        for (int i = 0; i < soldOuts.Length; i++)
        {
            soldOuts[i] = false;
        }
    }
}
