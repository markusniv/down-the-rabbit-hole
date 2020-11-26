using UnityEngine;

/// <summary>
/// Blocking state. Characters in this state cannot move, but also don't take any damage from weapons.
/// </summary>
public class Blocking : State
{
    State PreviousMovementState;

    /// <summary>
    /// Duration of the block in seconds
    /// </summary>
    public float Duration = 0.5f;
    public Blocking(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        // TODO: Add something visual to indicate blocking
        PreviousMovementState = Character.Movement.CurrentState;
        Character.Movement.CurrentState = new Immobile(Character);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        Character.Movement.CurrentState = PreviousMovementState;
    }
    
    /// <summary>
    /// This is called when Character is hit while they are blocking
    /// </summary>
    public void OnHit()
    {
        // TODO: Play Deflect sound
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Duration -= Time.fixedDeltaTime;
        if(Duration <= 0f)
        {
            Character.Combat.CurrentState = new Idle(Character);
        }
    }
}