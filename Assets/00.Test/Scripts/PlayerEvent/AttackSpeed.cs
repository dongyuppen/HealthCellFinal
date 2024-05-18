using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : MonoBehaviour
{
    public Animator animator;
    
    public float atkSpeed = 1;

    public SOPlayer playerData;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        InitializeAtkSpeedFromPlayerData();
    }

    private void Start()
    {
        SetAttackSpeed(atkSpeed);
    }

    private void Update()
    {
        // attackSpeed animation Update
        if (playerData != null && playerData.attackSpeed != atkSpeed)
        {
            SetAttackSpeed(atkSpeed);
        }
    }

    public void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        atkSpeed = speed;
    }

    private void InitializeAtkSpeedFromPlayerData()
    {
        if (playerData != null)
        {
            atkSpeed = playerData.attackSpeed;
        }
        else
        {
            Debug.LogWarning("Player data is not assigned to AttackSpeed!");
        }
    }
}
