
using System;
using UnityEngine;
/// <summary>
/// Player controlled character. There is only one player at the game.
/// </summary>
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : Character
{
    private float _score;

    /// <summary>
    /// Players current score.
    /// </summary>
    public float Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            OnScoreChange?.Invoke(_score);
        }
    }

    public event Action<float> OnScoreChange;

    public override void Die()
    {
        // TODO: Set Death state
        base.Die();
    }

    protected override void Update()
    {
        base.Update();
        if (Inventory.ActiveItem is ICanHotbar hotbarItem)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hotbarItem.PrimaryUse();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                hotbarItem.SecondaryUse();
            }
        }
    }
}