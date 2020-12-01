using System;
using UnityEngine;
using Weapons;

/// <summary>
/// Abstract base class for character combat
/// </summary>
public abstract class CharacterCombat : MonoBehaviour, IStateMachine
{
    /// <summary>
    /// Weapon currently held. Will be null if character doesn't hold weapon.
    /// </summary>
    public Weapon CurrentWeapon { get; set; }


    private float? _attackCooldown;
    /// <summary>
    /// Current attack cooldown in seconds. Invokes cooldown start when setting this value.
    /// </summary>
    public float? AttackCooldown
    {
        get
        {
            return _attackCooldown;
        }
        set
        {
            OnCooldownStart?.Invoke();
            _attackCooldown = value;
        }
    }

    /// <summary>
    /// Previous state
    /// </summary>
    public State PreviousState { get; private set; }

    /// <summary>
    /// Private backing field for <see cref="CurrentState"/>. DO NOT SET DIRECTLY.
    /// </summary>
    private State _currentState;
    public State CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState?.OnStateExit();
            PreviousState = _currentState;
            _currentState = value;
            OnStateChange?.Invoke(_currentState);
            _currentState?.OnStateEnter();
        }
    }

    #region Components
    /// <summary>
    /// Reference to the main Character script
    /// </summary>
    public Character Character;

    /// <summary>
    /// Reference to the hand that should be inside the character
    /// </summary>
    public Transform Hand;
    #endregion

    #region Events
    /// <summary>
    /// Triggered when character changes states.
    /// </summary>
    public event Action<State> OnStateChange;
    /// <summary>
    /// Triggered when an attack ends
    /// </summary>
    public event Action<Weapon> OnAttackEnd;
    /// <summary>
    /// Triggered when an attack starts
    /// </summary>
    public event Action<Weapon> OnAttackStart;
    /// <summary>
    /// Triggered at the end of an attack to initiate cooldown
    /// </summary>
    public event Action OnCooldownStart;
    /// <summary>
    /// Triggered when a cooldown is finished
    /// </summary>
    public event Action OnCooldownEnd;
    #endregion

    #region InvokingEvents
    /// <summary>
    /// Invoke an attack with the current equipped weapon
    /// </summary>
    /// <param name="weapon">Current weapon</param>
    public void InvokeAttackStart(Weapon weapon) => OnAttackStart?.Invoke(weapon);
    /// <summary>
    /// Invoke the end of an attack with the current equipped weapon
    /// </summary>
    /// <param name="weapon">Current weapon</param>
    public void InvokeAttackEnd(Weapon weapon) => OnAttackEnd?.Invoke(weapon);
    /// <summary>
    /// Invoke cooldown start at the end of an attack for the current weapon
    /// </summary>
    public void InvokeCooldownStart() => OnCooldownStart?.Invoke();
    /// <summary>
    /// Invoke the end of cooldown for the current weapon when cooldown timer finishes
    /// </summary>
    public void InvokeCooldownEnd() => OnCooldownEnd?.Invoke();
    #endregion

    /// <summary>
    /// Get the character component of this character, set its state to Idle and get its hand gameObject
    /// </summary>
    protected virtual void Awake()
    {
        Character = GetComponent<Character>();
        CurrentState = new Idle(Character);
        Hand = Character.gameObject.transform.Find("Hand");
    }

    protected virtual void Start()
    {
    }
    /// <summary>
    /// Handle counting cooldown down if cooldown is active
    /// </summary>
    protected virtual void FixedUpdate()
    {
        CurrentState?.OnFixedUpdate();
        ReduceCooldown();
    }

    /// <summary>
    /// Reduces cooldown until its 0.
    /// </summary>
    protected virtual void ReduceCooldown()
    {
        if (AttackCooldown > 0f)
        {
            _attackCooldown -= Time.fixedDeltaTime;
        }else if(AttackCooldown <= 0f && AttackCooldown != null)
        {
            OnCooldownEnd?.Invoke();
            _attackCooldown = null;
        }
    }
    /// <summary>
    /// Check for state changes
    /// </summary>
    protected virtual void Update()
    {
        CurrentState?.OnUpdate();
    }
}
