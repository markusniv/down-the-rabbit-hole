
/// <summary>
/// Potion that confuses characters
/// </summary>
public class ConfusePotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>
    
    public override string Tooltip => string.Format("Confuse target for <color=orange>5</color> seconds");

    /// <summary>
    /// Adds <see cref="Confused"/> status effect to the character
    /// </summary>
    public override void Consume()
    {
        // If your movement speed is equal or lower then 0 it will return item.
        if (Inventory.Character.Movement.MovementSpeedModifier <= 0) return;

        // Call the status effect.
        Inventory.Character.AddStatusEffect(new Confused(Inventory.Character));

        base.Consume();

    }

}