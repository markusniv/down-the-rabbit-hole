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
    // TODO: Add Weapon Type
    public Weapon CurrentWeapon { get; set; }

    /// <summary>
    /// Current attack cooldown in seconds.
    /// </summary>
    public float AttackCooldown { get; set; }

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
            OnStateChange?.Invoke();
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
    public event Action OnStateChange;
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
    public event Action<Weapon> OnCooldownStart;
    /// <summary>
    /// Triggered when a cooldown is finished
    /// </summary>
    public event Action<Weapon> OnCooldownEnd;
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
    /// <param name="weapon">Current weapon</param>
    public void InvokeCooldownStart(Weapon weapon) => OnCooldownStart?.Invoke(weapon);
    /// <summary>
    /// Invoke the end of cooldown for the current weapon when cooldown timer finishes
    /// </summary>
    /// <param name="weapon"></param>
    public void InvokeCooldownEnd(Weapon weapon) => OnCooldownEnd?.Invoke(weapon);
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
        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.fixedDeltaTime;
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
