using System;
using UnityEngine;

/// <summary>
/// Abstract base class for character movement
/// </summary>
[RequireComponent(typeof(Animator))]
public abstract class CharacterMovement : MonoBehaviour, IStateMachine
{
    /// <summary>
    /// Characters Base movement. This is always the same.
    /// </summary>
    protected float BaseMovementSpeed { get; } = 5f;

    /// <summary>
    /// Characters movement speed modifier. This is multiplier for that base movement. Change this to change characters speed.
    /// </summary>
    public float MovementSpeedModifier = 1f;

    /// <summary>
    /// Characters actual movement speed.
    /// </summary>
    public float MovementSpeed => BaseMovementSpeed * MovementSpeedModifier;

    /// <summary>
    /// Movement vector. Indicates direction where character is moving.
    /// </summary>
    public Vector2 Movement;

    /// <summary>
    /// Reference to the room where this character is currently
    /// </summary>
    public Room CurrentRoom;

    private bool _immobile;

    /// <summary>
    /// Character cannot move if this is set to true. Setting this automatically sets Animator paramater.
    /// </summary>
    public bool Immobile { get
        {
            return _immobile;
        }
        set
        {
            Animator.SetBool("Immobile", value);
            _immobile = value;
        }
    }

    /// <summary>
    /// Characters previous state.
    /// </summary>
    public State PreviousState { get; private set; }

    /// <summary>
    /// Private backing field for <see cref="CurrentState"/>. DO NOT SET DIRECTLY.
    /// </summary>
    private State _currentState;

    /// <summary>
    /// Characters current state.
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
            PreviousState = _currentState;
            _currentState = value;
            OnStateChange?.Invoke();
            _currentState?.OnStateEnter();
        }
    }

    #region Components

    public Rigidbody2D Rigidbody { get; private set; }
    protected CircleCollider2D Collider { get; private set; }
    public Character Character { get; private set; }

    public Animator Animator { get; private set; }

    #endregion Components

    #region Events

    /// <summary>
    /// Triggered when this character changes states.
    /// </summary>
    public event Action OnStateChange;

    public event Action<Room> OnRoomEnter;

    public event Action<Room> OnRoomLeave;

    #endregion Events

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CircleCollider2D>();
        Character = GetComponent<Character>();
        Animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        CurrentState?.OnUpdate();
    }

    protected virtual void FixedUpdate()
    {
        CurrentState?.OnFixedUpdate();
        if (Immobile) return;
        Animator.SetFloat("MovementInX", Movement.x);
        Animator.SetFloat("MovementInY", Movement.y);

        Rigidbody.MovePosition(Rigidbody.position + Movement * (BaseMovementSpeed * MovementSpeedModifier) * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Checks if Character enters room
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Room room))
        {
            CurrentRoom = room;
            OnRoomEnter?.Invoke(room);
        }
    }

    /// <summary>
    /// Checks if Character leaves room
    /// </summary>
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Room room))
        {
            CurrentRoom = null;
            OnRoomLeave?.Invoke(room);
        }
    }
}