using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Damageable : MonoBehaviour

{
     [SerializeField]
    private Slider hpBar; //체력바 슬라이더 변수 



    public UnityEvent<int, Vector2> damageableHit;

    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            // If health drops below 0, character is no longer alive
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invinsibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationsStrings.isAlive,value);
            Debug.Log("IsAlive set " + value);
        }
    }

    // The velocity should not be changed while this is true but needs to be respected by other physics components like
    // the player controller
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationsStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationsStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //hpBar.value = (int) Health / (int) MaxHealth; 
        //슬라이더는 기본적으로 최소값 0, 최대값 1이기에 fill에 현채 채력 값 넣으려면  현재체력 / 최대 체력을 해줘야함 
    
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invinsibilityTime)
            {
                // Remove invinsibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    // Returns whether the damageable took damage or not
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            //HandleHp();
            isInvincible = true;

            // Notify other subscribed components that the damageable was hit to handle the knockback and such
            animator.SetTrigger(AnimationsStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
          

            return true;

            
        }
        // Unable to be hit
        return false;
    }

    public void HandleHp()  // 호출 될 때 마다 hpBar의 value 값을 초기화
    {
       // hpBar.value = (int) Health / (int) MaxHealth; 
       

    }


    // Returns whether the character was healed or not
    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }
}
