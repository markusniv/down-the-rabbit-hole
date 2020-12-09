using UnityEngine;

public class SpeedBoots : PassiveRelic
{
    public float BonusSpeed = 0.1f;

    public override string Tooltip => string.Format("These boots increase your speed by <color=blue>2</color>%");

    public override void Apply()
    {
        base.Apply();
        /// <summary>
        /// Adds to as a passive equipment 0,1% more speed
        /// </summary>
        Inventory.Character.Movement.MovementSpeedModifier +=  BonusSpeed;
    }

    public override void Clear()
    {
        /// <summary>
        /// remove the 0,1% speed when removing equipment.
        /// </summary>
        Inventory.Character.Movement.MovementSpeedModifier -= BonusSpeed;


        base.Clear();
    }
}