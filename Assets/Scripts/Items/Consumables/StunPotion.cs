﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPotion : Consumable
{
    public override void Consume()
    {
        /// <summary>
        /// Call the status effect.
        /// </summary>
        Inventory.Character.Combat.CurrentState = new Stunned(Inventory.Character);
        base.Consume();
    }
}
