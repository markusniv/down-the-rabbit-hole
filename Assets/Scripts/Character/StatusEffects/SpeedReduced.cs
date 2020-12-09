using UnityEngine;

/// <summary>
/// Characters with this status will have their speed reduced
/// </summary>
public class SpeedReducted : StatusEffect
{
    /// <summary>
    /// Will be decrease by 0.5f
    /// </summary>
    private float Reduce = 0.5f;

    /// <summary>
    /// The duration how long it will take effect.
    /// </summary>
    private float Duration = 5f;

    public SpeedReducted(Character character) : base(character)
    {
    }

    /// <summary>
    /// Reduces movement by <see cref="Reduce"/>
    /// </summary>
    public override void OnStatusEnter()
    {
        // Reduce by 0.5f.
        Character.Movement.MovementSpeedModifier -= Reduce;

        base.OnStatusEnter();
    }

    /// <summary>
    /// Restores Movement speed
    /// </summary>
    public override void OnStatusExit()
    {
        // The speed will return to the original values that it has
        Character.Movement.MovementSpeedModifier += Reduce;

        base.OnStatusExit();
    }

    /// <summary>
    /// Removes effect when <see cref="Duration"/> is 0
    /// </summary>
    public override void OnFixedUpdate()
    {
        // Reduces the duration with current time per second
        Duration -= Time.fixedDeltaTime;

        // if the duration is 0 or smaller it will stop the effect.
        if (Duration <= 0)
        {
            Character.RemoveStatusEffect(this);
        }

        base.OnFixedUpdate();
    }
}