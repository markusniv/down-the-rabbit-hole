


using UnityEngine;
/// <summary>
/// Immobile state. Character cannot move while in this state.
/// </summary>
public class Immobile : State
{
    public Immobile(Character character) : base(character)
    {
    }


    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Character.Movement.Movement = Vector2.zero;
        Character.Movement.Immobile = true;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        Character.Movement.Immobile = false;
    }
}
