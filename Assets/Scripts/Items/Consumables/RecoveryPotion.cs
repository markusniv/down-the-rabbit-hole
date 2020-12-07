using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryPotion : Consumable
{
    public override string Tooltip => string.Format("Heal you for <color=green>{5}</color>s");


    public override void Consume()
    {
        /// <summary>
        /// Call the status effect.
        /// </summary>
        Inventory.Character.AddStatusEffect(new OverTimeHeal(Inventory.Character));

        base.Consume();
    }

}
