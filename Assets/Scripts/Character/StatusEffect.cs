
/// <summary>
/// Abstract definition of status effect.
/// </summary>
public abstract class StatusEffect
{
    /// <summary>
    /// Reference to the character who is currently in this status.
    /// </summary>
    protected Character Character;

    public StatusEffect(Character character)
    {
        Character = character;
    }

    /// <summary>
    /// Called on every Update when character is in this status.
    /// </summary>
    public virtual void OnUpdate() { }


    /// <summary>
    /// Called on every FixedUpdate when character is in this status.
    /// </summary>
    public virtual void OnFixedUpdate() { }

    /// <summary>
    /// Called when character enters this status.
    /// </summary>
    public virtual void OnStatusEnter() { }

    /// <summary>
    /// Called when character leaves this status.
    /// </summary>
    public virtual void OnStatusExit() { }
}