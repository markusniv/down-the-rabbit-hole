/// <summary>
/// Potion that will stun the character
/// </summary>
public class StunPotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>

    public override string Tooltip => string.Format("Stuns target for <color=yellow>5</color> seconds");

    /// <summary>
    /// Consumes the potion and set state to stunned
    /// </summary>
    public override void Consume()
    {
        // Adds the status effect.

        Inventory.Character.Combat.CurrentState = new Stunned(Inventory.Character);

        base.Consume();
    }
}