using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>
  
    public override string Tooltip => string.Format("Heals target for <color=red>5</color> seconds");
    /// <summary>
    /// Override consume method.
    /// </summary>
    
    public override void Consume()
    {
        /// <summary>
        /// If the your health is full it will not use the item.
        /// </summary>
    
        if (Inventory.Character.CurrentHealth == Inventory.Character.MaxHealth) return;
       
        /// <summary>
        /// Call the status effect.
        /// </summary>
        
        Inventory.Character.AddStatusEffect(new OverTimeHealing(Inventory.Character));

        base.Consume();
    }

}
