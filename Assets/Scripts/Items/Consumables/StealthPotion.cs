using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthPotion: Consumable
    {
    public override void Consume()
    {

        /// <summary>
        /// Call the stealth status effect.
        /// </summary>
        Inventory.Character.AddStatusEffect(new Stealthed(Inventory.Character));
        base.Consume();
    }
    
    }

