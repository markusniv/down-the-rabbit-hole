/// <summary>
/// This is where player spawns. It's almost empty room with many doors.
/// </summary>
public class SpawnRoom : Room
{
    /// <inheritdoc/>
    public override void Create(GridLocation gridLocation)
    {
        MinDoors = 3;
        MaxDoors = 4;
        base.Create(gridLocation);
    }
    /// <summary>
    /// Sets MinDoors to 3
    /// </summary>
    protected override void Start()
    {
        base.Start();

    }
}