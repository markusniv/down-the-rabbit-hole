public class HealthPotion : Consumable
{
    public override string Tooltip => string.Format("Use to restore <color=green>{0}</color> health", HealthRestoredOnUse);

    public int HealthRestoredOnUse;

    public override void Consume()
    {
        if (Inventory == null && Inventory.Character.CurrentHealth == Inventory.Character.MaxHealth) return;
        Inventory.Character.CurrentHealth += HealthRestoredOnUse;
        base.Consume();

    }

}