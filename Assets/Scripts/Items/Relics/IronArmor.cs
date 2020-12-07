using UnityEngine;

public class IronArmor : PassiveRelic
{
    public float BonusHealth = 0.2f;
    private float OriginalValue;
    public override string Tooltip => string.Format("This rusty armor adds <color=red>{0}</color> bonus health", this.BonusHealth);

    public override void Apply()
    {
        base.Apply();
        /// <summary>
        /// Adds 20% of max health increase.
        /// </summary>
        OriginalValue = Inventory.Character.MaxHealth;
        Inventory.Character.MaxHealth += (int)(Inventory.Character.MaxHealth * BonusHealth);

    }

    public override void Clear()
    {
        /// <summary>
        /// Removes the + 200 from max health when removing equipment
        /// </summary>
        Inventory.Character.MaxHealth = OriginalValue;
        Inventory.Character.CurrentHealth = Mathf.Min(Inventory.Character.CurrentHealth, Inventory.Character.MaxHealth);


        base.Clear();
    }
}