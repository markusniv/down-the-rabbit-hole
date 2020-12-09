/// <summary>
/// Attacking state. Characters in this state are currently attacking.
/// </summary>
public class Attacking : State
{
    public Attacking(Character character) : base(character)
    {
    }

    /// <summary>
    /// Start the attack, immobilize character and show current weapon
    /// </summary>
    public override void OnStateEnter()
    {
        Character.Combat.InvokeAttackStart(Character.Combat.CurrentWeapon);
        Character.Movement.CurrentState = new Immobile(Character);
        Character.Combat.CurrentWeapon?.Show();
    }

    /// <summary>
    /// End the attack, restore previous movement state and hide the weapon
    /// </summary>
    public override void OnStateExit()
    {
        Character.Combat.InvokeAttackEnd(Character.Combat.CurrentWeapon);
        Character.Movement.CurrentState = Character.Movement.PreviousState;
        Character.Combat.CurrentWeapon.Hide();
    }

    /// <summary>
    /// Animate the attack on each frame
    /// </summary>
    public override void OnUpdate()
    {
        Character.Combat.CurrentWeapon?.Attack();
    }
}