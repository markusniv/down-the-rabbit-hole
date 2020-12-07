using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPotion : Consumable
{
    public override string Tooltip => string.Format("Stuns target for a while");
    public override void Consume()
    {
        /// <summary>
        /// Call the status effect.
        /// </summary>
        Inventory.Character.Combat.CurrentState = new Stunned(Inventory.Character);
        base.Consume();
    }
}
