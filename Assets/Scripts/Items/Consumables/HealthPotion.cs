public class HealthPotion : Consumable
{
    public override string Tooltip => string.Format("Use to restore <color=green>{0}</color> health", HealthRestoredOnUse);

    public int HealthRestoredOnUse;

    public override void Consume()
    {
        /// <summary>
        /// If the your health is full it will not use the item.
        /// </summary>
        if (Inventory == null && Inventory.Character.CurrentHealth == Inventory.Character.MaxHealth) return;
        /// <summary>
        /// Adds to the current health 200 
        /// </summary>
        Inventory.Character.CurrentHealth += HealthRestoredOnUse;
        base.Consume();

    }

}