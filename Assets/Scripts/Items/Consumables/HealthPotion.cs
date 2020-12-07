public class HealthPotion : Consumable
{

    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>
    
    public override string Tooltip => string.Format("Target health restored by <color=red>{0}</color>", HealthRestoredOnUse);

    /// <summary>
    /// Sets HealthRestoredOnUse to 200.
    /// </summary>
    
    public int HealthRestoredOnUse= 200;
    
    /// <summary>
    /// Override consume method.
    /// </summary>

    public override void Consume()
    {
        /// <summary>
        /// If the your health is full it will not use the item.
        /// </summary>
        
        if (Inventory.Character.CurrentHealth == Inventory.Character.MaxHealth) return;
        
        /// <summary>
        /// Adds to the current health 200 
        /// </summary>
        
        Inventory.Character.CurrentHealth += HealthRestoredOnUse;
        base.Consume();

    }

}