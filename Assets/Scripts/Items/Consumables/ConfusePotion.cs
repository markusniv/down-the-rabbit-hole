public class ConfusePotion : Consumable
{
    public override string Tooltip => string.Format("Confuse for <color=green>{5}</color>S");

 

    public override void Consume()
    {
        /// <summary>
        /// If your movement speed is equal or lower then 0 it will return item.
        /// </summary>
        if (Inventory.Character.Movement.MovementSpeedModifier <= 0) return;
        /// <summary>
        /// Call the status effect.
        /// </summary>
        Inventory.Character.AddStatusEffect(new Confused(Inventory.Character));

        base.Consume();

    }

}