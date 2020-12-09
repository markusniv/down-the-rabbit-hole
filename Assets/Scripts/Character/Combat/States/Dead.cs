/// <summary>
/// Character is dead while in this state. They cannot perform any actions.
/// </summary>
public class Dead : State
{
    public Dead(Character character) : base(character)
    {
    }

    /// <summary>
    /// Immobilize character
    /// </summary>
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Character.Movement.CurrentState = new Immobile(Character);
    }


    /// <summary>
    /// Restore movement state incase character is revived somehow
    /// </summary>
    public override void OnStateExit()
    {
        base.OnStateExit();
        Character.Movement.CurrentState = Character.Movement.PreviousState;
    }
}