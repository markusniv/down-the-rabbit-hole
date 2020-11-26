

using UnityEngine;

/// <summary>
/// Player controlled state. Player controls the character who has this state.
/// </summary>
public class PlayerControlled : State
{
    public PlayerControlled(Character character) : base(character)
    {
    }

    public override void OnUpdate()
    {
        Character.Movement.Movement.x = Input.GetAxisRaw("Horizontal");
        Character.Movement.Movement.y = Input.GetAxisRaw("Vertical");
    }
}
