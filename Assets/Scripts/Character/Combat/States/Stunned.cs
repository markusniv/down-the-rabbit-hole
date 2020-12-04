using UnityEngine;

public class Stunned : State
{
    /// <summary>
    /// In seconds
    /// </summary>
    private float Duration = 0.5f;

    public Stunned(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        Character.Movement.CurrentState = new Immobile(Character);
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        Character.Movement.CurrentState = Character.Movement.PreviousState;
        base.OnStateExit();
    }

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