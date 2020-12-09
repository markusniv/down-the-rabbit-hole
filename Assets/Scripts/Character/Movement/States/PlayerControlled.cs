using UnityEngine;

/// <summary>
/// Player controlled. Characters in this state are controlled by player.
/// </summary>
public class PlayerControlled : State
{
    public PlayerControlled(Character character) : base(character)
    {
    }

    /// <summary>
    /// Gets movement from input system
    /// </summary>
    public override void OnUpdate()
    {
        if (Time.timeScale == 0f) return; 
        Character.Movement.Movement.x = Input.GetAxisRaw("Horizontal");
        Character.Movement.Movement.y = Input.GetAxisRaw("Vertical");
    }
}