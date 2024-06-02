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
        //instance = this; // Assigning the current instance to the static reference
         if (instance == null)
        {
            instance = this; // Assigning the current instance to the static reference
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 이 오브젝트가 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 중복 생성 방지
        }
    }

    private void Start()
    {
        money = 10000;
        CoinManager.instance.coins = money;
        CoinManager.instance.UpdateCoinsDisplay();
    }

    // List to hold all items in the game
    public List<Item> itemDB = new List<Item>();

    // Prefab for the field items in the game world
    public GameObject fieldItemPrefab;

    // Array to hold positions for spawning field items
    public Vector3[] pos;
}
