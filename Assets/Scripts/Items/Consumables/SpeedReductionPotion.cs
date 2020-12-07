using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpeedReductionPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>

    public override string Tooltip => string.Format("Target speed is decrease by <color=blue>1</color>");


    public override void Consume()
    {
        /// <summary>
        /// If your movement speed is equal or lower then 0 it will return item.
        /// </summary>
       
        if (Inventory.Character.Movement.MovementSpeedModifier <= 0) return;
        
        /// <summary>
        /// Call the status effect.
        /// </summary>
        
        Inventory.Character.AddStatusEffect(new SpeedReducted(Inventory.Character));
        
        base.Consume();

        
    }
}
