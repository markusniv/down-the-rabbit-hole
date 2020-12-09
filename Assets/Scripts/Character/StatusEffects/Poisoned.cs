using UnityEngine;

/// <summary>
/// Characters with this status will take damage over time
/// </summary>
public class Poisoned : StatusEffect
{
    /// <summary>
    /// The duration how long it will take effect.
    /// </summary>
    private float Duration = 5f;

    /// <summary>
    /// How much damage per second
    /// </summary>
    private float DamagePerTick = 10f;

    public Poisoned(Character character) : base(character)
    {
    }

    /// <summary>
    /// Deals damage over time and removes this effect if <see cref="Duration"/> is 0
    /// </summary>
    public override void OnFixedUpdate()
    {
        // Reduces the duration with current time per second
        Duration -= Time.fixedDeltaTime;
        // Reduce from current health 10 damage per second for 5 second
        Character.CurrentHealth -= DamagePerTick * Time.fixedDeltaTime;

        // if the duration is 0 or smaller it will stop the effect.
        if (Duration <= 0)
        {
            Character.RemoveStatusEffect(this);
        }
        base.OnFixedUpdate();
    }
}