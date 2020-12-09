
/// <summary>
/// Potion that will slow the character
/// </summary>
public class SpeedReductionPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>

    public override string Tooltip => string.Format("Target speed is decrease by <color=blue>0.5</color> for 5 seconds");

    /// <summary>
    /// Consumes the items and Adds status effect
    /// </summary>
    public override void Consume()
    {
        // If your movement speed is equal or lower then 0 it will return item.

        if (Inventory.Character.Movement.MovementSpeedModifier <= 0) return;

        // Adds the status effect.

        Inventory.Character.AddStatusEffect(new SpeedReducted(Inventory.Character));

        base.Consume();
    }
}