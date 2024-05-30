using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Damageable : MonoBehaviour

{
    public int level;

    // Reference to the ScriptableObject containing player's data
    public SOPlayer playerData;

    public AttackSpeed attackSpeed;


    // Event for when the damageble object is hit, includes damage amount and knockback direction
    public UnityEvent<int, Vector2> damageableHit;

    public UnityEvent<int, int> healthChanged;

    Animator animator;

    
    //private Color originalColor;
    private SpriteRenderer spriteRenderer;

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
                UpdatePlayerDataAfterDead();
                IsAlive = false;
                 StartCoroutine(Die(2));
                 
            }
        }
    }
    IEnumerator Die(float time)
    {
        AudioManager.instance.PlaySfx(AudioManager.sfx.fail);
        yield return new WaitForSeconds(time); 
        AudioManager.instance.PlayBgm(false);
        SceneManager.LoadScene("DieScene");


    }
 


    
    // Flag indicating whether the damageable entity is alive or not
    [SerializeField]
    private bool _isAlive = true;

    // Flag indicating invincibility of the damageable entity
    //[SerializeField]
    public bool isInvincible = false;

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
        InitializeLevelFromPlayerData();
        InitializeHealthFromPlayerData();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //originalColor = spriteRenderer.color;
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

    private void InitializeLevelFromPlayerData()
    {
        if (playerData != null)
        {
            level = playerData.level;
        }
        else
        {
            Debug.LogWarning("Player data is not assigned to Damageable!");
        }
    }

    private void InitializeHealthFromPlayerData()
    {
        if (playerData != null)
        {
            _maxHealth = playerData.maxHealth;
            _health = _maxHealth;
        }
        else
        {
            Debug.LogWarning("Player data is not assigned to Damageable!");
        }
    }

    

    private void UpdatePlayerDataAfterDead()
    {
        if (playerData != null)
        {
            // Update player's level in player data after Dead
            playerData.level = level;

            // Update player's maxHealth in player data after Dead
            playerData.maxHealth = _maxHealth;

            // Update player's attack damage in player data after Dead
            playerData.attackDamage1 = GetComponentInChildren<Attack>().attackDamage;

            // Update player's attack speed in player data after Dead
            playerData.attackSpeed = GetComponent<AttackSpeed>().atkSpeed;
        }
    }

    //Original Code: make player opacity
    /* 
    private void MakePlayerTransparent(float duration, float alphaValue)
    {
        Color tempColor = spriteRenderer.color;
        tempColor.a = alphaValue;
        spriteRenderer.color = tempColor;

     
        Invoke("ResetPlayerColor", duration);
    }

  
    private void ResetPlayerColor()
    {
        spriteRenderer.color = originalColor;
    }
    */

    private IEnumerator FlashPlayer(float duration, float interval)
    {
        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < duration)
        {
            spriteRenderer.enabled = isVisible;
            yield return new WaitForSeconds(interval);
            isVisible = !isVisible;
            elapsedTime += interval;
        }

        spriteRenderer.enabled = true; // Ensure sprite is visible after flashing
    }

    // Returns whether the damageable took damage or not
    public bool Hit(int damage, Vector2 knockback) // Function to handle when the damageable entity is hit
    {
        if (IsAlive && !isInvincible)
        {
            StartCoroutine(FlashPlayer(invincibilityTime, 0.1f)); // Flash effect
            
            //MakePlayerTransparent(0.5f, 0.5f); //Original Code: make player opacity

            // Reduce health by damage amount
            Health -= damage;
            AudioManager.instance.PlaySfx(AudioManager.sfx.pHit);
            
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

    public void IncreaseMaxHealth(int amount)
    {
        _maxHealth += amount;
    }

    public void IncreasePlayerAttackDamage(int increaseAmount)
    {
        Attack playerAttack = GetComponentInChildren<Attack>();
        if (playerAttack != null)
        {
            playerAttack.IncreaseAttackDamage(increaseAmount);
        }
        else
        {
            Debug.LogWarning("Attack script not found in children of player object!");
        }
    }
}
