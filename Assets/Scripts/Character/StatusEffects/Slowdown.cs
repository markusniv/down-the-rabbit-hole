using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowdown : StatusEffect
{

    /// <summary>
    /// The duration how long it will take effect.
    /// </summary>
    float Duration = 5f;

    /// <summary>
    /// Slowdown per 0.1.
    /// </summary>
    float Reduce = 0.1f;
    float OriginalSpeed;


    public Slowdown(Character character) : base(character)
    {

    }

    public override void OnStatusEnter()
    {

        /// <summary>
        /// Gets the value of the original speed so we can return it.
        /// </summary>
        OriginalSpeed = Character.Movement.MovementSpeedModifier;

        base.OnStatusEnter();
    }
    public override void OnStatusExit()
    {
        /// <summary>
        /// The speed will return to the original values that it has
        /// </summary>
        Character.Movement.MovementSpeedModifier = OriginalSpeed;
        base.OnStatusExit();
    }


    public override void OnFixedUpdate()
    {

        /// <summary>
        /// Reduces the duration with current time per second 
        /// </summary>
        Duration -= Time.fixedDeltaTime;
        /// <summary>
        /// Reduce the the movement speed by 50% of the original movement
        /// </summary>
        Character.Movement.MovementSpeedModifier -= OriginalSpeed *Reduce * Time.fixedDeltaTime;
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
