/// <summary>
/// This is where player spawns. It's almost empty room with many doors.
/// </summary>
[RoomPrefab("Prefabs/Rooms/SpawnRoom")]
public class SpawnRoom : Room
{
    public SpawnRoom(GridLocation location) : base(location)
    {
    }

    /// <summary>
    /// Sets MinDoors to 3
    /// </summary>
    protected override void Start()
    {
        base.Start();
        MinDoors = 3;
    }
}