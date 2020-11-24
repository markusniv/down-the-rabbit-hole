using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This Attribute specifies what prefab to use when room is created.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RoomPrefabAttribute : Attribute
{
    /// <summary>
    /// Path to prefab that will be used in <see cref="GetPrefab(Type)"/>
    /// </summary>
    string PathToPrefab;

    /// <summary>
    /// This specifies on what floor this prefab shoud be used. This allows different prefabs on different levels
    /// </summary>
    int FloorNumber;

    /// <summary>
    /// Specify prefab
    /// </summary>
    /// <param name="pathToPrefab">This path is defines same as Unitys Resources.Load</param>
    /// <param name="floorNumber">See <see cref="FloorNumber"/></param>
    public RoomPrefabAttribute(string pathToPrefab, int floorNumber = 0)
    {
        PathToPrefab = pathToPrefab;
        FloorNumber = floorNumber;
    }

    /// <summary>
    /// Gets Prefab for this type
    /// </summary>
    /// <param name="roomType">Type of the room</param>
    /// <returns>Unity Prefab</returns>
    public GameObject GetPrefab(Type roomType)
    {
        GameObject Prefab;
        if (!_cache.TryGetValue(roomType, out Prefab))
        {
            Prefab = Resources.Load<GameObject>(PathToPrefab);
            _cache.Add(roomType, Prefab);
        }
        return Prefab;
    }

    /// <summary>
    /// Cache for Prefabs
    /// </summary>
    private static Dictionary<Type, GameObject> _cache = new Dictionary<Type, GameObject>();
}