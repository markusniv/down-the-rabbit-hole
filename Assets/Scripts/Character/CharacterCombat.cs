using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    /// <summary>
    /// Weapon currently held. Will be null if character doesn't hold weapon.
    /// </summary>
    // TODO: Add Weapon Type
    public object CurrentWeapon { get; set; }

    /// <summary>
    /// Private backing field for <see cref="CurrentState"/>. DO NOT SET DIRECTLY.
    /// </summary>
    private State _currentState;
    
    
    /// <summary>
    /// Current combat state.
    /// </summary>
    public State CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState?.OnStateExit();
            _currentState = value;
            _currentState?.OnStateEnter();
            OnStateChange?.Invoke(_currentState);
        }
    }


    #region Events
    /// <summary>
    /// Called when state changes. State parameter contains new current state.
    /// </summary>
    public event Action<State> OnStateChange;
    #endregion

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
    protected virtual void Awake()
    {
        Character = GetComponent<Character>();
        Hand = Character.gameObject.transform.Find("Hand");
    }

    protected virtual void Start()
    {
        CurrentState = new Idle(Character);
    }

    protected virtual void FixedUpdate()
    {
        CurrentState?.OnFixedUpdate();
    }

    protected virtual void Update()
    {
        CurrentState?.OnUpdate();
    }
}
