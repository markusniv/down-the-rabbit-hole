using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>
    public override string Tooltip => string.Format("Target Speed increase by <color=blue>2</color>");

    /// <summary>
    /// Override consume method.
    /// </summary>

    public override void Consume()
    {
        /// <summary>
        /// If your movement speed is equal or higher then 6 it will return item.
        /// </summary>
        
        if (Inventory.Character.Movement.MovementSpeedModifier >=5 ) return;
        
        /// <summary>
        /// Call the status effect.
        /// </summary>
        
        Inventory.Character.AddStatusEffect(new SpeedBoost(Inventory.Character));
        base.Consume();
    }
}
