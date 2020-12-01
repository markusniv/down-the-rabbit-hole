using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : Consumable
{
    public override string Tooltip => string.Format("Poison <color=green>{100}</color> ", HealthReduce);

    public int HealthReduce = 100;


    public override void Consume()
    {
        /// <summary>
        /// Reduces from health 100.
        /// </summary>
        Inventory.Character.CurrentHealth -= HealthReduce;

        base.Consume();
    }

}
