using System;
using UnityEngine;

/// <summary>
/// Abstract base class for character movement
/// </summary>
public abstract class CharacterMovement : MonoBehaviour
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
    /// Movement vector. Indicates direction where character is moving.
    /// </summary>
    public Vector2 Movement;

    /// <summary>
    /// Reference to the room where this character is currently
    /// </summary>
    public Room CurrentRoom;

    /// <summary>
    /// Private backing field for <see cref="CurrentState"/>. DO NOT SET DIRECTLY
    /// </summary>
    private State _currentState;

    /// <summary>
    /// Current movement state
    /// </summary>
    public State CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState?.OnStateEnter();
            _currentState = value;
            _currentState?.OnStateEnter();
            OnStateChange?.Invoke(_currentState);
        }
    }

    #region Components

    public Rigidbody2D Rigidbody { get; private set; }
    protected CircleCollider2D Collider { get; private set; }
    public Character Character { get; private set; }

    #endregion Components

    #region Events

    public event Action<State> OnStateChange;

    public event Action<Room> OnRoomEnter;

    public event Action<Room> OnRoomLeave;

    #endregion Events

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CircleCollider2D>();
        Character = GetComponent<Character>();
    }

    protected virtual void Update()
    {
        CurrentState?.OnUpdate();
    }

    protected virtual void FixedUpdate()
    {
        CurrentState.OnFixedUpdate();
        // TODO: Check if Character is dodging

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