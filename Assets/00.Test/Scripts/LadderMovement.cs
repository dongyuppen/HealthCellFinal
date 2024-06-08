using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    public float climbspeed = 8f;
    private bool isLadder;
    private bool isClimbing;

    private Tilemap ladderTilemap;
    private Vector3Int ladderCellPosition;

    Animator animator;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D cd;
    TouchingDirections touchingDirections;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();


    }
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            animator.SetBool("isLadder", true);
            cd.isTrigger = true;
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbspeed);

            AlignToTileCenter();
        }
        else if(touchingDirections.IsGrounded)
        {
            rb.gravityScale = 2f;
            cd.isTrigger = false;
        }
        else
        {
            rb.gravityScale = 2f;
            cd.isTrigger = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            TilemapCollider2D tilemapCollider = collision.GetComponent<TilemapCollider2D>();
            if (tilemapCollider != null)
            {
                ladderTilemap = tilemapCollider.GetComponent<Tilemap>();
                Vector3 worldPosition = transform.position;
                ladderCellPosition = ladderTilemap.WorldToCell(worldPosition);
                isLadder = true;
            }      
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            animator.SetBool("isLadder", false);
            isLadder = false;
            isClimbing = false;
           
        }
    }

    void AlignToTileCenter()
    {
        if (ladderTilemap != null)
        {
            Vector3Int cellPosition = ladderTilemap.WorldToCell(transform.position);
            Vector3 tileCenterPosition = ladderTilemap.GetCellCenterWorld(cellPosition);
            transform.position = new Vector3(tileCenterPosition.x, transform.position.y, transform.position.z);
            
            Debug.Log($"Tile Center Position: {tileCenterPosition}, Character Position: {transform.position}");

        }
    }
}
