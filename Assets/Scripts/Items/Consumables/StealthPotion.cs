/// <summary>
/// Potion that will put character on stealth
/// </summary>
public class StealthPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>

    public override string Tooltip => string.Format("Use before going to a another room to activate stealth for <color=silver>5</color> seconds");

    public override void Consume()
    {
        /// <summary>
        /// Call the stealth status effect.
        /// </summary>

        Inventory.Character.AddStatusEffect(new Stealthed(Inventory.Character));

        base.Consume();
    }
}