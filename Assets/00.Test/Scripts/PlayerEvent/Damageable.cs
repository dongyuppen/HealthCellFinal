using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Damageable : MonoBehaviour

{
    // Event for when the damageble object is hit, includes damage amount and knockback direction
    public UnityEvent<int, Vector2> damageableHit;

    public UnityEvent<int, int> healthChanged;

    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    // Maximum health property
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

    // Current health property
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            // If health drops below 0, character is no longer alive
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    // Flag indicating whether the damageable entity is alive or not
    [SerializeField]
    private bool _isAlive = true;

    // Flag indicating invincibility of the damageable entity
    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 1f;
    // Duration of invicibility after being hit


    // Alive status property
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
    public bool LockVelocity // Velocity locking property
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
    }

    private void Update()
    {
        // Update invincibility state(timer)
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                // Remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    // Returns whether the damageable took damage or not
    public bool Hit(int damage, Vector2 knockback) // Function to handle when the damageable entity is hit
    {
        if (IsAlive && !isInvincible)
        {
            // Reduce health by damage amount
            Health -= damage;
            
            isInvincible = true; // Set invincibility

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

    // Returns whether the character was healed or not
    public bool Heal(int healthRestore) // Function to handle healing of the damageable entity
    {
        if (IsAlive && Health < MaxHealth)
        {
            // Calculate actual amount of healing
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);

            // Increase health by actual healing amount
            Health += actualHeal;

            // Invoke healing event
            CharacterEvents.characterHealed(gameObject, actualHeal);

            return true;
        }

        return false;
    }
}
