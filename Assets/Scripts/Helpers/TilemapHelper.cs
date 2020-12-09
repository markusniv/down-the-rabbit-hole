using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This helper class contains methods to get tilemap related things more easily
/// </summary>
public static class TilemapHelper
{
    public enum TileType
    {
        TilemapGround,
        TilemapWall
    }
    /// <summary>
    /// Gets tile from tilemap
    /// </summary>
    /// <param name="type">Tile type to fetch</param>
    /// <returns>Returns tile object</returns>
    public static Tile GetTile(TileType type)
    {
        return Resources.Load<Tile>("Sprites/Tilemaps/" + type.ToString());
    }
}