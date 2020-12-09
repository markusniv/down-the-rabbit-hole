using System.Linq;
using UnityEngine;

/// <summary>
/// Normal empty room with chest in it.
/// </summary>
public class ChestRoom : Room
{
    public override void Create(GridLocation gridLocation)
    {
        MinDoors = 2;
        MaxDoors = 3;
        base.Create(gridLocation);
    }

    /// <summary>
    /// Initializes room with chest
    /// </summary>
    public override void Populate()
    {
        base.Populate();
        var chest = GetComponentInChildren<Chest>();
        var possibleItems = PrefabHelper.GetItems<Relic>();

        chest.hiddenItem = possibleItems.ElementAt(Random.Range(0, possibleItems.Count()));
    }
}