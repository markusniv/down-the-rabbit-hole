/// <summary>
/// Attacking state. Characters in this state are currently attacking. They cannot move while they have this state.
/// </summary>
public class Attacking : State
{
    private State PreviousMovementState;

    public Attacking(Character character) : base(character)
    {
    }

    /// <inheritdoc/>
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        // TODO: Play sound
        // TODO: Invoke attack start?
        PreviousMovementState = Character.Movement.CurrentState;
        Character.Movement.CurrentState = new Immobile(Character);

        // TODO: enable this lane when weapons are ready
        // Character.Combat.CurrentWeapon?.Show();
    }

    /// <inheritdoc/>
    public override void OnStateExit()
    {
        base.OnStateExit();
        // TODO: Invoke attack end
        Character.Movement.CurrentState = PreviousMovementState;
        // TODO: enable this lane when weapons are ready
        // Character.Combat.CurrentWeapon?.Hide();
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        base.OnUpdate();
        // TODO: Uncomment when weapon is ready
        //Character.Combat.CurrentWeapon?.Attack();
    }
}