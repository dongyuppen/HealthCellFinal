using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class MovingPlatForm : MonoBehaviour
{
    public Transform posA, posB;
    public float speed;
    Vector3 targetPos;

    PlayerController playerController;
    Rigidbody2D rb;
    Vector3 moveDirection;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //Start Point is B
        targetPos = posB.position;
        DirectionCalulate();
    }

    private void Update()
    {
        //If the player gets closer to point A, it changes to point B
        if (Vector2.Distance(transform.position, posA.position) < 0.05f) 
        {
            targetPos = posB.position;
            DirectionCalulate();
        }
        //If the player gets closer to point B, it changes to point A
        if (Vector2.Distance(transform.position, posB.position) < 0.05f)
        {
            targetPos = posA.position;
            DirectionCalulate();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    //Method that determines the direction of the platform
    void DirectionCalulate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player is on a platform, change "isOnPlatform" to "true"
        if (collision.CompareTag("Player"))
        {
            playerController.isOnPlatform = true;
            playerController.platformRb = rb;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If the player is on a platform, change "isOnPlatform" to "false"
        if (collision.CompareTag("Player"))
        {
            playerController.isOnPlatform = false;
        }
    }
}
