using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EnemyDamageable : MonoBehaviour

{   public Image fadePanel;
    float time = 0f;
    float Fadetime = 1f;
    
    public bool isBoss = false;
    public int level;

    

    // Event for when the damageble object is hit, includes damage amount and knockback direction
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;

    public UnityEvent<int, int> healthChanged;

    Animator animator;

    // Reference to the ScriptableObject containing monster's data
    public SOMonster monsterData;


    public GameObject itemPrefab;  // pickup item Prefab
    public GameObject coinPrefab; // Coin Prefab

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
                IsAlive = false;
                AudioManager.instance.PlaySfx(AudioManager.sfx.MonsterDie);
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
    public float invincibilityTime = 0.25f;
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
            animator.SetBool(AnimationsStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);

            if (value == false)
            {
                damageableDeath.Invoke();
                DropItem();
                DropCoin();
                
            }
        }
    }

    private void DropItem()
    {
        if (itemPrefab != null)
        {
            float dropChance = UnityEngine.Random.value; 
            if (dropChance < 0.5f) // 50% Drop Chance
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private void DropCoin()
    {
        if (coinPrefab != null)
        {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);   
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
        InitializeLevelFromMonsterData();
        InitializeHealthFromMonsterData();
    }
    

     private BossController bossController;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossController = GetComponent<BossController>();
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

    if(isBoss == true && Health <= 0)
    {      
        StartCoroutine(FadeOutIn());
    }

    }
    IEnumerator FadeOutIn()
    {   
        bossController.OnBossDefeated();
        yield return new WaitForSeconds(2f);
        fadePanel.gameObject.SetActive(true);
        Color alpha = fadePanel.color;
        while(alpha.a < 1f)
        {
            time += Time.deltaTime / Fadetime;
            alpha.a = Mathf.Lerp(0, 1, time);
            fadePanel.color = alpha;
            yield return null;
        }
        time = 0f;
        Debug.Log("on");
        Invoke("GoEnding", 1.5f);
        yield return null;
    }


    public void GoEnding () 
    {
        
        if(isBoss == true && Health <= 0)
    {
         SceneManager.LoadScene("LoadingScene"); 
    }

    }


    private void InitializeLevelFromMonsterData()
    {
        if (monsterData != null)
        {
            level = monsterData.level;
        }
        else
        {
            Debug.LogWarning("Monster data is not assigned to EnemyAttack!");
        }
    }

    private void InitializeHealthFromMonsterData()
    {
        if (monsterData != null)
        {
            _maxHealth = monsterData.maxHealth;
            _health = _maxHealth;
        }
        else
        {
            Debug.LogWarning("Monster data is not assigned to EnemyDamageable!");
        }
    }

    private IEnumerator FlashEnemy(float duration, float interval)
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
            StartCoroutine(FlashEnemy(invincibilityTime, 0.1f)); // Flash effect

            // Reduce health by damage amount
            Health -= damage;

            AudioManager.instance.PlaySfx(AudioManager.sfx.mHitI);
            
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
