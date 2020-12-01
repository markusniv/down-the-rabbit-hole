public abstract class PassiveRelic : Relic
{

    public override void OnPickup(Character pickedUpBy)
    {
        base.OnPickup(pickedUpBy);
        Apply();
    }

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