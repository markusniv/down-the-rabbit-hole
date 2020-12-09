
/// <summary>
/// Restores health over time
/// </summary>
public class RecoveryPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>

    public override string Tooltip => string.Format("Heals target for <color=red>5</color> seconds");

    /// <summary>
    /// Consumes the potion and adds status effect to the character
    /// </summary>
    public override void Consume()
    {
        // If the your health is full it will not use the item.

        if (Inventory.Character.CurrentHealth == Inventory.Character.MaxHealth) return;

        // Call the status effect.

        Inventory.Character.AddStatusEffect(new OverTimeHealing(Inventory.Character));

        base.Consume();
    }
}