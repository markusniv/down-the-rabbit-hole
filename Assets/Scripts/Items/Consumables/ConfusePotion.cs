public class ConfusePotion : Consumable
{
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>
    
    public override string Tooltip => string.Format("Confuse target for <color=orange>5</color> seconds");

    /// <summary>
    /// Override consume method.
    /// </summary>

    public override void Consume()
    {
        /// <summary>
        /// If your movement speed is equal or lower then 0 it will return item.
        /// </summary>
        
        if (Inventory.Character.Movement.MovementSpeedModifier <= 0) return;

        /// <summary>
        /// Call the status effect.
        /// </summary>
        
        Inventory.Character.AddStatusEffect(new Confused(Inventory.Character));

        base.Consume();

    }

}