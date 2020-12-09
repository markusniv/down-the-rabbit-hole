using UnityEngine;

/// <summary>
/// Relic that adds bonus health to character
/// </summary>
public class IronArmor : PassiveRelic
{
    public int BonusHealth = 100;
    public override string Tooltip => string.Format("This rusty armor adds <color=red>{0}</color> bonus health", BonusHealth);

    /// <summary>
    /// Adds bonus health to character
    /// </summary>
    public override void Apply()
    {
        base.Apply();

        // Adds 100 of max health increase.
        
        Inventory.Character.MaxHealth += BonusHealth;

    }

    /// <summary>
    /// Removes added health to the character
    /// </summary>
    public override void Clear()
    {
        // Removes the 100 from max health when removing equipment
        
        Inventory.Character.MaxHealth -= BonusHealth;
        // Ensures that CurrentHealth never goes above max health
        Inventory.Character.CurrentHealth = Mathf.Min(Inventory.Character.CurrentHealth, Inventory.Character.MaxHealth);


        base.Clear();
    }
}