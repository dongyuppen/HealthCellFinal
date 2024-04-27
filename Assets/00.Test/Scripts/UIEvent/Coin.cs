using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;

    private CoinManager coinManager;


    // Rotation speed of the pickup object
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    // Start is called before the first frame update
    void Start()
    {
        coinManager = CoinManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the pickup object based on spinRotationSpeed
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            //give score to player
            coinManager.ChangeCoins(value);
            Destroy(gameObject);
        }
    }
}
