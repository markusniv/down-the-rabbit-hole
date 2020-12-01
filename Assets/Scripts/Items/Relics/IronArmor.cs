using UnityEngine;

public class IronArmor : PassiveRelic
{
    public int BonusHealth = 200;

    public override string Tooltip => string.Format("This rusty armor adds <color=red>{0}</color> bonus health", this.BonusHealth);

    public override void Apply()
    {
        base.Apply();
        Inventory.Character.MaxHealth += BonusHealth;
    }

    public override void Clear()
    {
        Inventory.Character.MaxHealth -= BonusHealth;
        Inventory.Character.CurrentHealth = Mathf.Min(Inventory.Character.CurrentHealth, Inventory.Character.MaxHealth);


        base.Clear();
    }
}