
using UnityEngine;
/// <summary>
/// Player controlled character. There is only one player at the game.
/// </summary>
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : Character
{


    public override void Die()
    {
        // TODO: Set Death state
        base.Die();
    }

    protected override void Update()
    {
        base.Update();
        // TODO: Hotbar item usage
    }
}