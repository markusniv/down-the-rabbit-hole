﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpeedReductionPotion : Consumable
{


    public override string Tooltip => string.Format("Use to decrease speed by <color=green>50%</color>");


    public override void Consume()
    {
        /// <summary>
        /// If your movement speed is equal or lower then 0 it will return item.
        /// </summary>
        if (Inventory.Character.Movement.MovementSpeedModifier <= 0) return;
        /// <summary>
        /// Call the status effect.
        /// </summary>
        Inventory.Character.AddStatusEffect(new SpeedReducted(Inventory.Character));
        base.Consume();

        
    }
}
