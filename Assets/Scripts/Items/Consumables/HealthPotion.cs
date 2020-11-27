public class HealthPotion : Consumable
{
    public override string Tooltip => string.Format("Use to restore <color=green>{0}</color> health", HealthRestoredOnUse);

    public int HealthRestoredOnUse;

    public override void Consume()
    {
        if (Inventory == null && Inventory.Parent.CurrentHealth == Inventory.Parent.MaxHealth) return;
        Inventory.Parent.CurrentHealth += HealthRestoredOnUse;
        base.Consume();

    }

}