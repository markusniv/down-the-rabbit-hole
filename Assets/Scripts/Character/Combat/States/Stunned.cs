using UnityEngine;

public class Stunned : State
{
    /// <summary>
    /// In seconds
    /// </summary>
    private float Duration = 2f;

    public Stunned(Character character) : base(character)
    {
    }

    /// <summary>
    /// Immobilize character
    /// </summary>
    public override void OnStateEnter()
    {
        Character.Movement.CurrentState = new Immobile(Character);
        base.OnStateEnter();
    }

    /// <summary>
    /// Restore movement state
    /// </summary>
    public override void OnStateExit()
    {
        Character.Movement.CurrentState = Character.Movement.PreviousState;
        base.OnStateExit();
    }

    /// <summary>
    /// Calculates how long this stun should last
    /// </summary>
    public override void OnFixedUpdate()
    {
        Duration -= Time.fixedDeltaTime;
        if (Duration <= 0)
        {
            Character.Combat.CurrentState = new Idle(Character);
        }
        base.OnFixedUpdate();
    }
}