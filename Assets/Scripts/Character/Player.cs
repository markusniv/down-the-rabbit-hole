
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

    /// <summary>
    /// Checks if player wants to use items
    /// </summary>
    protected override void Update()
    {
        if (Time.timeScale == 0f) return;
        base.Update();
        if (Inventory.ActiveItem is ICanHotbar hotbarItem && !DisplayInventory.Instance.MouseOverItem)
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