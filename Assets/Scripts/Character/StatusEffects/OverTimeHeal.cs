using UnityEngine;

/// <summary>
/// Characters with this status will heal over time
/// </summary>
public class OverTimeHealing : StatusEffect
{
    /// <summary>
    /// The duration how long it will take effect.
    /// </summary>
    private float Duration = 5f;

    /// <summary>
    /// How much Healing per second
    /// </summary>
    private float Health = 25f;

    public OverTimeHealing(Character character) : base(character)
    {
    }

    /// <summary>
    /// Increases health over time. Removes effect after <see cref="Duration"/> is 0
    /// </summary>
    public override void OnFixedUpdate()
    {
        // Reduces the duration with current time per second
        Duration -= Time.fixedDeltaTime;

        // Reduce from current health 50 damage per second for 5 second
        Character.CurrentHealth += Health * Time.fixedDeltaTime;

        // if the duration is 0 or smaller it will stop the effect.

        if (Duration <= 0)
        {
            Character.RemoveStatusEffect(this);
        }
        base.OnFixedUpdate();
    }
}