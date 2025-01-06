using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DataDefinition))]
public class Character : MonoBehaviour, ISaveable
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

    private DataDefinition _dataDefinition;

    public DataDefinition M_DataDefinition
    {
        get
        {
            if (_dataDefinition is null)
                _dataDefinition = GetComponent<DataDefinition>();
            return _dataDefinition;
        }
    }

    public UnityEvent<Transform> OnTakeDamageEvent;
    public UnityEvent OnPlayerDeathEvent;
    public UnityEvent<Character> OnHealthChanged;

    private void OnEnable()
    {
        CurrentHealth = maxHealth;
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
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

    public void GetSaveData(SaveData data)
    {
        if (data.characterPosDict.ContainsKey(M_DataDefinition.ID))
        {
            data.characterPosDict[M_DataDefinition.ID] = transform.position;
        }
        else
        {
            data.characterPosDict.Add(M_DataDefinition.ID, transform.position);
            data.floatDataDict.Add(M_DataDefinition.ID+"health",CurrentHealth);
        }
    }

    public void LoadData(SaveData data)
    {
        if (data.characterPosDict.TryGetValue(M_DataDefinition.ID, out var value))
        {
            transform.position = value;
            this.CurrentHealth = data.floatDataDict[M_DataDefinition.ID + "health"];
            
            //跟新UI
            OnHealthChanged?.Invoke(this);
        }
    }
}