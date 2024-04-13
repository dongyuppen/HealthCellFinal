using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    // ���� �ӵ�
    public float flightSpeed = 2f;
    // ��������Ʈ�� ������ ������ �����ϴ� �Ÿ�
    public float waypointReachedDistance = 0.1f;
    // ���� ���� ���� ��
    public DetectionZone biteDetectionZone;
    // ��� �ݶ��̴�
    public Collider2D deathCollider;
    // ��������Ʈ ���
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
    // FlyingEnemy�� ������ �� �ִ��� Ȯ���ϴ� �Ӽ�
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
        // ���� ��������Ʈ�� ���ư���
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        // Check if we have reached the waypoint already
        // �̹� ��������Ʈ�� �����ߴ��� Ȯ��
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        // See if we need to switch waypoints
        // ��������Ʈ�� ��ȯ�ؾ� �ϴ��� Ȯ��
        if (distance <= waypointReachedDistance)
        {
            // Switch to next waypoint
            // ���� ��������Ʈ�� ��ȯ
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                // Loop back to original waypoint
                // ������ ��������Ʈ�� ����
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
        // ���� ����ü�� ������ ������
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}
