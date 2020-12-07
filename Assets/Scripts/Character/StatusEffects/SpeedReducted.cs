using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedReducted: StatusEffect
{
    /// <summary>
    /// Will be decrease by 1
    /// </summary>   
    float Reduce = 1f;
    /// <summary>
    /// The duration how long it will take effect.
    /// </summary>
    float Duration = 5f;
    private float Original;
   
    public SpeedReducted(Character character) : base(character)
    {
       
    }

    public override void OnStatusEnter()
    {

        /// <summary>
        /// Reduce by 1 speed.
        /// </summary>
        Character.Movement.MovementSpeedModifier -= Reduce;
        

        base.OnStatusEnter();
    }
    public override void OnStatusExit()
    {
        /// <summary>
        /// The speed swill return to the original values that it has
        /// </summary
       Character.Movement.MovementSpeedModifier += Reduce;
        
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
