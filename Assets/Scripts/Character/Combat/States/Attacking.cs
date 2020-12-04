using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attacking state. Characters in this state are currently attacking.
/// </summary>
public class Attacking : State
{

    public Attacking(Character character) : base(character)
    {
    }

    private State PreviousMovementState;

    public override void OnStateEnter()
    {
        //base.OnStateEnter();
        Character.Combat.InvokeAttackStart(Character.Combat.CurrentWeapon);
        PreviousMovementState = Character.Movement.CurrentState;
        Character.Movement.CurrentState = new Immobile(Character);

        Character.Combat.CurrentWeapon?.Show();
    }

    public override void OnStateExit()
    {
        //base.OnStateExit();
        Character.Combat.InvokeAttackEnd(Character.Combat.CurrentWeapon);
        Character.Movement.CurrentState = Character.Movement.PreviousState;
        Character.Combat.CurrentWeapon.Hide();
    }

    public override void OnUpdate()
    {
        Character.Combat.CurrentWeapon?.Attack();

    }
}
