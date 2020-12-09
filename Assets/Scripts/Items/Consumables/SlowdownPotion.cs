
/// <summary>
/// Potion that will slow the character
/// </summary>
public class SlowdownPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>

    public override string Tooltip => string.Format("Slow down target for <color=blue>5</color> seconds");

    /// <summary>
    /// Consumes the item and add status effect
    /// </summary>
    public override void Consume()
    {
        // Adds the status effect.

        Inventory.Character.AddStatusEffect(new Slowdown(Inventory.Character));

        base.Consume();
    }
}