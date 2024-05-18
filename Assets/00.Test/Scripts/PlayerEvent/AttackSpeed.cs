using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : MonoBehaviour
{
    Animator animator;
    
    public float atkSeed = 1;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAttackSpeed(atkSeed);
    }

    public void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        atkSeed = speed;
    }
}
