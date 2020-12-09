/// <summary>
/// Potion that will increase movement speed of character
/// </summary>
public class SpeedPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>
    public override string Tooltip => string.Format("Target Speed increase by <color=blue>2</color> for 5 seconds");

    /// <summary>
    /// Consumes the item and adds the status effect
    /// </summary>
    public override void Consume()
    {
        // If your movement speed is equal or higher then 6 it will return item.

        if (Inventory.Character.Movement.MovementSpeedModifier >= 5) return;

        // Add the status effect.

        Inventory.Character.AddStatusEffect(new SpeedBoost(Inventory.Character));
        base.Consume();
    }
}