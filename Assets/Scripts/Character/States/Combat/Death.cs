/// <summary>
/// Death state. Character is dead while they have this state. They cannot perform any actions during this state.
/// </summary>
public class Death : State
{
    public Death(Character character) : base(character)
    {
    }
}