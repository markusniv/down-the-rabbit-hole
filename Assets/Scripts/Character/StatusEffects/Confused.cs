using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confused : StatusEffect
{
    

    float Duration = 5f; // seconds

    public Confused(Character character) : base(character)
    {
      
    }

    public override void OnStatusEnter()
    {

        /// <summary>
        /// Make you move reverse order.
        /// </summary>
        Character.Movement.MovementSpeedModifier *=-1;
        base.OnStatusEnter();
    }

    public override void OnStatusExit()
    {
        /// <summary>
        /// Will make it positive so you can move correctly.
        /// </summary>
        Character.Movement.MovementSpeedModifier *=-1;
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

