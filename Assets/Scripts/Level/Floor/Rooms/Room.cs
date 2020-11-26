using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

/// <summary>
/// Abstract definition of room.
/// </summary>
public abstract class Room : MonoBehaviour
{
    /// <summary>
    /// Maximum number of doors in this room. Use this to limit door count.
    /// </summary>
    public int MaxDoors { get; set; } = 4;

    /// <summary>
    /// Minimum number of doors in this room. Use this to force more doors.
    /// </summary>
    public int MinDoors { get; set; } = 1;

    /// <summary>
    /// Location of the room relative to its room.
    /// </summary>
    [Flags]
    public enum DoorLocation
    {
        None = 0,
        Top = 1,
        Right = 2,
        Bottom = 4,
        Left = 8
    }

    /// <summary>
    /// Door locations as bit field
    /// </summary>
    /// <example>
    /// Doors = DoorLocation.Top | DoorLocation.Bottom
    /// </example>
    public DoorLocation Doors { get; private set; }

    /// <summary>
    /// Gets door count
    /// </summary>
    /// <returns>Returns count of doors</returns>
    public int DoorCount() {
        int count = 0;
        foreach(DoorLocation d in Enum.GetValues(typeof(DoorLocation)))
        {
            if (d == DoorLocation.None) continue;
            if (Doors.HasFlag(d)) count++;
        }
        return count;
    }

    /// <summary>
    /// This will set <see cref="Doors"/> and it will modify tilemap and set doors into correct places
    /// </summary>
    /// <param name="doorLocations"></param>
    public void SetDoors(DoorLocation doorLocations)
    {
        Doors = doorLocations;
        var floorTile = TilemapHelper.GetTile(TilemapHelper.TileType.TilemapGround);
        var wallTile = TilemapHelper.GetTile(TilemapHelper.TileType.TilemapWall);

        // TODO: There might be more efficient method to do this
        // Clear doors
        Tilemap.SetTile(new Vector3Int(9, 9, 0), wallTile);
        Tilemap.SetTile(new Vector3Int(17, 5, 0), wallTile);
        Tilemap.SetTile(new Vector3Int(9, 0, 0), wallTile);
        Tilemap.SetTile(new Vector3Int(0, 5, 0), wallTile);
        // If room has top door
        if (Doors.HasFlag(Room.DoorLocation.Top))
        {
            Tilemap.SetTile(new Vector3Int(9, 9, 0), floorTile);
        }
        // If room has right door
        if (Doors.HasFlag(Room.DoorLocation.Right))
        {
            Tilemap.SetTile(new Vector3Int(17, 5, 0), floorTile);
        }
        // If room has bottom door
        if (Doors.HasFlag(Room.DoorLocation.Bottom))
        {
            Tilemap.SetTile(new Vector3Int(9, 0, 0), floorTile);
        }
        // If room has left door
        if (Doors.HasFlag(Room.DoorLocation.Left))
        {
            Tilemap.SetTile(new Vector3Int(0, 5, 0), floorTile);
        }
    }

    /// <summary>
    /// Location of the room relative to center of the grid. Center is (0,0) and room next to it is (1,0)
    /// </summary>
    public GridLocation Location { get; private set; }

    /// <summary>
    /// Derived classes may populate room with enemies or items with this method.
    /// </summary>
    public virtual void Populate() { }

    /// <summary>
    /// This method Creates structure of the room. This also creates Doors. Here you can place structural things in to the room for example pillars or bolders.
    /// </summary>
    public virtual void Create(GridLocation gridLocation)
    {
        Location = gridLocation;
        var doorNumber = Random.Range(MinDoors, MaxDoors + 1);
        IEnumerable<DoorLocation> doors;
        var doorValues = Enum.GetValues(typeof(DoorLocation)).Cast<int>().Where(x => x != 0).ToList();
        var doorsToEliminate = doorValues.Count - doorNumber;
        for(var i = 0; i < doorsToEliminate; i++)
        {
            doorValues.RemoveAt(Random.Range(0, doorValues.Count));
        }
        doors = doorValues.Count > 0 ? doorValues.Cast<DoorLocation>() : null;
        Doors = doors?.Aggregate((a, b) => a | b) ?? DoorLocation.None;

        transform.position = new Vector3(gridLocation.X * 18, gridLocation.Y * 10);
    }

    #region Components

    /// <summary>
    /// Inner collider that is used to detect characters entering the room.
    /// </summary>
    protected BoxCollider2D Collider;

    public Tilemap Tilemap;

    #endregion Components

    /// <summary>
    /// Virtual Awake. This initializes components. Derived classes should call this.
    /// </summary>
    protected virtual void Awake()
    {
        Tilemap = GetComponent<Tilemap>();
    }

    /// <summary>
    /// Virtual start. Derived classes can override this.
    /// </summary>
    protected virtual void Start()
    {
    }

    /// <summary>
    /// Virtual up. Derived classes can override this.
    /// </summary>
    protected virtual void Update()
    {
    }

    /// <summary>
    /// Definition for room location. Location is defined as grid. Center is (0,0) and room next to it is (1,0).
    /// </summary>
    public struct GridLocation
    {
        public GridLocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}