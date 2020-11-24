using UnityEngine;
/// <summary>
/// Most basic room type. Contains only enemies.
/// </summary>
[RoomPrefab("Prefabs/Rooms/GenericRoom")]
public class GenericRoom : Room
{
    /// <summary>
    /// Creates GenericRoom that has more doors when close to the center and less if more away.
    /// </summary>
    /// <inheritdoc/>
    public GenericRoom(GridLocation location) : base(location)
    {
        // if room is close to center
        if(Mathf.Abs(location.X) < 5 && Mathf.Abs(location.Y) < 5)
        {
            MinDoors = 1;
            MaxDoors = 2;
        }else
        {
            MinDoors = 0;
            MaxDoors = 1;
        }
    }
    /// <summary>
    /// Populates this room with 1 to 4 enemies
    /// </summary>
    public override void Populate()
    {
        base.Populate();
        // TODO: Create Enemies
    }
}