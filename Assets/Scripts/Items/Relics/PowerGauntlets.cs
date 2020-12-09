/// <summary>
/// Relic that adds flat damage to the character
/// </summary>
public class PowerGauntlets : PassiveRelic
{
    /// <summary>
    /// Set BonusPower to 30f
    /// </summary>
    public float BonusPower = 30f;

    /// <inheritdoc/>
    public override string Tooltip => string.Format("These gauntlets increase your power by <color=red>{0}</color>.", BonusPower);

    /// <summary>
    /// Adds flat damage
    /// </summary>
    public override void Apply()
    {
        base.Apply();
        // Add to damage modifier 0.1
        Inventory.Character.FlatDamageModifier += BonusPower;
    }

    /// <summary>
    /// Removes added bonus
    /// </summary>
    public override void Clear()
    {
        // Reduce to damage modifier 0.1
        Inventory.Character.FlatDamageModifier -= BonusPower;

        base.Clear();
    }
}