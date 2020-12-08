using Weapons;
using UnityEngine;

public class PowerGantlent : PassiveRelic
{

    /// <summary>
    /// Set BonusPower to 0.2f
    /// </summary>
    public float BonusPower = 0.1f;
    /// <summary>
    /// when the mouse is hovering the item this text will show it self.
    /// </summary>
    public override string Tooltip => string.Format("These Gantlent increase your power by <color=red>20%</color>.");
    public override void Apply()
    {

        /// <summary>
        /// Apply when equiqment
        /// </summary>
        base.Apply();
        /// <summary>
        /// Add to damage modifier 0.1
        /// </summary>
        Inventory.Character.GetComponent<Character>().DamageModifier += BonusPower;
    }
    public override void Clear()
    {

        /// <summary>
        /// Reduce to damage modifier 0.1
        /// </summary>
        Inventory.Character.GetComponent<Character>().DamageModifier -= BonusPower;
        /// <summary>
        /// Clear the equipment when removed
        /// </summary>
        base.Clear();
    }
}