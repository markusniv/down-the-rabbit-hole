using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : Consumable
{
    public float SpeedBoost = 0.1f;
    public override string Tooltip => string.Format("Use to increase speed by <color=green>{0}%</color>", SpeedBoost);


    public override void Consume()
    {
        /// <summary>
        /// If the current speed value is 2 or higher it will return the item, cause we do not want it to be to fast to break trough the wall.
        /// </summary>
        if (Inventory.Character.GetComponent<CharacterMovement>().MovementSpeedModifier >= 2) return;
        /// <summary>
        /// Adds to the current speed value 0.1.
        /// </summary>
        float result = Inventory.Character.GetComponent<CharacterMovement>().MovementSpeedModifier += SpeedBoost;
        /// <summary>
        /// Setting the current speed value to match result.
        /// </summary>
        Inventory.Character.GetComponent<CharacterMovement>().MovementSpeedModifier = result;
        base.Consume();


    }
}
