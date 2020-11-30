/// <summary>
/// Abstract base class for states.
/// </summary>
public abstract class State
{
    /// <summary>
    /// Reference to the character who is currently in this state.
    /// </summary>
    protected Character Character;

    public State(Character character)
    {
        Character = character;
    }

    /// <summary>
    /// Called on every Update when character is in this state.
    /// </summary>
    public virtual void OnUpdate() { }


    /// <summary>
    /// Called on every FixedUpdate when character is in this state.
    /// </summary>
    public virtual void OnFixedUpdate() { }

    /// <summary>
    /// Called when character enters this state.
    /// </summary>
    public virtual void OnStateEnter() { }

    /// <summary>
    /// Called when character leaves this state.
    /// </summary>
    public virtual void OnStateExit() { }

}