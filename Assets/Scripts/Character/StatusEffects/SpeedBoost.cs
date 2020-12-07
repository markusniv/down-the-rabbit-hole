using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : StatusEffect
{
    float SpeedChange = 2f; // How much will increase the status.

    float Duration = 5f; // seconds

    public SpeedBoost(Character character) : base(character)
    {
     
    }

    public override void OnStatusEnter()
    {

        /// <summary>
        /// Will increase the speed by 2.
        /// </summary>
        Character.Movement.MovementSpeedModifier += SpeedChange;
        base.OnStatusEnter();
    }

    public override void OnStatusExit()
    {
        /// <summary>
        /// Will decrease the speed by 2.
        /// </summary>
        Character.Movement.MovementSpeedModifier -= SpeedChange;
        base.OnStatusExit();
    }

    public override void OnFixedUpdate()
    {
        /// <summary>
        /// Reduces the duration with current time per second 
        /// </summary>
        Duration -= Time.fixedDeltaTime;
        /// <summary>
        /// if the duration is 0 or smaller it will stop the effect.
        /// </summary>
        if (Duration <= 0)
        {
            Character.RemoveStatusEffect(this);
        }
        base.OnFixedUpdate();
    }
}

