using System;

/// <summary>
/// Interface for state machines.
/// </summary>
public interface IStateMachine
{
    State PreviousState { get; }
    State CurrentState { get; set; }

    event Action<State> OnStateChange;
}
