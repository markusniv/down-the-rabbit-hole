﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTimeHeal : StatusEffect
{

    /// <summary>
    /// The duration how long it will take effect.
    /// </summary>
    float Duration = 5f;

    /// <summary>
    /// How much Healing per second
    /// </summary>
    int Health = 10;

    public OverTimeHeal(Character character) : base(character)
    {
    }


    public override void OnFixedUpdate()
    {

        /// <summary>
        /// Reduces the duration with current time per second 
        /// </summary>
        Duration -= Time.fixedDeltaTime;
        /// <summary>
        /// Reduce from current health 50 damage per second for 5 second
        /// </summary>
        Character.CurrentHealth += Health * Time.fixedDeltaTime;
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