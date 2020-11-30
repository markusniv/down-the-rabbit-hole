using UnityEngine;

/// <summary>
/// Player controlled. Characters in this state are controlled by player.
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