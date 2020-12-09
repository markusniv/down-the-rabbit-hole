using Weapons;
using UnityEngine;

public class PowerGauntlets : PassiveRelic
{

    /// <summary>
    /// Set BonusPower to 5f
    /// </summary>
    public float BonusPower = 5f;
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>
    public override string Tooltip => string.Format("These gauntlets increase your power by <color=red>{0}</color>.", BonusPower);
    public override void Apply()
    {

        /// <summary>
        /// Apply when equiqment
        /// </summary>
        base.Apply();
        /// <summary>
        /// Add to damage modifier 0.1
        /// </summary>
        Inventory.Character.FlatDamageModifier += BonusPower;
    }
    public override void Clear()
    {

        /// <summary>
        /// Reduce to damage modifier 0.1
        /// </summary>
        Inventory.Character.FlatDamageModifier -= BonusPower;
        /// <summary>
        /// Clear the equipment when removed
        /// </summary>
        base.Clear();
    }
}