/// <summary>
/// Abstract state
/// </summary>
public abstract class State
{
    /// <summary>
    /// Reference to the Character who is in this state
    /// </summary>
    protected Character Character;

    /// <summary>
    /// Create State
    /// </summary>
    /// <param name="character">Who will enter this state</param>
    public State(Character character)
    {
        Character = character;
    }

    /// <summary>
    /// Called on every Update
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// Called on every FixedUpdate
    /// </summary>
    public virtual void OnFixedUpdate() { }

    /// <summary>
    /// Called when Character enters this state
    /// </summary>
    public virtual void OnStateEnter() { }

    /// <summary>
    /// Called when Character leaves this state
    /// </summary>
    public virtual void OnStateExit() { }
}