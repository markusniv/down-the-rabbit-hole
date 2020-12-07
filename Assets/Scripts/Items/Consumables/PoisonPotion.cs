﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : Consumable
{
    public override string Tooltip => string.Format("Poison <color=green>{5}</color>");


    public override void Consume()
    {
        /// <summary>
        /// Call the status effect.
        /// </summary>
        Inventory.Character.AddStatusEffect(new Poisoned(Inventory.Character));

        base.Consume();
    }

}
