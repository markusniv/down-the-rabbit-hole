using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Abstract character. This defines common character features.
/// </summary>
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    public float MaxHealth = 1000;

    /// <summary>
    /// Private backing field for <see cref="CurrentHealth"/>. DO NOT SET THIS DIRECTLY.
    /// </summary>
    [SerializeField]
    private float _currentHealth;

    /// <summary>
    /// Particles that will be shown when <see cref="Character"/> dies
    /// </summary>
    public GameObject DeathParticles;

    /// <summary>
    /// Boolean to check if the character is dead
    /// </summary>
    public bool Dead;

    /// <summary>
    /// Current health for character. If this is set below 0, it will call <see cref="Die"/>. Maximum value for this is <see cref="MaxHealth"/>. Values are clamped automatically.
    /// </summary>
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
            if (_currentHealth == 0f && !Dead) Die();
        }
    }
    /// <summary>
    /// To be able to modify damage, when character has damage increasing relic. Values are clamped automatically.
    /// </summary>
    public float DamageModifier = 1f;

    /// <summary>
    /// Flat modifier. Increases characters all damage by flat ammount.
    /// </summary>
    public float FlatDamageModifier = 0f;

    /// <summary>
    /// Gets Characters actual damage. <see cref="DamageModifier"/> and <see cref="FlatDamageModifier"/> Has been applied to this
    /// </summary>
    public Func<float, float> Damage => (BaseDamage) => (BaseDamage * DamageModifier) * FlatDamageModifier;


    private List<StatusEffect> _statusEffects = new List<StatusEffect>();
    /// <summary>
    /// Status effects that character currently has
    /// </summary>
    public IReadOnlyList<StatusEffect> StatusEffects => _statusEffects;

    /// <summary>
    /// Adds status effect to the character
    /// </summary>
    /// <param name="effect">Effect to be added</param>
    public virtual void AddStatusEffect(StatusEffect effect)
    {
        effect.OnStatusEnter();
        _statusEffects.Add(effect);
    }

    /// <summary>
    /// Removes status effect from character
    /// </summary>
    /// <param name="effect">Effect to remove</param>
    public virtual void RemoveStatusEffect(StatusEffect effect)
    {
        effect.OnStatusExit();
        _statusEffects.Remove(effect);
    }

    /// <summary>
    /// Removes all status effects safely
    /// </summary>
    public virtual void ClearAllStatusEffects()
    {
        _statusEffects.ForEach(x => x.OnStatusExit());
        _statusEffects.Clear();
    }

    #region Events
    /// <summary>
    /// Is Triggered when <see cref="Die"/> is called. This event can be used to track character death.
    /// </summary>
    public event Action OnDeath;
    #endregion

    #region Components
    public Inventory Inventory { get; private set; }
    public CharacterCombat Combat { get; private set; }
    public CharacterMovement Movement { get; private set; }

    public Animator Animator { get; private set; }
    #endregion


    /// <summary>
    /// This triggers the "dying" process
    /// </summary>
    public virtual void Die()
    {
        Dead = true;
        // Set health to zero just incase this was called without setting CurrentHealth
        _currentHealth = 0;
        ClearAllStatusEffects();
        OnDeath?.Invoke();
        Instantiate(DeathParticles, transform.position, Quaternion.identity);
        Combat.CurrentState = new Dead(this);
    }

    protected virtual void Awake()
    {
        Inventory = GetComponent<Inventory>();
        Combat = GetComponent<CharacterCombat>();
        Movement = GetComponent<CharacterMovement>();
        Animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    /// <summary>
    /// Runs <see cref="StatusEffect.OnUpdate"/>
    /// </summary>
    protected virtual void Update()
    {
        foreach (var effect in _statusEffects.ToList())
        {
            effect.OnUpdate();
        }
    }

    /// <summary>
    /// Runs <see cref="StatusEffect.OnFixedUpdate"/>
    /// </summary>
    protected virtual void FixedUpdate()
    {
        foreach (var effect in _statusEffects.ToList())
        {
            effect.OnFixedUpdate();
        }
    }





}