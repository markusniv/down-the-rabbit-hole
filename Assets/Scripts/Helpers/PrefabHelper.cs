
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This helper class contains methods to get prefabs more easily
/// </summary>
public static class PrefabHelper
{
    /// <summary>
    /// Gets room prefab associated with the room script
    /// </summary>
    /// <typeparam name="T">Room type</typeparam>
    /// <returns>Returns unity prefab</returns>
    public static GameObject GetRoomPrefab<T>() where T : Room
    {
        return Resources.Load<GameObject>("Prefabs/Floors/Rooms/" + typeof(T).Name);
    }

    /// <summary>
    /// Gets all enemy prefabs
    /// </summary>
    /// <returns>Enemy prefabs</returns>
    public static GameObject[] GetEnemies()
    {
        return Resources.LoadAll<GameObject>("Prefabs/Characters/Enemies");
    }

    /// <summary>
    /// Gets all items and ensures that they have item script in them
    /// </summary>
    /// <typeparam name="T">Defines what kinda items to return</typeparam>
    /// <returns>Returns items</returns>
    public static IEnumerable<GameObject> GetItems<T>() where T : Item
    {

        return Resources.LoadAll<GameObject>("Prefabs/Items").Where(x => x.TryGetComponent<T>(out _));
    }
}
