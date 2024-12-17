using System;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")] public float maxHealth;

    [Header("受伤无敌")] public float invulnerableDuration;

    public float invulnerableCounter;

    private float _currentHealth = 0;
    public bool isInvulnerable;

    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (Math.Abs(_currentHealth - value) > 0)
            {
                if (value < 0)
                {
                    _currentHealth = 0;
                    OnPlayerDeathEvent?.Invoke();
                }
                else
                {
                    _currentHealth = value;
                }

                OnHealthChanged?.Invoke(this);
            }
        }
    }

    public UnityEvent<Transform> OnTakeDamageEvent;
    public UnityEvent OnPlayerDeathEvent;
    public UnityEvent<Character> OnHealthChanged;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if (isInvulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter < 0)
            {
                isInvulnerable = false;
            }
        }
    }

    /// <summary>
    /// 接受伤害
    /// </summary>
    /// <param name="attack"></param>
    public void TakeDamage(Attack attack)
    {
        if (isInvulnerable) return;
        //LogManager.Log(attack.damage);
        CurrentHealth -= attack.damage;
        if (CurrentHealth != 0)
        {
            TriggerInvulnerable();
            OnTakeDamageEvent?.Invoke(attack.transform);
        }
    }

    /// <summary>
    /// 触发无敌状态
    /// </summary>
    public void TriggerInvulnerable()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            CurrentHealth = 0;
            OnHealthChanged?.Invoke(this);
            OnPlayerDeathEvent?.Invoke();
        }
    }
}