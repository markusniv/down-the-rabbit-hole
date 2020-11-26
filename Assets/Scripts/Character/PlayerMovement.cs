
/// <summary>
/// Player Movement. This script managers player movement.
/// </summary>
public class PlayerMovement : CharacterMovement
{
    protected override void Awake()
    {
        base.Awake();
        CurrentState = new PlayerControlled(Character);
    }
}
