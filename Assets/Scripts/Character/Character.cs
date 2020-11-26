using System;
using UnityEngine;

/// <summary>
/// Abstract character. This defines common character features.
/// </summary>
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(CharacterCombat))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    public int MaxHealth = 1000;

    /// <summary>
    /// Private backing field for <see cref="CurrentHealth"/>. DO NOT SET THIS DIRECTLY.
    /// </summary>
    [SerializeField]
    private int _currentHealth;

    /// <summary>
    /// Current health for character. If this is set below 0, it will call <see cref="Die"/>. Maximum value for this is <see cref="MaxHealth"/>. Values are clamped automatically.
    /// </summary>
    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }set
        {
            _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            if (_currentHealth == 0) Die();
        }
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
    #endregion


    /// <summary>
    /// This triggers the "dying" process
    /// </summary>
    public virtual void Die()
    {
        // Set health to zero just incase this was called without setting CurrentHealth
        _currentHealth = 0;
        OnDeath?.Invoke();
    }

    protected virtual void Awake()
    {
        Inventory = GetComponent<Inventory>();
        Combat = GetComponent<CharacterCombat>();
        Movement = GetComponent<CharacterMovement>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {

    }
}