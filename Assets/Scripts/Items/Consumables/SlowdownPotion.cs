using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownPotion : Consumable
{    
     /// <summary>
     /// when the mouse is hovering the item this text will show it self.
     /// </summary>
     
    public override string Tooltip => string.Format("Slow down target for <color=blue>5</color> seconds");

    /// <summary>
    /// Override consume method.
    /// </summary>
   
    public override void Consume()
    {
       
        /// <summary>
        /// Call the status effect.
        /// </summary>
       
        Inventory.Character.AddStatusEffect(new Slowdown(Inventory.Character));

        base.Consume();
    }

}
