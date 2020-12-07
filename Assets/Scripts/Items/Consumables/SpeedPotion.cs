using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : Consumable
{
    public float SpeedBoost = 2f;
    public override string Tooltip => string.Format("Use to increase speed by <color=green>{0}%</color>", SpeedBoost * 100);

    public override void Consume()
    {
        /// <summary>
        /// If your movement speed is equal or higher then 6 it will return item.
        /// </summary>
        if (Inventory.Character.Movement.MovementSpeedModifier >=5 ) return;
        /// <summary>
        /// Call the status effect.
        /// </summary>
        Inventory.Character.AddStatusEffect(new SpeedBoost(Inventory.Character, SpeedBoost));
        base.Consume();
    }
}
