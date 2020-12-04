using UnityEngine;

public class SpeedBoots : PassiveRelic
{
    public float BonusSpeed = 0.2f;

    public override string Tooltip => string.Format("These boots increase your speed by <color=red>2</color>.");

    public override void Apply()
    {
        base.Apply();
        /// <summary>
        /// Adds to as a passive equipment 0,2% more speed
        /// </summary>
        Inventory.Character.GetComponent<CharacterMovement>().MovementSpeedModifier += (Inventory.Character.GetComponent<CharacterMovement>().MovementSpeedModifier * BonusSpeed);
    }

    public override void Clear()
    {
        /// <summary>
        /// remove the 0,2% speed when removing equipment.
        /// </summary>
        Inventory.Character.GetComponent<CharacterMovement>().MovementSpeedModifier -= (Inventory.Character.GetComponent<CharacterMovement>().MovementSpeedModifier * BonusSpeed);


        base.Clear();
    }
}