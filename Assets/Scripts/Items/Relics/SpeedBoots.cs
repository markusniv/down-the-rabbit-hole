/// <summary>
/// Relic that adds movement speed to character
/// </summary>
public class SpeedBoots : PassiveRelic
{
    /// <summary>
    /// Normalized movement speed bonus. 0.1f = 10% increase
    /// </summary>
    public float BonusSpeed = 0.1f;

    /// <inheritdoc/>
    public override string Tooltip => string.Format("These boots increase your speed by <color=blue>2</color>%");

    /// <summary>
    /// Adds speed bonus
    /// </summary>
    public override void Apply()
    {
        base.Apply();
        // Adds to as a passive equipment 0,1% more speed
        Inventory.Character.Movement.MovementSpeedModifier += BonusSpeed;
    }

    /// <summary>
    /// Removes speed bonus
    /// </summary>
    public override void Clear()
    {
        // remove the 0,1% speed when removing equipment.
        Inventory.Character.Movement.MovementSpeedModifier -= BonusSpeed;

        base.Clear();
    }
}