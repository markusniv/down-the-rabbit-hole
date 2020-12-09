/// <summary>
/// Player Movement. This script managers player movement.
/// </summary>
public class PlayerMovement : CharacterMovement
{
    /// <summary>
    /// Sets state to <see cref="PlayerControlled"/> Upon Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        CurrentState = new PlayerControlled(Character);
    }
}