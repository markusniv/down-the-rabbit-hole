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

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        // TODO: Play weapons attack sound.
        Character.Movement.CurrentState = new Immobile(Character);

        // TODO: Character.Combat.CurrentWeapon?.Show()
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        Character.Movement.CurrentState = Character.Movement.PreviousState;
        // TODO: Character.Combat.CurrentWeapon?.Hide()
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // TODO: Character.Combat.CurrentWeapon?.Attack();
    }
}
