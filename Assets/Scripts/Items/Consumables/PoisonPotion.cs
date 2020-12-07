using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : Consumable
{
    public override string Tooltip => string.Format("Poisons the target, dealing damage over time");


    public override void Consume()
    {
        /// <summary>
        /// Call the status effect.
        /// </summary>
        Inventory.Character.AddStatusEffect(new Poisoned(Inventory.Character));

        base.Consume();
    }

}
