using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : Consumable
{
   // public override string Tooltip => string.Format("Poison <color=green>{100}</color> ", HealthReduce);


    public override void Consume()
    {
        Inventory.Character.AddStatusEffect(new Poisoned(Inventory.Character));

        base.Consume();
    }

}
