using UnityEngine;
/// <summary>
/// Most basic room type. Contains only enemies.
/// </summary>
public class GenericRoom : Room
{

    /// <inheritdoc/>
    public override void Create(GridLocation gridLocation)
    {
        // if room is close to center
        if (Mathf.Abs(gridLocation.X) < 5 && Mathf.Abs(gridLocation.Y) < 5)
        {
            MinDoors = 1;
            MaxDoors = 2;
        }
        else
        {
            MinDoors = 0;
            MaxDoors = 1;
        }
        base.Create(gridLocation);
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