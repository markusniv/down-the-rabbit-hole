using UnityEngine;

/// <summary>
/// Characters with this status are slowed down
/// </summary>
public class Slowdown : StatusEffect
{
    /// <summary>
    /// The duration how long it will take effect.
    /// </summary>
    private float Duration = 5f;

    /// <summary>
    /// Slowdown per 0.1.
    /// </summary>
    private float Reduce = 0.1f;

    private float OriginalSpeed;

    public Slowdown(Character character) : base(character)
    {
    }

    /// <summary>
    /// Saves original speed.
    /// </summary>
    public override void OnStatusEnter()
    {
        // Gets the value of the original speed so we can return it.
        OriginalSpeed = Character.Movement.MovementSpeedModifier;

        base.OnStatusEnter();
    }

    /// <summary>
    /// Restores movement to normal
    /// </summary>
    public override void OnStatusExit()
    {
        // The speed will return to the original values that it has
        Character.Movement.MovementSpeedModifier = OriginalSpeed;
        base.OnStatusExit();
    }

    /// <summary>
    /// Reduces movement speed over time and removes this effect after <see cref="Duration"/> is 0
    /// </summary>
    public override void OnFixedUpdate()
    {
        // Reduces the duration with current time per second
        Duration -= Time.fixedDeltaTime;
        // Reduce the the movement speed by 50% of the original movement
        Character.Movement.MovementSpeedModifier -= OriginalSpeed * Reduce * Time.fixedDeltaTime;

        // if the duration is 0 or smaller it will stop the effect.

        if (Duration <= 0)
        {
            Character.RemoveStatusEffect(this);
        }

        base.OnFixedUpdate();
    }
}