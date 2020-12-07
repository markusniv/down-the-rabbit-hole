using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>
   
    public override string Tooltip => string.Format("Stuns target for <color=yellow>5</color> seconds");
    public override void Consume()
    {
        /// <summary>
        /// Call the status effect.
        /// </summary>
       
        Inventory.Character.Combat.CurrentState = new Stunned(Inventory.Character);
        
        base.Consume();
    }
}
