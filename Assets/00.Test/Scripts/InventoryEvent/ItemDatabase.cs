using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    // Static reference to the ItemDatabase instance, ensuring there's only one instance in the game
    public static ItemDatabase instance;

    public int money = 0;

    private void Awake()
    {
        instance = this; // Assigning the current instance to the static reference
    }

    private void Start()
    {
        money = 10000;
    }

    // List to hold all items in the game
    public List<Item> itemDB = new List<Item>();

    // Prefab for the field items in the game world
    public GameObject fieldItemPrefab;

    // Array to hold positions for spawning field items
    public Vector3[] pos;
}
