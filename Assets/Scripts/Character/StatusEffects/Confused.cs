using UnityEngine;

/// <summary>
/// Characters with this status will be confused and will move backwards
/// </summary>
public class Confused : StatusEffect
{
    /// <summary>
    /// Effect duration in seconds
    /// </summary>
    private float Duration = 5f; // seconds

    public Confused(Character character) : base(character)
    {
    }


    /// <summary>
    /// Reverses <see cref="CharacterMovement.MovementSpeedModifier"/>
    /// </summary>
    public override void OnStatusEnter()
    {
        Character.Movement.MovementSpeedModifier *= -1;
        base.OnStatusEnter();
    }

    /// <summary>
    /// Restores <see cref="CharacterMovement.MovementSpeedModifier"/> to positive
    /// </summary>
    public override void OnStatusExit()
    {

        Character.Movement.MovementSpeedModifier *= -1;
        base.OnStatusExit();
    }

    /// <summary>
    /// Calculates when this effect should end
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