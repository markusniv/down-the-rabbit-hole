/// <summary>
/// Character is dead while in this state. They cannot perform any actions.
/// </summary>
public class Dead : State
{
    public Dead(Character character) : base(character)
    {
    }
}