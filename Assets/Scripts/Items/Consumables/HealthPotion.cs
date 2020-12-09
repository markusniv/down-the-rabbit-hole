/// <summary>
/// Potion that heals instantly
/// </summary>
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
    /// Consumes item and instantly restores health by <see cref="HealthRestoredOnUse"/>
    /// </summary>

    public override void Consume()
    {
        // If the your health is full it will not use the item.
        
        if (Inventory.Character.CurrentHealth == Inventory.Character.MaxHealth) return;
        
        // Adds to the current health 200 
        
        Inventory.Character.CurrentHealth += HealthRestoredOnUse;
        base.Consume();

    }

}