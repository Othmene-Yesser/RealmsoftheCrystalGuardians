using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;

    [SerializeField] GameObject uDumbPieceOf;
    [SerializeField]
    private int _maxHealth =100;

    Animator animator;

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
            _health= value;
            healthChanged.Invoke(_health,MaxHealth);
            //if no health no live :)
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);

            if (value == false)
            {
                damageableDeath.Invoke();
            }
        }
    }

    public bool IsHit 
    {
        get
        {
            return animator.GetBool(AnimationStrings.isHit);
        }
        private set
        {
            animator.SetBool(AnimationStrings.isHit, value);
        }
    }

    [SerializeField]
    private bool isInvincible = false;

    

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    
    private void Awake()
    {
        animator= uDumbPieceOf.GetComponent<Animator>();
    }
    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                //remove invincibility
                isInvincible = false;
                timeSinceHit = 0f;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    //returns whether the damageable tooke damage or nah 
    public bool Hit(int damage, Vector2 knockBack)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            //notify other subscribed components that the damageable was hit to handle the knockback UwU
            IsHit = true;
            damageableHit?.Invoke(damage, knockBack);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        //unable to be hit 
        return false;
    }

    public bool heal(int healthToRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthToRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }
}
