/// <summary>
/// Idle state. Characters in this state can perform attack actions.
/// </summary>
public class Idle : State
{
    public Idle(Character character) : base(character)
    {
    }
}
