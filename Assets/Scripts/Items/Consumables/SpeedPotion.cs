using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : Consumable
{
    public float SpeedBoost = 2f;
    public override string Tooltip => string.Format("Use to increase speed by <color=green>{0}%</color>", SpeedBoost * 100);


    public override void Consume()
    {

        Inventory.Character.AddStatusEffect(new SpeedBoost(Inventory.Character, SpeedBoost));
        base.Consume();
    }
}
