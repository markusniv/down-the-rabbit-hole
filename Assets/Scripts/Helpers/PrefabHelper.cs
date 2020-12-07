
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

    public static GameObject[] GetEnemies()
    {
        return Resources.LoadAll<GameObject>("Prefabs/Characters/Enemies");
    }
}
