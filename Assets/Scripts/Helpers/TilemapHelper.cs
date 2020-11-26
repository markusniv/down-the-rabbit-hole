using System.Collections;
using System.Collections.Generic;
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
    public static Tile GetTile(TileType type)
    {
        return Resources.Load<Tile>("Sprites/Tilemaps/" + type.ToString());
    }
}
