using UnityEngine;

/// <summary>
/// Characters with this status get speed boost for some duration
/// </summary>
public class SpeedBoost : StatusEffect
{
    /// <summary>
    /// How much speed is changed
    /// </summary>
    private float SpeedChange = 1f;

    /// <summary>
    /// Duration of this effect in seconds
    /// </summary>
    private float Duration = 5f; // seconds

    public SpeedBoost(Character character) : base(character)
    {
    }

    /// <summary>
    /// Increases <see cref="CharacterMovement.MovementSpeedModifier"/> by <see cref="SpeedChange"/>
    /// </summary>
    public override void OnStatusEnter()
    {
        // Will increase the speed by 1.
        Character.Movement.MovementSpeedModifier += SpeedChange;
        base.OnStatusEnter();
    }

    /// <summary>
    /// Decreases <see cref="CharacterMovement.MovementSpeedModifier"/> by <see cref="SpeedChange"/>
    /// </summary>
    public override void OnStatusExit()
    {
        // Will decrease the speed by 1.
        Character.Movement.MovementSpeedModifier -= SpeedChange;
        base.OnStatusExit();
    }

    /// <summary>
    /// Removes effect after <see cref="Duration"/> is 0
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