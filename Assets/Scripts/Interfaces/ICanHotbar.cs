using UnityEngine;

/// <summary>
/// Items with this interface can be used in hotbar
/// </summary>
public interface ICanHotbar
{
    /// <summary>
    /// Icon that will be used in hotbar
    /// </summary>
    Sprite Icon { get; }

    SpriteRenderer SpriteRenderer { get; set; }

    /// <summary>
    /// Action to perform when Player presses left click
    /// </summary>
    void PrimaryUse();


    /// <summary>
    /// Action to perform when Player presses right click
    /// </summary>
    void SecondaryUse();
}