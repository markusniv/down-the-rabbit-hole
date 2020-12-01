using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedReductionPotion : Consumable
{
    public float SpeedReduction = 0.02f;
    public override string Tooltip => string.Format("Use to decrease speed by <color=green>{0}%</color>", SpeedReduction);


    public override void Consume()
    {
        /// <summary>
        /// If the amount of speed is lower or equal it will return the potion,so you would be able to move still
        /// </summary>
        if (Inventory.Character.GetComponent<CharacterMovement>().MovementSpeedModifier <= 0.8) return;
        /// <summary>
        /// Fetching the MovementSpeedModifier so it can be reduce
        /// </summary>
        float result = Inventory.Character.GetComponent<CharacterMovement>().MovementSpeedModifier -= SpeedReduction;
        /// <summary>
        /// How many times this item can be used until its destroyed.
        /// </summary>
        Inventory.Character.GetComponent<CharacterMovement>().MovementSpeedModifier = result;
        base.Consume();
    }
}
