using Weapons;
using UnityEngine;

public class PowerGantlent : PassiveRelic
{
    public float BonusPower = 0.2f;

    public override string Tooltip => string.Format("These Gantlent increase your power by <color=red>20%</color>.");
    public override void Apply()
    {

        
        base.Apply();
        Inventory.Character.GetComponent<Character>().DamageModifier += BonusPower;
    }
    public override void Clear()
    {

        Inventory.Character.GetComponent<Character>().DamageModifier -= BonusPower;
        base.Clear();
    }
}