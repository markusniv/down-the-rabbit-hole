
/// <summary>
/// Potion that reduces health over time
/// </summary>
public class PoisonPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>

    public override string Tooltip => string.Format("Poisons target for <color=purple>5</color> seconds");

    /// <summary>
    /// Consumes item and adds status effect
    /// </summary>

    public override void Consume()
    {
        Inventory.Character.AddStatusEffect(new Poisoned(Inventory.Character));

        base.Consume();
    }
}