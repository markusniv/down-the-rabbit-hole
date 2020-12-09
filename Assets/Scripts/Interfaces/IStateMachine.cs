using System;

/// <summary>
/// Interface for state machines.
/// </summary>
public interface IStateMachine
{
    /// <summary>
    /// Previous State
    /// </summary>
    State PreviousState { get; }
    /// <summary>
    /// Current State
    /// </summary>
    State CurrentState { get; set; }

    /// <summary>
    /// Is Called when <see cref="CurrentState"/> Changes
    /// </summary>
    event Action<State> OnStateChange;
}
