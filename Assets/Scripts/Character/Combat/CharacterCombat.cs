using System;
using UnityEngine;

/// <summary>
/// Abstract base class for character combat
/// </summary>
public abstract class CharacterCombat : MonoBehaviour, IStateMachine
{
    /// <summary>
    /// Weapon currently held. Will be null if character doesn't hold weapon.
    /// </summary>
    // TODO: Add Weapon Type
    public object CurrentWeapon { get; set; }

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

    public Animator Animator;
    #endregion

    #region Events
    /// <summary>
    /// Triggered when character changes states.
    /// </summary>
    public event Action OnStateChange;
    #endregion 
    protected virtual void Awake()
    {
        Character = GetComponent<Character>();
        Animator = GetComponent<Animator>();
        Hand = Character.gameObject.transform.Find("Hand");
    }

    protected virtual void Start()
    {
    }

    protected virtual void FixedUpdate()
    {
        CurrentState?.OnFixedUpdate();
        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.fixedDeltaTime;
        }
    }

    protected virtual void Update()
    {
        CurrentState?.OnUpdate();
    }
}
