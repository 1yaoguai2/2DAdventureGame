using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class Character : MonoBehaviour
{
    [Header("基本属性")] public float maxHealth;
    [SerializeField] private float currentHealth;

    [Header("受伤无敌")] public float invulnerableDuration;

    public float invulnerableCounter;

    public bool isInvulnerable;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (Math.Abs(currentHealth - value) > 0)
            {
                if (value < 0)
                {
                    currentHealth = 0;
                }
                else
                {
                    currentHealth = value;
                }
            }
        }
    }

    public UnityEvent<Transform> OnTakeDamageEvent;
    
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
        TriggerInvulnerable();
        OnTakeDamageEvent?.Invoke(attack.transform);
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
}