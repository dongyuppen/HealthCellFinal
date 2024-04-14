using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    // 비행 속도
    public float flightSpeed = 2f;
    // 웨이포인트에 도달한 것으로 간주하는 거리
    public float waypointReachedDistance = 0.1f;
    // 물기 공격 감지 존
    public DetectionZone biteDetectionZone;
    // 사망 콜라이더
    public Collider2D deathCollider;
    // 웨이포인트 목록
    public List<Transform> waypoints;

    Animator animator;
    Rigidbody2D rb;
    EnemyDamageable damageable;

    Transform nextWaypoint;
    int waypointNum = 0;

    public bool _hasTarget = false;

    // Property to check if the FlyingEnemy has a target
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            // Set animator parameter to indicate whether the Enemy has a target
            animator.SetBool(AnimationsStrings.hasTarget, value);
        }
    }

    // Property to check if the FlyingEnemy can move
    // FlyingEnemy가 움직일 수 있는지 확인하는 속성
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationsStrings.canMove);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<EnemyDamageable>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    private void OnEnable()
    {
        damageable.damageableDeath.AddListener(OnDeath);
    }

    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void Flight()
    {
        // Fly to next waypoint
        // 다음 웨이포인트로 날아가기
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        // Check if we have reached the waypoint already
        // 이미 웨이포인트에 도달했는지 확인
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        // See if we need to switch waypoints
        // 웨이포인트를 전환해야 하는지 확인
        if (distance <= waypointReachedDistance)
        {
            // Switch to next waypoint
            // 다음 웨이포인트로 전환
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                // Loop back to original waypoint
                // 원래의 웨이포인트로 루프
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            // Facing the right
            if (rb.velocity.x < 0)
            {
                // Flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            // Facing the left
            if (rb.velocity.x > 0)
            {
                // Flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }

    public void OnDeath()
    {
        // Dead Flyier falls to the Ground
        // 죽은 비행체가 땅으로 떨어짐
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}
