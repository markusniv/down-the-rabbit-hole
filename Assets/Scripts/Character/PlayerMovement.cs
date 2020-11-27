
using UnityEngine;
/// <summary>
/// Player Movement. This script managers player movement.
/// </summary>
public class PlayerMovement : CharacterMovement
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        Character.Movement.Movement.y = Input.GetAxisRaw("Vertical");
        Character.Movement.Movement.x = Input.GetAxisRaw("Horizontal");
    }
}
