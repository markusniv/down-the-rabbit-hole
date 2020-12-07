using UnityEngine;

public class IronArmor : PassiveRelic
{
    public int BonusHealth = 100;
    private int OriginalValue;
    public override string Tooltip => string.Format("This rusty armor adds <color=red>{0}</color> bonus health", BonusHealth);

    public override void Apply()
    {
        /// <summary>
        /// When you apply it will call apply
        /// </summary>
        
        base.Apply();

        /// <summary>
        /// Adds 100 of max health increase.
        /// </summary>
        
        Inventory.Character.MaxHealth += BonusHealth;

    }


    public override void Clear()
    {
        /// <summary>
        /// Removes the 100 from max health when removing equipment
        /// </summary>
        
        Inventory.Character.MaxHealth -= BonusHealth;
        /// <summary>
        /// It will take the smallest health avaible.
        /// </summary>
        Inventory.Character.CurrentHealth = Mathf.Min(Inventory.Character.CurrentHealth, Inventory.Character.MaxHealth);


        base.Clear();
    }
}