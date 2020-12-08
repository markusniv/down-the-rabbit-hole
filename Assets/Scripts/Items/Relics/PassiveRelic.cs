public abstract class PassiveRelic : Relic
{
    /// <summary>
    /// This is called when this item is added to inventory.
    /// </summary>
    /// <param name="pickedUpBy">Who picked this Relic</param>
    public override void OnPickup(Character pickedUpBy)
    {
        base.OnPickup(pickedUpBy);
        Apply();
    }

    /// <summary>
    /// This is called when this item is dropped from inventory
    /// </summary>
    /// <param name="droppedBy">Who dropped this item</param>
    public override void OnDrop(Character droppedBy)
    {
        Clear();
        base.OnDrop(droppedBy);
    }

    public virtual void Apply()
    {

    }

    public virtual void Clear()
    {

    }
}